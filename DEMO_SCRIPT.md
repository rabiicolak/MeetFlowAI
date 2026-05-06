# MeetFlow AI - Demo Script

Demo sırasında izlenecek adımlar:

1. **Dashboard Gösterimi**
   - Menüden "Dashboard" sayfasına geçiş.
   - İstatistik kartları (Zaman tasarrufu %50+ vb.).
   - Chart.js grafikleri ve son toplantılar tablosunun tanıtılması.
   - AI İçgörüleri panelinin gösterimi.

2. **Ana Sayfada Metin Analizi**
   - Ana sayfaya dönüş.
   - "Metin Analizi" tabı seçili iken mock toplantı notu (Örn: Sprint Planlama) girilir.
   - "Analiz Et" butonuna tıklanır ve gelen sonuçlardaki Aksiyon, Karar ve Risk Skorları gösterilir.

3. **Ses Analizi**
   - "Ses Analizi" tabına geçilir.
   - Dosya seçmeden (mock olarak) analize başlanır.
   - Ses transkripti ve sonrasındaki detaylı analizin gösterimi.

4. **Rapor Kopyalama ve Export**
   - Çıkan analizin altındaki Markdown/HTML/TXT export butonlarına tıklanarak raporun panoya kopyalandığı (Toast bildirimi ile) gösterilir.

5. **Mail Gönderme**
   - "Mail ile Gönder" butonuna tıklanarak, raporun mail formatında açıldığı gösterilir.

6. **GitHub Issues/PR/AI Traceability Gösterimi**
   - Kod editöründe `AI_TRACEABILITY.md` veya kaynak kodlardaki (örn: DashboardController) `// AI Plan Agent` yorum satırları gösterilerek sürecin nasıl yönetildiği anlatılır.
