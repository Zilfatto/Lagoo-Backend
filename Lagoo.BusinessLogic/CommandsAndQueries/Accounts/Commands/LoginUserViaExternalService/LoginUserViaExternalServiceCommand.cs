using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Common.Dtos;
using Lagoo.Domain.Enums;
using MediatR;

namespace Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.LoginUserViaExternalService;

/// <summary>
///   Command to login user in the app via an external authentication service
/// </summary>
public class LoginUserViaExternalServiceCommand : IRequest<AuthenticationTokensDto>
{
    /// <summary>
    ///   External authentication service
    /// </summary>
    public ExternalAuthService ExternalAuthService { get; set; }

    /// <summary>
    ///   Access token for getting needed user information from other platforms
    /// </summary>
    public string ExternalServiceAccessToken { get; set; } = string.Empty;
    
    /// <summary>
    ///   A Guid associated with a particular device for updating Refresh token if it exists on a device,
    ///   otherwise create a new one
    /// </summary>
    public Guid DeviceId { get; set; }
}