using Lagoo.BusinessLogic.Common.Exceptions.Api;
using Lagoo.BusinessLogic.Common.Extensions;
using Lagoo.BusinessLogic.Common.ExternalServices.FacebookAuthService;
using Lagoo.BusinessLogic.Common.ExternalServices.GoogleAuthService;
using Lagoo.BusinessLogic.Common.ExternalServices.Models;
using Lagoo.BusinessLogic.Resources.CommandsAndQueries;
using Lagoo.Domain.Entities;
using Lagoo.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Lagoo.BusinessLogic.Common.Services.ExternalAuthServicesManager;

public class ExternalAuthServicesManager : IExternalAuthServicesManager
{
    private readonly UserManager<AppUser> _userManager;
    
    private readonly IFacebookAuthService _facebookAuthService;

    private readonly IGoogleAuthService _googleAuthService;

    public ExternalAuthServicesManager(UserManager<AppUser> userManager, IFacebookAuthService facebookAuthService, IGoogleAuthService googleAuthService)
    {
        _userManager = userManager;
        _facebookAuthService = facebookAuthService;
        _googleAuthService = googleAuthService;
    }
    
    public async Task<IExternalAuthServiceUserInfo> GetUserInfoAsync(ExternalAuthService externalAuthService, string accessToken) => externalAuthService switch
    {
        ExternalAuthService.Facebook => await _facebookAuthService.GetUserInfoAsync(accessToken),
        ExternalAuthService.Google => await _googleAuthService.GetUserInfoAsync(accessToken),
        _ => throw new ArgumentOutOfRangeException(nameof(externalAuthService), AccountResources.InvalidExternalAuthService)
    };

    public async Task<IdentityResult> BindUserAsync(AppUser user, ExternalAuthService externalAuthService, string accessToken)
    {
        var userInfo = await GetUserInfoAsync(externalAuthService, accessToken);

        return await _userManager.AddLoginAsync(user, new UserLoginInfo(externalAuthService.GetEnumDescription(), userInfo.Id, userInfo.ToString()));
    }

    public async Task<IdentityResult> UnbindUserAsync(AppUser user, ExternalAuthService externalAuthService)
    {
        var logins = await _userManager.GetLoginsAsync(user);

        if (logins is null || !logins.Any())
        {
            throw new BadRequestException(AccountResources.UserDoesNotHaveExternalLogins);
        }

        var loginProvider = externalAuthService.GetEnumDescription();
        var userLoginInfo = logins.SingleOrDefault(uli => uli.LoginProvider == loginProvider);
        
        if (userLoginInfo is null)
        {
            throw new BadRequestException(AccountResources.UserDoesNotHaveSpecificExternalLogin);
        }

        return await _userManager.RemoveLoginAsync(user, userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
    }
}