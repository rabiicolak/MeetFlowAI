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

            // 4. İki kelimeli kişi adlarını yakala ve katılımcı listesini doldur
            var knownNames = new[] { 
                "yeşim ayma", "rabia çorak", "esmanur", "esma", "rabia", 
                "hatice", "arda", "mehmet", "ali", "ayşe", "ahmet", "yeşim" 
            };

            foreach (var name in knownNames)
            {
                if (lowerFragmentMatcher(lowerText, name))
                {
                    var normalized = CapitalizeName(name);
                    if (!result.Participants.Contains(normalized))
                    {
                        result.Participants.Add(normalized);
                    }
                }
            }

            // 5. & 6. Virgül ve bağlaçlarla ayırarak her parçayı ayrı görev olarak değerlendir
            var separators = new[] { ".", ",", ";", "\n", " ve ", " ardından ", " sonra " };
            var fragments = meetingText.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(f => f.Trim())
                                       .Where(f => f.Length > 0)
                                       .ToList();

            // 2. Emir kiplerini görev olarak algıla
            var taskVerbs = new Dictionary<string, string> 
            {
                { "yaz", "yazacak" },
                { "gönder", "gönderecek" },
                { "hallet", "halledecek" },
                { "tamamla", "tamamlayacak" },
                { "hazırla", "hazırlayacak" },
                { "kontrol et", "kontrol edecek" },
                { "test et", "test edecek" },
                { "ilgilen", "ilgilenecek" },
                { "başlat", "başlatacak" }
            };

            var deadlineKeywords = new[] { "yarın", "bugün", "cuma", "pazartesi", "bu hafta", "ay sonuna kadar" };
            var riskKeywords = new[] { "risk", "sorun", "hata", "gecikme", "yetişmeyebilir", "problem", "kesinti" };

            foreach (var fragment in fragments)
            {
                var lowerFragment = fragment.ToLowerInvariant();

                // Görev ve Karar Çıkarımı
                var foundVerb = taskVerbs.FirstOrDefault(v => lowerFragment.EndsWith(v.Key) || lowerFragment.Contains(v.Key + " "));
                if (foundVerb.Key != null)
                {
                    string assignee = "Belirsiz";
                    foreach (var name in knownNames)
                    {
                        if (lowerFragment.Contains(name))
                        {
                            assignee = CapitalizeName(name);
                            if (!result.Participants.Contains(assignee))
                                result.Participants.Add(assignee);
                            break;
                        }
                    }

                    var actionText = lowerFragment;
                    if (assignee != "Belirsiz")
                    {
                        actionText = actionText.Replace(assignee.ToLowerInvariant(), "").Trim();
                    }

                    // 3. İsim + iş + fiil kalıbını yakala
                    actionText = ReplaceLastOccurrence(actionText, foundVerb.Key, foundVerb.Value).Trim();

                    // Özel case düzeltmesi (Esmanur kendi hallet -> kendi görevini halledecek)
                    if (actionText.Contains("kendi halledecek")) 
                    {
                        actionText = actionText.Replace("kendi halledecek", "kendi görevini halledecek");
                    }

                    // Görev oluştur (7. Görevleri tek cümlede birleştirme; her görev ayrı liste elemanı olsun)
                    var taskTitle = "";
                    if (assignee != "Belirsiz")
                    {
                        taskTitle = $"{assignee} {actionText}.";
                    }
                    else
                    {
                        taskTitle = char.ToUpperInvariant(actionText[0]) + actionText.Substring(1) + ".";
                    }

                    var tDeadline = "Belirsiz";
                    foreach (var dl in deadlineKeywords)
                    {
                        if (lowerFragment.Contains(dl))
                        {
                            tDeadline = char.ToUpperInvariant(dl[0]) + dl.Substring(1);
                            if (!result.Deadlines.Contains(tDeadline)) result.Deadlines.Add(tDeadline);
                            break;
                        }
                    }

                    var tPriority = lowerFragment.Contains("önemli") || lowerFragment.Contains("acil") ? "Yüksek" : "Normal";

                    if (!result.Tasks.Any(t => t.Title == taskTitle))
                    {
                        result.Tasks.Add(new TaskItemViewModel 
                        { 
                            Title = taskTitle, 
                            Assignee = assignee, 
                            Deadline = tDeadline, 
                            Priority = tPriority 
                        });
                    }

                    // 10. Karar kısmında görevlerden türetilmiş kısa kararlar üret
                    var decisionStr = actionText.Replace("ları ", "ların ").Replace("leri ", "lerin ");
                    
                    if (foundVerb.Key == "yaz") decisionStr = decisionStr.Replace("yazacak", "yazılması kararlaştırıldı");
                    else if (foundVerb.Key == "gönder") decisionStr = decisionStr.Replace("gönderecek", "gönderilmesi kararlaştırıldı");
                    else if (foundVerb.Key == "hallet") decisionStr = decisionStr.Replace("halledecek", "halledilmesi kararlaştırıldı");
                    else if (foundVerb.Key == "tamamla") decisionStr = decisionStr.Replace("tamamlayacak", "tamamlanması kararlaştırıldı");
                    else if (foundVerb.Key == "hazırla") decisionStr = decisionStr.Replace("hazırlayacak", "hazırlanması kararlaştırıldı");
                    else if (foundVerb.Key == "kontrol et") decisionStr = decisionStr.Replace("kontrol edecek", "kontrol edilmesi kararlaştırıldı");
                    else if (foundVerb.Key == "test et") decisionStr = decisionStr.Replace("test edecek", "test edilmesi kararlaştırıldı");
                    else if (foundVerb.Key == "ilgilen") decisionStr = decisionStr.Replace("ilgilenecek", "ilgilenilmesi kararlaştırıldı");
                    else if (foundVerb.Key == "başlat") decisionStr = decisionStr.Replace("başlatacak", "başlatılması kararlaştırıldı");

                    if (!string.IsNullOrEmpty(decisionStr))
                    {
                        decisionStr = char.ToUpperInvariant(decisionStr[0]) + decisionStr.Substring(1) + ".";
                        if (!result.Decisions.Contains(decisionStr))
                            result.Decisions.Add(decisionStr);
                    }
                }

                // Risk Tespiti
                if (riskKeywords.Any(k => lowerFragment.Contains(k)))
                {
                    var riskSentence = char.ToUpperInvariant(fragment[0]) + fragment.Substring(1);
                    if (!riskSentence.EndsWith(".")) riskSentence += ".";
                    result.Risks.Add(riskSentence);
                }
            }

            // Summary, NextStep ve Sabit Değerler
            result.QualityScore = 90;
            result.RiskScore = result.Risks.Count > 0 ? 5 : 0;
            result.WorkloadStatus = result.Tasks.Count > 2 ? "Yoğun" : (result.Tasks.Count > 0 ? "Dengeli" : "Boş");
            
            // 9. Eğer görev varsa "Belirgin görev yok" yazma
            if (result.Tasks.Count == 0 && result.Decisions.Count == 0 && result.Risks.Count == 0)
            {
                result.Summary = "Bu metinde belirgin görev, karar veya risk tespit edilemedi.";
                result.NextStep = "Belirgin bir sonraki adım bulunmuyor.";
            }
            else
            {
                result.Summary = $"Toplantıda görev dağılımı yapıldı. Toplam {result.Tasks.Count} görev ve {result.Decisions.Count} karar çıkarıldı.";
                result.NextStep = result.Tasks.Count > 0 ? "Görevlerin tamamlanması takip edilecek." : "Değerlendirme toplantısı planlanacak.";
            }

            return Task.FromResult(result);
        }

        private bool lowerFragmentMatcher(string text, string search)
        {
            return text.Contains(search);
        }

        private string CapitalizeName(string name)
        {
            return string.Join(" ", name.Split(' ').Select(w => char.ToUpperInvariant(w[0]) + w.Substring(1)));
        }

        private string ReplaceLastOccurrence(string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);
            if (place == -1)
                return source;
            return source.Remove(place, find.Length).Insert(place, replace);
        }
    }
}
