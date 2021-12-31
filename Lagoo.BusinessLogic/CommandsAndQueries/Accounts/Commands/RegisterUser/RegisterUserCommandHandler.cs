using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.CreateAuthTokens;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Common.Dtos;
using Lagoo.BusinessLogic.Common.Exceptions.Api;
using Lagoo.BusinessLogic.Common.Services.ExternalAuthServicesManager;
using Lagoo.BusinessLogic.Resources.CommandsAndQueries;
using Lagoo.Domain.Entities;
using Lagoo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RegisterUser;

/// <summary>
/// Request handler for <see cref="RegisterUserCommand"/>
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationTokensDto>
{
    private readonly UserManager<AppUser> _userManager;

    private readonly IMediator _mediator;

    private readonly IExternalAuthServicesManager _externalAuthServicesManager;

    private readonly IStringLocalizer<AccountResources> _accountLocalizer;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, IMediator mediator, IExternalAuthServicesManager externalAuthServicesManager, IStringLocalizer<AccountResources> accountLocalizer)
    {
        _userManager = userManager;
        _mediator = mediator;
        _externalAuthServicesManager = externalAuthServicesManager;
        _accountLocalizer = accountLocalizer;
    }

    public async Task<AuthenticationTokensDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        if (request.ExternalAuthService is not null && request.ExternalAuthServiceAccessToken is not null)
        {
            await CreateAccountWithExternalLogin(user, request.ExternalAuthService.Value, request.ExternalAuthServiceAccessToken);
        }
        else if (request.Password is not null)
        {
            await CreateAccount(user, request.Password);
        }
        else
        {
            throw new BadRequestException(_accountLocalizer["InvalidData"]);
        }

        return await _mediator.Send(new CreateAuthTokensCommand(user), cancellationToken);
    }

    private async Task CreateAccount(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new BadRequestException(result);
        }
    }

    private async Task CreateAccountWithExternalLogin(AppUser user, ExternalAuthService externalAuthService,
        string accessToken)
    {
        var result = await _userManager.CreateAsync(user);
        
        if (!result.Succeeded)
        {
            throw new BadRequestException(result);
        }
        
        var loginResult = await _externalAuthServicesManager.BindUserAsync(user, externalAuthService, accessToken);
            
        if (!loginResult.Succeeded)
        {
            throw new BadRequestException(loginResult);
        }
    }
}