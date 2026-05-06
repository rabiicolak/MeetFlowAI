# MeetFlow AI

MeetFlow AI, toplantı notlarınızı ve ses kayıtlarınızı saniyeler içinde analiz eden, aksiyon maddelerini çıkaran ve profesyonel raporlar üreten yapay zeka destekli bir platformdur.

## Problem
Toplantı sonrası notları toparlamak, aksiyonları belirlemek ve ilgili kişilere raporlamak ciddi bir zaman kaybıdır. Manuel süreçler verimliliği düşürmekte ve iletişimsizlik riskini artırmaktadır.

## Çözüm
MeetFlow AI, toplantı metinlerini ve ses kayıtlarını yapay zeka ile analiz ederek;
- Aksiyon maddeleri çıkarır
- Kararları özetler
- Risk ve verimlilik skorları oluşturur
- Otomatik formatlarda raporlar hazırlar ve mail ile paylaşır.

## Özellikler
- **Metin Analizi:** Yazılı toplantı notlarını analiz eder.
- **Ses Analizi:** Ses kayıtlarından transkript oluşturur ve içgörü çıkarır.
- **Rapor Export:** HTML, Markdown, TXT formatlarında rapor indirme.
- **Mail Paylaşımı:** Analiz raporlarını tek tıkla e-posta olarak gönderir.
- **Dashboard:** Toplam analizler, ses analizleri, zaman tasarrufu ve raporlama metriklerini grafiklerle görselleştirir.

## Kurulum
1. Repoyu klonlayın.
2. `src/MeetFlow.Web` dizinine gidin.
3. `dotnet build` komutu ile derleyin.
4. `dotnet run` komutu ile uygulamayı başlatın.
5. Tarayıcıda `https://localhost:5001` (veya `http://localhost:5000`) adresine gidin.

## GitHub Workflow
Projede Plan Agent ve Skills Agent ile AI destekli bir geliştirme süreci yürütülmüştür. Issue ve PR süreçlerinde AI Traceability (Yapay Zeka İzlenebilirliği) kurallarına uyulmaktadır.
