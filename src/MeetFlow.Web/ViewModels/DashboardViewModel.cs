using System.Collections.Generic;

namespace MeetFlow.Web.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAnalysis { get; set; } = 128;
        public int TotalAudioAnalysis { get; set; } = 42;
        public int TotalTimeSaved { get; set; } = 76;
        public int AverageEfficiency { get; set; } = 84;
        public int AverageRiskScore { get; set; } = 37;
        public int TotalReportsSent { get; set; } = 58;

        public int TodayAudioRecords { get; set; } = 6;
        public int AvgTranscriptConfidence { get; set; } = 91;
        public int LongestMeetingMinutes { get; set; } = 54;
        public int TurkishDetectionRate { get; set; } = 98;

        public int EmailReports { get; set; } = 24;
        public int HtmlExport { get; set; } = 15;
        public int MarkdownExport { get; set; } = 11;
        public int TxtExport { get; set; } = 8;
        
        public List<MeetingItem> RecentMeetings { get; set; } = new List<MeetingItem>();
    }

    public class MeetingItem
    {
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Source { get; set; }
        public int RiskScore { get; set; }
        public int QualityScore { get; set; }
        public int Efficiency { get; set; }
        public string? ReportStatus { get; set; }
    }
}
