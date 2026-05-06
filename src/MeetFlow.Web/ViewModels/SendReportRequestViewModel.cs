using System.ComponentModel.DataAnnotations;

namespace MeetFlow.Web.ViewModels
{
    public class SendReportRequestViewModel
    {
        [Required(ErrorMessage = "E-posta adresi boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string RecipientEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konu boş olamaz.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rapor içeriği boş olamaz.")]
        public string ReportContent { get; set; } = string.Empty;
    }
}
