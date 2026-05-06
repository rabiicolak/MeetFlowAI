using System.ComponentModel.DataAnnotations;

namespace MeetFlow.Web.ViewModels
{
    public class MeetingAnalysisRequestViewModel
    {
        [Required(ErrorMessage = "Toplantı metni boş olamaz.")]
        [MinLength(5, ErrorMessage = "Toplantı metni en az 5 karakter olmalıdır.")]
        public string MeetingText { get; set; } = string.Empty;
    }
}
