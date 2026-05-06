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
    public async Task<IActionResult> Analyze(MeetingAnalysisRequestViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", request);
        }

        var result = await _meetingAnalysisService.AnalyzeAsync(request.MeetingText);
        
        // Frontend tasarımı bozulmasın diye (eğer mevcutsa) ViewData veya ViewBag üzerinden aktarılabilir, 
        // ya da doğrudan model olarak Index'e dönülebilir. 
        // Ancak frontend tasarımı dokunulmamış haliyle AJAX ile çalışacaksa 
        // bu metod normalde JSON dönmeliydi. Ama View dönmesi istendiği için:
        ViewBag.AnalysisResult = result;
        
        return View("Index", request);
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
