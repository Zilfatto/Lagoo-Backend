using System.Security.Claims;
using Lagoo.BusinessLogic.Common.Exceptions.Api;
using Lagoo.BusinessLogic.Common.Exceptions.Base;
using Lagoo.BusinessLogic.Common.ExternalServices.Database;
using Lagoo.BusinessLogic.Common.Services.JwtAuthService;
using Lagoo.BusinessLogic.Resources.CommandsAndQueries;
using Lagoo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RefreshAccessToken;

/// <summary>
///   Request handler for <see cref="RefreshAccessTokenCommand"/>
/// </summary>
public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, RefreshAccessTokenResponseDto>
{
    private readonly IAppDbContext _context;

    private readonly IJwtAuthService _jwtAuthService;

    public RefreshAccessTokenCommandHandler(IAppDbContext context, IJwtAuthService jwtAuthService)
    {
        _context = context;
        _jwtAuthService = jwtAuthService;
    }

    public async Task<RefreshAccessTokenResponseDto> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = ExtractUserIdFromAccessToken(request.AccessToken);

        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.Owner)
            .FirstOrDefaultAsync(rt => rt.Value == request.RefreshTokenValue && rt.OwnerId == userId, cancellationToken);

        if (refreshToken is null || refreshToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new BadRequestException(AccountResources.RefreshTokenWasNotFoundOrExpired);
        }

        var (accessToken, accessTokenExpiresAt) = await TryCreateAccessTokenAsync(refreshToken.Owner);

        return new RefreshAccessTokenResponseDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt
        };
    }

    private Guid ExtractUserIdFromAccessToken(string accessToken)
    {
        ClaimsPrincipal claimsPrincipal;
        
        try
        {
            claimsPrincipal = _jwtAuthService.GetPrincipalFromToken(accessToken);
        }
        catch (BaseArgumentException exception)
        {
            throw new BadRequestException(exception.Message);
        }
        
        var claimUserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (claimUserId is null || !Guid.TryParse(claimUserId, out var userId))
        {
            throw new BadRequestException(AccountResources.InvalidJwtClaims);
        }

        return userId;
    }
    
    private Task<(string, DateTime)> TryCreateAccessTokenAsync(AppUser user)
    {
        try
        {
            return _jwtAuthService.GenerateAccessTokenAsync(user);
        }
        catch (BaseArgumentException exception)
        {
            throw new BadRequestException(exception.Message);
        }
    }
}