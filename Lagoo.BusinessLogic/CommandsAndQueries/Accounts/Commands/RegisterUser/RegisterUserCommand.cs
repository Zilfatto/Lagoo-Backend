using Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Common.Dtos;
using Lagoo.Domain.Enums;
using MediatR;

namespace Lagoo.BusinessLogic.CommandsAndQueries.Accounts.Commands.RegisterUser;

/// <summary>
///   Command for registering users in the app.
///   If an external authentication service has been specified, created account will be bound to it.
///   Will throw an exception, if a connection to an external authentication service has failed. 
/// </summary>
public class RegisterUserCommand : IRequest<AuthenticationDataDto>
{
    /// <summary>
    ///   First name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    ///   Last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    ///   Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///   Password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///   Password confirmation
    /// </summary>
    public string? ConfirmPassword { get; set; }

    /// <summary>
    ///   External authentication service
    /// </summary>
    public ExternalAuthService? ExternalAuthService { get; set; }

    /// <summary>
    ///   Access token of external authentication service for getting needed user information from it 
    /// </summary>
    public string? ExternalAuthServiceAccessToken { get; set; }

    /// <summary>
    ///   A Guid associated with a particular device after register or login.
    ///   Used for updating Refresh token if it existed on a device, otherwise create a new one
    /// </summary>
    public Guid? DeviceId { get; set; }
}