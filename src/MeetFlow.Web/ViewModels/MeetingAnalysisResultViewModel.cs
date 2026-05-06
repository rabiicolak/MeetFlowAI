namespace MeetFlow.Web.ViewModels
{
    public class MeetingAnalysisResultViewModel
    {
        public string Summary { get; set; } = string.Empty;
        public int RiskScore { get; set; } = 0;
        public int QualityScore { get; set; } = 0;
        public string WorkloadStatus { get; set; } = string.Empty;
        public string NextStep { get; set; } = string.Empty;
        public List<string> Decisions { get; set; } = new List<string>();
        public List<TaskItemViewModel> Tasks { get; set; } = new List<TaskItemViewModel>();
        public List<string> Risks { get; set; } = new List<string>();
        public List<string> Deadlines { get; set; } = new List<string>();
        public List<string> Participants { get; set; } = new List<string>();
    }
}
