using Microsoft.AspNetCore.Http;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    public interface IAudioTranscriptionService
    {
        Task<AudioTranscriptionResultViewModel> TranscribeAsync(IFormFile audioFile);
    }
}
