using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.LoginUser;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.LoginUserViaExternalService;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RefreshAccessToken;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RegisterUser;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Common.Dtos;
using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Queries.GetExternalAuthServiceUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lagoo.Api.Controllers;

/// <summary>
///   The accounts controller
/// </summary>
[Route("api/accounts")]
public class AccountsController : ApiController
{
    public AccountsController(ISender sender) : base(sender) { }

    /// <summary>
    ///   Register a user in the app, optionally via an external authentication
    /// </summary>
    /// <param name="command">User data for registration</param>
    /// <returns>Access and Refresh tokens with their expiration dates, device ID</returns>
    [HttpPost("auth/register")]
    public Task<AuthenticationDataDto> RegisterUser([FromBody] RegisterUserCommand command) => Sender.Send(command);

    /// <summary>
    ///   Login a user in the app
    /// </summary>
    /// <param name="command">User data to login in the app</param>
    /// <returns>Access and Refresh tokens with their expiration dates, device ID</returns>
    [HttpPost("auth/login")]
    public Task<AuthenticationDataDto> LoginUser([FromBody] LoginUserCommand command) => Sender.Send(command);

    /// <summary>
    ///   Login a user via any supported external authentication service
    /// </summary>
    /// <param name="command">External authentication service, its access token and an optional refresh token if exists on a device</param>
    /// <returns>Access and Refresh tokens with their expiration dates, device ID</returns>
    [HttpPost("auth/external-service/login")]
    public Task<AuthenticationDataDto> LoginUserViaExternalAuthService([FromBody] LoginUserViaExternalServiceCommand command) =>
        Sender.Send(command);

    /// <summary>
    ///   Get user information from specified external authentication service
    /// </summary>
    /// <param name="query">External authentication service and access token to it</param>
    /// <returns>User info from specified external authentication service</returns>
    [HttpGet("auth/external-service/user-info")]
    public Task<GetExternalAuthServiceUserInfoResponseDto> GetExternalAuthServiceUserInfo(
        [FromQuery] GetExternalAuthServiceUserInfoQuery query) => Sender.Send(query);

    /// <summary>
    ///   Refresh access token using refresh token for further access to guarded endpoints
    /// </summary>
    /// <param name="command">Expired Access token and Refresh token</param>
    /// <returns>New Access token</returns>
    [HttpPost("auth/refresh")]
    public Task<RefreshAccessTokenResponseDto> RefreshAccessToken([FromBody] RefreshAccessTokenCommand command) =>
        Sender.Send(command);
}