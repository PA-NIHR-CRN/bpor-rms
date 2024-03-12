using System.Diagnostics;
using Bpor.Rms.Infrastructure.Entities;
using Bpor.Rms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bpor.Rms.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bpor.Rms.Web.Controllers;

public class HomeController(ILogger<HomeController> logger, IIdentityProviderService idpService, ParticipantDbContext dbContext)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var participants = await dbContext.Participants.ToListAsync();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
