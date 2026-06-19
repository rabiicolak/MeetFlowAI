namespace MeetFlow.Web.ViewModels
{
    public class AudioTranscriptionResultViewModel
    {
        public string Transcript { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int DurationEstimateSeconds { get; set; }
    }
}
