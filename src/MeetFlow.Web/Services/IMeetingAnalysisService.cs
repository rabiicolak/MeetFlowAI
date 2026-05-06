using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    public interface IMeetingAnalysisService
    {
        Task<MeetingAnalysisResultViewModel> AnalyzeAsync(string meetingText);
    }
}
