# AI Traceability (Yapay Zeka İzlenebilirliği)

MeetFlow AI geliştirme sürecinde yapay zeka ajanları aktif bir şekilde kullanılmıştır. Bu süreçte hangi ajanın hangi aşamada katkı sağladığı aşağıda listelenmiştir.

## Plan Agent Kullanımı
- Proje mimarisinin ve dosya yapısının oluşturulması.
- Metin ve ses analizi pipeline'ının tasarlanması.
- Dashboard metriklerinin ve "zaman tasarrufu" odaklı özellik setinin planlanması.
- Issue'ların oluşturulması ve PR'ların gözden geçirilmesi süreçlerinin tasarlanması.

## Skills Agent Kullanımı
- **Backend:** Mock servislerin, controller logic'lerinin ve iş kurallarının kodlanması.
- **Frontend:** Bootstrap 5 ile UI komponentlerinin, kartların ve formların responsive şekilde entegrasyonu.
- **Dashboard:** Chart.js kullanılarak grafiklerin (Line, Bar, Doughnut, Pie) oluşturulması, mock verilerin Controller'dan View'a aktarılması.

## PR ve Issue Süreci
Projeye eklenen her yeni özellik için öncelikle Issue açılır ve planlama yapılır. Yazılan kodlarda, ilgili değişiklikleri ve nedenlerini belirten `// AI Plan Agent:` veya `<!-- AI Plan Agent ... -->` etiketli yorumlar bırakılarak şeffaflık sağlanır. Tüm PR'lar AI Traceability şablonu üzerinden geçer.
