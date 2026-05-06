using Microsoft.AspNetCore.Mvc;
using MeetFlow.Web.Services;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Controllers
{
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportExportService _reportExportService;
        private readonly IEmailService _emailService;

        public ReportController(IReportExportService reportExportService, IEmailService emailService)
        {
            _reportExportService = reportExportService;
            _emailService = emailService;
        }

        [HttpPost("ExportText")]
        public IActionResult ExportText([FromBody] MeetingAnalysisResultViewModel analysis)
        {
            if (analysis == null)
            {
                return Json(new { success = false, message = "Analiz verisi bulunamadı." });
            }

            var reportContent = _reportExportService.GeneratePlainTextReport(analysis);
            return Json(new { success = true, message = "Rapor başarıyla oluşturuldu.", reportContent = reportContent });
        }

        [HttpPost("ExportMarkdown")]
        public IActionResult ExportMarkdown([FromBody] MeetingAnalysisResultViewModel analysis)
        {
            if (analysis == null)
            {
                return Json(new { success = false, message = "Analiz verisi bulunamadı." });
            }

            var reportContent = _reportExportService.GenerateMarkdownReport(analysis);
            return Json(new { success = true, message = "Rapor başarıyla oluşturuldu.", reportContent = reportContent });
        }

        [HttpPost("ExportHtml")]
        public IActionResult ExportHtml([FromBody] MeetingAnalysisResultViewModel analysis)
        {
            if (analysis == null)
            {
                return Json(new { success = false, message = "Analiz verisi bulunamadı." });
            }

            var reportContent = _reportExportService.GenerateHtmlReport(analysis);
            return Json(new { success = true, message = "Rapor başarıyla oluşturuldu.", reportContent = reportContent });
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] SendReportRequestViewModel request)
        {
            if (!ModelState.IsValid || request == null)
            {
                return Json(new { success = false, message = "Geçersiz e-posta bilgileri veya boş içerik." });
            }

            var success = await _emailService.SendMeetingReportAsync(request.RecipientEmail, request.Subject, request.ReportContent);

            if (success)
            {
                return Json(new { success = true, message = $"{request.RecipientEmail} adresine toplantı raporu başarıyla gönderildi." });
            }
            else
            {
                return Json(new { success = false, message = "E-posta gönderilirken bir hata oluştu." });
            }
        }
    }
}
