using Microsoft.AspNetCore.Mvc;
using MeetFlow.Web.Services;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Controllers
{
    // AI-Augmented Workflow: Ses kaydından metne geçiş pipeline’ı backend seviyesinde hazırlanmıştır.
    [Route("[controller]")]
    public class AudioController : Controller
    {
        private readonly IAudioTranscriptionService _audioTranscriptionService;

        public AudioController(IAudioTranscriptionService audioTranscriptionService)
        {
            _audioTranscriptionService = audioTranscriptionService;
        }

        [HttpPost("Transcribe")]
        public async Task<IActionResult> Transcribe(IFormFile audioFile)
        {
            var result = await _audioTranscriptionService.TranscribeAsync(audioFile);
            
            if (!result.Success)
            {
                return Json(new { success = false, message = result.Message });
            }

            return Json(new 
            { 
                success = true, 
                transcript = result.Transcript, 
                message = result.Message,
                durationEstimateSeconds = result.DurationEstimateSeconds
            });
        }
    }
}
