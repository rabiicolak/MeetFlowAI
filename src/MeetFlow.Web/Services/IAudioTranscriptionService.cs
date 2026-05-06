using Microsoft.AspNetCore.Http;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    public interface IAudioTranscriptionService
    {
        Task<AudioAnalysisResultViewModel> TranscribeAsync(IFormFile audioFile);
    }
}
