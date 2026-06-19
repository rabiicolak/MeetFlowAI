namespace MeetFlow.Web.ViewModels
{
    public class MeetingAnalysisResultViewModel
    {
        public string Summary { get; set; } = string.Empty;
        public List<string> Decisions { get; set; } = new List<string>();
        public List<string> ActionItems { get; set; } = new List<string>();
        public List<string> Risks { get; set; } = new List<string>();
        public List<string> Participants { get; set; } = new List<string>();
        public List<string> Deadlines { get; set; } = new List<string>();
        public string FollowUpSuggestion { get; set; } = string.Empty;
        public int ManualTimeMinutes { get; set; }
        public int AiTimeMinutes { get; set; }
        public int SavedTimeMinutes { get; set; }
        public int EfficiencyGainPercentage { get; set; }
        public int RiskScore { get; set; }
        public int MeetingQualityScore { get; set; }
        public Dictionary<string, int> WorkloadDistribution { get; set; } = new Dictionary<string, int>();
    }
}
