using Bpor.Rms.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bpor.Rms.Web.Controllers;

public class AccountController(IOptions<IdentityProviderApiSettings> config) : Controller
{
    public async Task<IActionResult> Logout()
    {
        // Sign out of the application
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirect to WSO2 IS for logout
        var callbackUrl = Url.Action("Index", "Home", null, protocol: Request.Scheme);
        var logoutUrl =
            $"{config.Value.BaseUrl}/oidc/logout?post_logout_redirect_uri={Uri.EscapeDataString(callbackUrl)}&id_token_hint={HttpContext.GetTokenAsync("id_token").Result}";
        return Redirect(logoutUrl);
    }
}
