using Microsoft.AspNetCore.Mvc;
using MeetFlow.Web.ViewModels;
using System.Collections.Generic;

namespace MeetFlow.Web.Controllers
{
    public class DashboardController : Controller
    {
        // AI Plan Agent: Dashboard metrikleri, manuel iş yükü azaltımını görünür hale getirmek için planlanmıştır.
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalAnalysis = 128,
                TotalAudioAnalysis = 42,
                TotalTimeSaved = 76,
                AverageEfficiency = 84,
                AverageRiskScore = 37,
                TotalReportsSent = 58,
                
                TodayAudioRecords = 6,
                AvgTranscriptConfidence = 91,
                LongestMeetingMinutes = 54,
                TurkishDetectionRate = 98,
                
                EmailReports = 24,
                HtmlExport = 15,
                MarkdownExport = 11,
                TxtExport = 8,

                RecentMeetings = new List<MeetingItem>
                {
                    new MeetingItem { Title = "Sprint Planlama", Type = "Yazılım", Source = "Metin", RiskScore = 22, QualityScore = 88, Efficiency = 84, ReportStatus = "Gönderildi" },
                    new MeetingItem { Title = "Sunucu Kesintisi", Type = "Kriz", Source = "Ses", RiskScore = 78, QualityScore = 64, Efficiency = 81, ReportStatus = "Bekliyor" },
                    new MeetingItem { Title = "Yönetim Değerlendirme", Type = "Yönetim", Source = "Metin", RiskScore = 35, QualityScore = 79, Efficiency = 86, ReportStatus = "Gönderildi" },
                    new MeetingItem { Title = "Finans Takibi", Type = "Operasyon", Source = "Ses", RiskScore = 41, QualityScore = 72, Efficiency = 80, ReportStatus = "Export Edildi" }
                }
            };

            return View(model);
        }
    }
}
