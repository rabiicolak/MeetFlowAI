using Microsoft.AspNetCore.Mvc;
using MeetFlow.Web.Services;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Controllers
{
    [Route("[controller]")]
    public class AudioController : Controller
    {
        private readonly IAudioTranscriptionService _audioTranscriptionService;
        private readonly IMeetingAnalysisService _meetingAnalysisService;

        public AudioController(IAudioTranscriptionService audioTranscriptionService, IMeetingAnalysisService meetingAnalysisService)
        {
            _audioTranscriptionService = audioTranscriptionService;
            _meetingAnalysisService = meetingAnalysisService;
        }

        // AI-Augmented Workflow: Ses kaydından metne geçiş pipeline’ı backend seviyesinde hazırlanmıştır.
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
                durationEstimateSeconds = result.DurationEstimateSeconds,
                confidenceScore = result.ConfidenceScore,
                detectedLanguage = result.DetectedLanguage,
                createdAt = result.CreatedAt
            });
        }

        [HttpPost("AnalyzeMeetingAudio")]
        public async Task<IActionResult> AnalyzeMeetingAudio(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return Json(new { success = false, message = "Ses dosyası bulunamadı." });
            }

            // 1 & 2: Audio upload & transcription
            var transcriptionResult = await _audioTranscriptionService.TranscribeAsync(audioFile);
            if (!transcriptionResult.Success)
            {
                return Json(new { success = false, message = transcriptionResult.Message });
            }

            // 3: Transcription -> Meeting Analysis
            var analysisResult = await _meetingAnalysisService.AnalyzeAsync(transcriptionResult.Transcript);

            // 4: Return combined result
            var responseModel = new AudioMeetingAnalysisResponseViewModel
            {
                Audio = transcriptionResult,
                Analysis = analysisResult
            };

            return Json(new { success = true, message = "Analiz başarıyla tamamlandı.", data = responseModel });
        }
    }
}
