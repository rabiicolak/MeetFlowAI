using System.Diagnostics;

namespace MeetFlow.Web.Services
{
    // AI-Assisted Communication Flow: Toplantı analiz raporlarının paylaşım akışı mock servis ile simüle edilmiştir.
    public class MockEmailService : IEmailService
    {
        public Task<bool> SendMeetingReportAsync(string recipientEmail, string subject, string reportContent)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(reportContent))
            {
                return Task.FromResult(false);
            }

            // Fake mail log
            Debug.WriteLine($"[MockEmailService] Gönderilen Adres: {recipientEmail}");
            Debug.WriteLine($"[MockEmailService] Konu: {subject}");
            Debug.WriteLine($"[MockEmailService] İçerik Uzunluğu: {reportContent.Length} karakter");

            return Task.FromResult(true);
        }
    }
}
