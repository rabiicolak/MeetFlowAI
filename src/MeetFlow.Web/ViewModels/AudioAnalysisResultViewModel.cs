namespace MeetFlow.Web.ViewModels
{
    public class AudioAnalysisResultViewModel
    {
        public string Transcript { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int DurationEstimateSeconds { get; set; }
        public double ConfidenceScore { get; set; }
        public string DetectedLanguage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
