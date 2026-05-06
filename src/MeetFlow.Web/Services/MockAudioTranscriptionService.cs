using Microsoft.AspNetCore.Http;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    // AI Skills Agent: Ses kaydı transkript akışı gerçek API entegrasyonuna hazır olacak şekilde mock servisle simüle edilmiştir.
    public class MockAudioTranscriptionService : IAudioTranscriptionService
    {
        public Task<AudioTranscriptionResultViewModel> TranscribeAsync(IFormFile audioFile)
        {
            var result = new AudioTranscriptionResultViewModel();

            if (audioFile == null || audioFile.Length == 0)
            {
                result.Success = false;
                result.Message = "Ses dosyası bulunamadı veya boş.";
                return Task.FromResult(result);
            }

            var allowedMimeTypes = new[] { "audio/mpeg", "audio/wav", "audio/ogg", "audio/x-m4a", "audio/mp4" };
            if (!allowedMimeTypes.Contains(audioFile.ContentType.ToLowerInvariant()) && !audioFile.FileName.EndsWith(".mp3") && !audioFile.FileName.EndsWith(".wav"))
            {
                result.Success = false;
                result.Message = "Geçersiz dosya formatı. Sadece ses dosyaları kabul edilmektedir.";
                return Task.FromResult(result);
            }

            // Mock transcription (Demo verisi)
            result.Success = true;
            result.Message = "Ses kaydı başarıyla metne dönüştürüldü.";
            result.Transcript = "Rabia görevleri tamamlasın. Hatice finansal işleri yarına kadar halletsin.";
            result.DurationEstimateSeconds = 35;

            return Task.FromResult(result);
        }
    }
}
