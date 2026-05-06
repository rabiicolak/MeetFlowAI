namespace MeetFlow.Web.Services
{
    public interface IEmailService
    {
        Task<bool> SendMeetingReportAsync(string recipientEmail, string subject, string reportContent);
    }
}
