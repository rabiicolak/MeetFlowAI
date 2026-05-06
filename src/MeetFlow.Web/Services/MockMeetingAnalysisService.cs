using System.Text.RegularExpressions;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    // AI Skills Agent: Türkçe toplantı metni analizi, görev/karar/risk çıkarımı ve verimlilik hesaplama kuralları bu servis üzerinde optimize edilmiştir.
    public class MockMeetingAnalysisService : IMeetingAnalysisService
    {
        public Task<MeetingAnalysisResultViewModel> AnalyzeAsync(string meetingText)
        {
            var result = new MeetingAnalysisResultViewModel();

            if (string.IsNullOrWhiteSpace(meetingText))
                return Task.FromResult(result);

            // Cümle ve parçalara ayırma
            var separators = new[] { ".", ",", ";", "\n", " ve ", " ama ", " fakat ", " ayrıca " };
            var fragments = meetingText.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(f => f.Trim())
                                       .Where(f => f.Length > 0)
                                       .ToList();

            var taskKeywords = new[] { "yapacak", "yapılacak", "tamamlayacak", "tamamlasın", "tamamla", "halledecek", "halletsin", "hallet", "başlayacak", "başlasın", "hazırlanacak", "hazırlayacak", "hazırlasın", "test edecek", "kontrol edecek", "ilgilenecek", "sorumlu", "teslim edecek", "bitirecek" };
            var decisionKeywords = new[] { "karar aldık", "karar verildi", "onaylandı", "kabul edildi", "kullanılacak", "başlanacak", "açılsın", "başlatılsın", "devreye alınacak", "planlandı", "seçildi", "uygulanacak" };
            var riskKeywords = new[] { "risk", "sorun", "gecikme", "hata", "kesinti", "yetişmeyebilir", "problem", "engel", "eksik", "yoğunluk", "kritik", "acil", "aksama" };
            var deadlineKeywords = new[] { "bugün", "yarın", "cuma", "pazartesi", "salı", "çarşamba", "perşembe", "bu hafta", "haftaya", "hafta sonuna kadar", "ay sonuna kadar", "öğlene kadar", "akşama kadar" };
            var excludeNames = new[] { "MeetFlow", "AI", "Dashboard", "API" };

            // Orijinal metinden katılımcıları bulma (Büyük harfle başlayan isimler, veya "İsim:" formatı)
            var nameRegex = new Regex(@"\b[A-ZÇĞİÖŞÜ][a-zçğıöşü]+\b");
            var matches = nameRegex.Matches(meetingText);
            foreach (Match match in matches)
            {
                var name = match.Value;
                if (!excludeNames.Contains(name) && name.Length > 2 && !result.Participants.Contains(name))
                {
                    // Basit bir heuristics: İlk kelimenin katılımcı olma ihtimali
                    result.Participants.Add(name);
                }
            }

            foreach (var fragment in fragments)
            {
                var lowerFragment = fragment.ToLowerInvariant();

                // Görev Tespiti
                if (taskKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    // Doğal cümle formatı
                    var taskSentence = char.ToUpperInvariant(fragment[0]) + fragment.Substring(1);
                    if (!taskSentence.EndsWith(".")) taskSentence += ".";
                    
                    // "tamamlasın" vb. kelimeleri "tamamlayacak" şeklinde düzeltebiliriz ama test case'lerinde istenilen çıktıya göre formatlayalım
                    if(lowerFragment.Contains("tamamlasın")) taskSentence = taskSentence.Replace("tamamlasın", "tamamlayacak");
                    if(lowerFragment.Contains("halletsin")) taskSentence = taskSentence.Replace("halletsin", "halledecek");

                    result.ActionItems.Add(taskSentence);
                }

                // Karar Tespiti
                if (decisionKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    var decisionSentence = char.ToUpperInvariant(fragment[0]) + fragment.Substring(1);
                    if (!decisionSentence.EndsWith(".")) decisionSentence += ".";
                    
                    if(lowerFragment.Contains("açılsın")) decisionSentence = decisionSentence.Replace("açılsın", "açılmasına karar verildi");

                    result.Decisions.Add(decisionSentence);
                }

                // Risk Tespiti
                if (riskKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    result.Risks.Add(fragment);
                }

                // Deadline Tespiti
                foreach (var deadline in deadlineKeywords)
                {
                    if (lowerFragment.Contains(deadline) && !result.Deadlines.Contains(char.ToUpperInvariant(deadline[0]) + deadline.Substring(1)))
                    {
                        result.Deadlines.Add(char.ToUpperInvariant(deadline[0]) + deadline.Substring(1));
                    }
                }
            }

            // Fallback'ler
            if (result.Decisions.Count == 0)
                result.Decisions.Add("Belirgin bir karar tespit edilmedi.");
            
            if (result.Risks.Count == 0)
                result.Risks.Add("Şu an için belirgin bir risk tespit edilmedi.");

            // Summary Üretimi
            var partCount = result.Participants.Count;
            var partNames = partCount > 0 ? string.Join(" ve ", result.Participants.Take(2)) + (partCount > 2 ? " vd." : "") : "Ekip";
            
            var taskCount = result.ActionItems.Count;
            var decCount = result.Decisions.Count(d => d != "Belirgin bir karar tespit edilmedi.");
            var deadlineCount = result.Deadlines.Count;
            
            result.Summary = $"{partNames}'nin katıldığı toplantıda genel durum ve aksiyonlar ele alındı. Toplantıdan {taskCount} görev, {decCount} karar ve {deadlineCount} deadline çıkarıldı.";

            // Follow Up
            if (result.Risks.Any(r => r != "Şu an için belirgin bir risk tespit edilmedi."))
                result.FollowUpSuggestion = "Riskli başlıklar için kısa süre içinde takip toplantısı yapılması önerilir.";
            else if (result.Deadlines.Count > 0)
                result.FollowUpSuggestion = "Deadline yaklaşmadan önce görev durumlarının kontrol edilmesi önerilir.";
            else if (result.ActionItems.Count > 0)
                result.FollowUpSuggestion = "Görevlerin sorumlularla birlikte takip edilmesi önerilir.";
            else
                result.FollowUpSuggestion = "Toplantı çıktılarının netleştirilmesi için ek aksiyon planı hazırlanması önerilir.";

            // Zaman Hesaplamaları
            var wordCount = meetingText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            result.ManualTimeMinutes = 30 + (wordCount / 100) * 5;
            if (taskCount > 3) result.ManualTimeMinutes += 2;
            if (result.Risks.Any(r => r != "Şu an için belirgin bir risk tespit edilmedi.")) result.ManualTimeMinutes += 3;

            result.AiTimeMinutes = Math.Min(12, Math.Max(5, wordCount / 50));
            result.SavedTimeMinutes = result.ManualTimeMinutes - result.AiTimeMinutes;
            
            double eff = ((double)result.SavedTimeMinutes / result.ManualTimeMinutes) * 100;
            result.EfficiencyGainPercentage = (int)Math.Max(50, Math.Min(95, eff));

            // Risk Score
            result.RiskScore = 0;
            if (result.Risks.Any(r => r != "Şu an için belirgin bir risk tespit edilmedi."))
            {
                result.RiskScore += 25;
                result.RiskScore += result.Risks.Count * 10;
                if (meetingText.ToLowerInvariant().Contains("acil") || meetingText.ToLowerInvariant().Contains("kritik") || meetingText.ToLowerInvariant().Contains("kesinti"))
                    result.RiskScore += 20;
                if (result.Deadlines.Count > 0)
                    result.RiskScore += 15;
            }
            else
            {
                result.RiskScore = 15;
            }
            result.RiskScore = Math.Min(100, result.RiskScore);

            // Quality Score
            result.MeetingQualityScore = 40;
            if (taskCount > 0) result.MeetingQualityScore += 20;
            if (decCount > 0) result.MeetingQualityScore += 20;
            else result.MeetingQualityScore -= 10;
            
            if (result.Deadlines.Count > 0) result.MeetingQualityScore += 20;
            else result.MeetingQualityScore -= 10;
            
            if (partCount > 0) result.MeetingQualityScore += 20;
            if (result.Risks.Any(r => r != "Şu an için belirgin bir risk tespit edilmedi.")) result.MeetingQualityScore += 10;
            if (!string.IsNullOrEmpty(result.FollowUpSuggestion)) result.MeetingQualityScore += 10;

            result.MeetingQualityScore = Math.Max(0, Math.Min(100, result.MeetingQualityScore));

            // Workload
            foreach (var task in result.ActionItems)
            {
                string owner = "Belirsiz";
                foreach (var participant in result.Participants)
                {
                    if (task.Contains(participant))
                    {
                        owner = participant;
                        break;
                    }
                }
                
                if (result.WorkloadDistribution.ContainsKey(owner))
                    result.WorkloadDistribution[owner]++;
                else
                    result.WorkloadDistribution[owner] = 1;
            }

            return Task.FromResult(result);
        }
    }
}
