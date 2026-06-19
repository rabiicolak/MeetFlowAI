using Microsoft.AspNetCore.Http;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    // AI Skills Agent: Ses kayıtlarından toplantı transkripti üretimi mock AI pipeline ile simüle edilmiştir.
    public class MockAudioTranscriptionService : IAudioTranscriptionService
    {
        public Task<AudioAnalysisResultViewModel> TranscribeAsync(IFormFile audioFile)
        {
            var result = new AudioAnalysisResultViewModel
            {
                CreatedAt = DateTime.UtcNow
            };

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

            result.Success = true;
            result.Message = "Ses kaydı başarıyla metne dönüştürüldü.";
            result.ConfidenceScore = 0.94;
            result.DetectedLanguage = "tr-TR";

            var fileName = audioFile.FileName.ToLowerInvariant();
            
            if (fileName.Contains("risk"))
            {
                result.Transcript = "Rabia görevleri tamamlasın. Hatice finansal raporu cuma gününe kadar hazırlasın. Sunucuda gecikme riski bulunuyor.";
                result.DurationEstimateSeconds = 45;
            }
            else if (audioFile.Length > 1024 * 1024 * 5) // 5MB'dan büyükse uzun diyelim
            {
                result.Transcript = "Herkese merhaba, toplantıya başlayalım. Rabia görevleri tamamlasın. Hatice finansal işleri halletsin. Mehmet backend entegrasyonuna başlayacak. Projenin cuma gününe kadar bitmesi planlandı. Ayrıca bazı tasarımsal eksiklikler bir risk oluşturabilir, Can ilgilenecek.";
                result.DurationEstimateSeconds = 120;
            }
            else
            {
                result.Transcript = "Toplantı başladı. Rabia görevleri yarına kadar tamamlayacak. Yeni tasarım onaylandı.";
                result.DurationEstimateSeconds = 25;
            }

            return Task.FromResult(result);
        }
    }
}

