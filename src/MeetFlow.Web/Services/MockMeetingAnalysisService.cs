using System.Text.RegularExpressions;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    public class MockMeetingAnalysisService : IMeetingAnalysisService
    {
        public Task<MeetingAnalysisResultViewModel> AnalyzeAsync(string meetingText)
        {
            var result = new MeetingAnalysisResultViewModel();

            if (string.IsNullOrWhiteSpace(meetingText))
                return Task.FromResult(result);

            var lowerText = meetingText.ToLowerInvariant();

            // Katılımcı Çıkarımı
            var nameRegex = new Regex(@"\b([a-zçğıöşü]+)\b", RegexOptions.IgnoreCase);
            var possibleNames = new[] { "esma", "rabia", "hatice", "arda", "mehmet", "ali", "ayşe", "ahmet" };
            
            var matches = nameRegex.Matches(lowerText);
            foreach (Match match in matches)
            {
                var word = match.Groups[1].Value;
                if (possibleNames.Contains(word))
                {
                    var normalized = char.ToUpperInvariant(word[0]) + word.Substring(1);
                    if (!result.Participants.Contains(normalized))
                    {
                        result.Participants.Add(normalized);
                    }
                }
            }

            var separators = new[] { ".", ",", ";", "\n", " ve ", " ama ", " fakat ", " ayrıca " };
            var fragments = meetingText.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(f => f.Trim())
                                       .Where(f => f.Length > 0)
                                       .ToList();

            var taskVerbs = new[] { "doldur", "doldursun", "hazırla", "hazırlasın", "hallet", "yap", "yapsın", "tamamla", "tamamlasın" };
            var decisionKeywords = new[] { "karar", "onaylandı", "kabul edildi", "başlatılsın", "açılsın", "kullanılacak", "canlıya alınacak" };
            var riskKeywords = new[] { "risk", "sorun", "hata", "gecikme", "yetişmeyebilir", "problem", "kesinti" };
            var deadlineKeywords = new[] { "yarın", "bugün", "cuma", "pazartesi", "bu hafta", "ay sonuna kadar" };

            foreach (var fragment in fragments)
            {
                var lowerFragment = fragment.ToLowerInvariant();

                // Görev Çıkarımı
                foreach (var participant in result.Participants)
                {
                    if (lowerFragment.Contains(participant.ToLowerInvariant()))
                    {
                        foreach (var verb in taskVerbs)
                        {
                            if (lowerFragment.Contains(verb))
                            {
                                var taskTitle = char.ToUpperInvariant(verb[0]) + verb.Substring(1) + " işini tamamla";
                                if (lowerFragment.Contains("rapor")) taskTitle = "Raporları doldur";
                                else if (lowerFragment.Contains("sunum")) taskTitle = "Sunumu hazırla";
                                else if (lowerFragment.Contains("finans")) taskTitle = "Finansal işleri hallet";

                                var tDeadline = "Belirsiz";
                                foreach (var dl in deadlineKeywords)
                                {
                                    if (lowerFragment.Contains(dl))
                                    {
                                        tDeadline = char.ToUpperInvariant(dl[0]) + dl.Substring(1);
                                        break;
                                    }
                                }

                                var tPriority = lowerFragment.Contains("önemli") || lowerFragment.Contains("acil") ? "Yüksek" : "Normal";

                                if (!result.Tasks.Any(t => t.Title == taskTitle && t.Assignee == participant))
                                {
                                    result.Tasks.Add(new TaskItemViewModel 
                                    { 
                                        Title = taskTitle, 
                                        Assignee = participant, 
                                        Deadline = tDeadline, 
                                        Priority = tPriority 
                                    });
                                }
                            }
                        }
                    }
                }

                // Karar Tespiti
                if (decisionKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    var decisionSentence = char.ToUpperInvariant(fragment[0]) + fragment.Substring(1);
                    if (!decisionSentence.EndsWith(".")) decisionSentence += ".";
                    result.Decisions.Add(decisionSentence);
                }

                // Risk Tespiti
                if (riskKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    var riskSentence = char.ToUpperInvariant(fragment[0]) + fragment.Substring(1);
                    if (!riskSentence.EndsWith(".")) riskSentence += ".";
                    result.Risks.Add(riskSentence);
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

            // Summary, NextStep ve Sabit Değerler
            result.QualityScore = 85;
            result.RiskScore = result.Risks.Count > 0 ? 4 : 0;
            result.WorkloadStatus = result.Tasks.Count > 2 ? "Çok Yoğun" : (result.Tasks.Count > 0 ? "Normal" : "Boş");
            
            if (result.Tasks.Count == 0 && result.Decisions.Count == 0 && result.Risks.Count == 0)
            {
                result.Summary = "Bu metinde belirgin görev, karar veya risk tespit edilemedi.";
                result.NextStep = "Belirgin bir sonraki adım bulunmuyor.";
            }
            else
            {
                result.Summary = $"Toplantı notunda {result.Tasks.Count} görev ve {result.Deadlines.Count} deadline çıkarılmıştır.";
                if (result.Participants.Contains("Esma") && result.Tasks.Any(t => t.Title.Contains("Rapor")))
                {
                     result.Summary = "Toplantı notunda yarına kadar Esma’nın raporları doldurması gerektiği belirtilmiştir.";
                }
                result.NextStep = result.Tasks.Count > 0 ? "Görevlerin tamamlanması takip edilecek." : "Değerlendirme toplantısı planlanacak.";
            }

            return Task.FromResult(result);
        }
    }
}
