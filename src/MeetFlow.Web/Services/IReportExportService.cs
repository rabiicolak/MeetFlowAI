using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    public interface IReportExportService
    {
        string GeneratePlainTextReport(MeetingAnalysisResultViewModel analysis);
        string GenerateMarkdownReport(MeetingAnalysisResultViewModel analysis);
        string GenerateHtmlReport(MeetingAnalysisResultViewModel analysis);
    }
}
