using System.Text;
using MeetFlow.Web.ViewModels;

namespace MeetFlow.Web.Services
{
    // AI-Augmented Reporting Workflow: Analiz çıktılarından otomatik rapor üretim altyapısı hazırlanmıştır.
    public class MockReportExportService : IReportExportService
    {
        public string GeneratePlainTextReport(MeetingAnalysisResultViewModel analysis)
        {
            var sb = new StringBuilder();
            sb.AppendLine("MEETFLOW AI - TOPLANTI RAPORU");
            sb.AppendLine("===============================");
            sb.AppendLine($"ÖZET:\n{analysis.Summary}\n");

            sb.AppendLine("KARARLAR:");
            foreach (var item in analysis.Decisions) sb.AppendLine($"- {item}");
            sb.AppendLine();

            sb.AppendLine("GÖREVLER:");
            foreach (var item in analysis.Tasks) sb.AppendLine($"- {item.Title}");
            sb.AppendLine();

            sb.AppendLine("RİSKLER:");
            foreach (var item in analysis.Risks) sb.AppendLine($"- {item}");
            sb.AppendLine();

            sb.AppendLine("DEADLINE'LAR:");
            foreach (var item in analysis.Deadlines) sb.AppendLine($"- {item}");
            sb.AppendLine();

            sb.AppendLine("KATILIMCILAR:");
            sb.AppendLine(string.Join(", ", analysis.Participants));
            sb.AppendLine();

            sb.AppendLine($"KALİTE SKORU: {analysis.QualityScore}/100");
            sb.AppendLine($"RİSK SKORU: {analysis.RiskScore}/100");
            sb.AppendLine();

            sb.AppendLine($"AI ÖNERİSİ:\n{analysis.NextStep}");

            return sb.ToString();
        }

        public string GenerateMarkdownReport(MeetingAnalysisResultViewModel analysis)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# MeetFlow AI - Toplantı Raporu\n");
            
            sb.AppendLine("## 📝 Özet");
            sb.AppendLine($"{analysis.Summary}\n");

            sb.AppendLine("## ⚖️ Kararlar");
            foreach (var item in analysis.Decisions) sb.AppendLine($"- {item}");
            sb.AppendLine();

            sb.AppendLine("## 📋 Görevler");
            foreach (var item in analysis.Tasks) sb.AppendLine($"- [ ] {item.Title} (Sorumlu: {item.Assignee})");
            sb.AppendLine();

            sb.AppendLine("## ⚠️ Riskler");
            foreach (var item in analysis.Risks) sb.AppendLine($"- **{item}**");
            sb.AppendLine();

            sb.AppendLine("## ⏰ Deadline'lar");
            foreach (var item in analysis.Deadlines) sb.AppendLine($"- 📅 {item}");
            sb.AppendLine();

            sb.AppendLine("## 👥 Katılımcılar");
            sb.AppendLine(string.Join(", ", analysis.Participants) + "\n");

            sb.AppendLine("## 📊 Skorlar");
            sb.AppendLine($"- **Kalite Skoru:** {analysis.QualityScore}/100");
            sb.AppendLine($"- **Risk Skoru:** {analysis.RiskScore}/100\n");

            sb.AppendLine("## 💡 AI Önerisi");
            sb.AppendLine($"> {analysis.NextStep}");

            return sb.ToString();
        }

        public string GenerateHtmlReport(MeetingAnalysisResultViewModel analysis)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'><title>MeetFlow AI Toplantı Raporu</title>");
            sb.AppendLine("<style>body{font-family:Arial,sans-serif;line-height:1.6;color:#333;margin:20px;} h1,h2{color:#2c3e50;} .card{border:1px solid #ddd;padding:15px;margin-bottom:20px;border-radius:5px;} .risk{color:#e74c3c;} .task{color:#3498db;}</style>");
            sb.AppendLine("</head><body>");
            
            sb.AppendLine("<h1>MeetFlow AI Toplantı Raporu</h1>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<h2>Özet</h2>");
            sb.AppendLine($"<p>{analysis.Summary}</p>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<h2>Kararlar</h2><ul>");
            foreach (var item in analysis.Decisions) sb.AppendLine($"<li>{item}</li>");
            sb.AppendLine("</ul></div>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<h2>Görevler</h2><ul>");
            foreach (var item in analysis.Tasks) sb.AppendLine($"<li class='task'>{item.Title} ({item.Assignee})</li>");
            sb.AppendLine("</ul></div>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<h2>Riskler</h2><ul>");
            foreach (var item in analysis.Risks) sb.AppendLine($"<li class='risk'>{item}</li>");
            sb.AppendLine("</ul></div>");

            sb.AppendLine("<div class='card'>");
            sb.AppendLine("<h2>Metrikler</h2>");
            sb.AppendLine($"<ul><li>Kalite Skoru: {analysis.QualityScore}/100</li>");
            sb.AppendLine($"<li>Risk Skoru: {analysis.RiskScore}/100</li></ul>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='card' style='background-color:#f9f9f9;'>");
            sb.AppendLine("<h2>AI Önerisi</h2>");
            sb.AppendLine($"<p><i>{analysis.NextStep}</i></p>");
            sb.AppendLine("</div>");

            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}
