namespace MeetFlow.Web.ViewModels
{
    public class AudioMeetingAnalysisResponseViewModel
    {
        public AudioAnalysisResultViewModel Audio { get; set; } = new AudioAnalysisResultViewModel();
        public MeetingAnalysisResultViewModel Analysis { get; set; } = new MeetingAnalysisResultViewModel();
    }
}
