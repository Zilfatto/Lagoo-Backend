using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RefreshAccessToken;
using Lagoo.BusinessLogic.Common.Exceptions.Api;
using Lagoo.BusinessLogic.Common.Exceptions.Base;
using Lagoo.BusinessLogic.UnitTests.CommandsAndQueries.Accounts.Common;
using Lagoo.BusinessLogic.UnitTests.Common.Helpers;
using Lagoo.Domain.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Lagoo.BusinessLogic.UnitTests.CommandsAndQueries.Accounts.Commands.RefreshAccessToken;

[TestFixture]
public class RefreshAccessTokenCommandHandlerTests : AccountTestsBase
{
    [SetUp]
    public void SetUp()
    {
        Context.RefreshTokens = TestHelpers.MockDbSet(Array.Empty<RefreshToken>());
    }
    
    [Test]
    public async Task Handle_CommandContainsValidDataAndRefreshTokenExists_ShouldReturnAccessTokenData()
    {
        Context.RefreshTokens = TestHelpers.MockDbSet(DefaultRefreshToken);

        var command = GenerateCommandWithValidDefaultData();

        var handler = CreateHandler();

        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.AreEqual(DefaultAccessToken, result.AccessToken);
        Assert.AreEqual(DefaultAccessTokenExpirationDate, result.AccessTokenExpiresAt);
    }

    [Test]
    public void Handle_CommandWithInvalidAccessToken_ShouldThrowBadRequestException()
    {
        JwtAuthService.GetPrincipalFromToken("").ThrowsForAnyArgs(new BaseArgumentException());

        var command = GenerateCommandWithValidDefaultData(accessToken: "invalid_access_token");

        var handler = CreateHandler();

        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_CommandWithAccessTokenWhichHasNoNameIdentifierClaim_ShouldThrowBadRequestException()
    {
        JwtAuthService.GetPrincipalFromToken("").ReturnsForAnyArgs(new ClaimsPrincipal(new ClaimsIdentity()));

        var command = GenerateCommandWithValidDefaultData(accessToken: "access_token_without_name_identifier");

        var handler = CreateHandler();
        
        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Handle_CommandWithAccessTokenWhichHasInvalidNameIdentifierClaim_ShouldThrowBadRequestException()
    {
        JwtAuthService.GetPrincipalFromToken("").ReturnsForAnyArgs(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "invalid_user_id") })));
        
        var command = GenerateCommandWithValidDefaultData(accessToken: "access_token_without_name_identifier");

        var handler = CreateHandler();
        
        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_RefreshTokenDoesNotExist_ShouldThrowBadRequestException()
    {
        var command = GenerateCommandWithValidDefaultData();
        
        var handler = CreateHandler();
        
        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Handle_RefreshTokenWithSpecifiedValueDoesNotExists_ShouldThrowBadRequestException()
    {
        Context.RefreshTokens = TestHelpers.MockDbSet(DefaultRefreshToken);

        var command = GenerateCommandWithValidDefaultData(refreshTokenValue: "some_refresh_token_value");
        
        var handler = CreateHandler();
        
        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_RefreshTokenExpired_ShouldThrowBadRequestException()
    {
        var defaultExpiredRefreshToken = GenerateDefaultRefreshToken(DefaultDeviceId, DateTime.MinValue, null);

        Context.RefreshTokens = TestHelpers.MockDbSet(defaultExpiredRefreshToken);

        var command = GenerateCommandWithValidDefaultData(refreshTokenValue: DefaultRefreshTokenValue);

        var handler = CreateHandler();

        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_JwtAuthServiceCannotGenerateAccessToken_ShouldThrowBadRequestException()
    {
        JwtAuthService.GenerateAccessTokenAsync(DefaultUser).ThrowsForAnyArgs<BaseArgumentException>();
        Context.RefreshTokens = TestHelpers.MockDbSet(DefaultRefreshToken);

        var command = GenerateCommandWithValidDefaultData();

        var handler = CreateHandler();

        Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
    }

    private RefreshAccessTokenCommandHandler CreateHandler() => new(Context, JwtAuthService);
    
    private RefreshAccessTokenCommand GenerateCommandWithValidDefaultData(string accessToken = DefaultAccessToken,
        string refreshTokenValue = DefaultRefreshTokenValue) => new()
    {
        AccessToken = accessToken,
        RefreshTokenValue = refreshTokenValue
    };
}