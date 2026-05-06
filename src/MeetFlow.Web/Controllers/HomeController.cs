using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeetFlow.Web.Models;
using MeetFlow.Web.Services;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMeetingAnalysisService _meetingAnalysisService;

    public HomeController(ILogger<HomeController> logger, IMeetingAnalysisService meetingAnalysisService)
    {
        _logger = logger;
        _meetingAnalysisService = meetingAnalysisService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Analyze(MeetingAnalysisRequestViewModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.MeetingText))
        {
            return View("Index", model);
        }

        var result = await _meetingAnalysisService.AnalyzeAsync(model.MeetingText);
        
        ViewBag.AnalysisResult = result;
        return View("Index", model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
