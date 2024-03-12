using System.Diagnostics;
using Bpor.Rms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bpor.Rms.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bpor.Rms.Web.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger, IIdentityProviderService idpService)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var token = await idpService.GetOrAcquireTokenAsync();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
