# 🛡️ Basit Antivirüs Projesi (C# .NET)

Bu proje, **Nesne Tabanlı Programlama / Görsel Programlama** dersi kapsamında geliştirilmiş, imza tabanlı (signature-based) çalışan bir masaüstü antivirüs uygulamasıdır.

**Hazırlayan:** Oğuzhan Yıldırım  
**Öğrenci No:** 240541029

## 🚀 Proje Hakkında
Uygulama, seçilen bir dizindeki dosyaları tarar, her dosyanın **MD5 hash** (parmak izi) değerini hesaplar ve bu değeri bilinen zararlı yazılım veritabanı ile karşılaştırır. Eşleşme bulunursa kullanıcıyı uyarır.

## ⚙️ Özellikler

* **📂 Klasör Tarama:** Belirlenen klasör ve alt klasörlerdeki tüm dosyaları analiz eder.
* **🔍 MD5 İmza Kontrolü:** Dosyaların benzersiz hash değerlerini çıkarır.
* **🦠 Virüs Tespiti:** Önceden tanımlanmış "Virüs Veritabanı" ile eşleşen dosyaları tespit eder.
* **🛡️ Karantina & Silme:** Zararlı dosyaları sistemden uzaklaştırır veya siler.
* **🧪 Test Modu:** Sistemin çalışıp çalışmadığını denemek için yapay bir virüs dosyası (Fake Virus) oluşturma özelliği.
* **📊 Canlı İlerleme:** Tarama durumunu gösteren ilerleme çubuğu (ProgressBar) ve log ekranı.
* **🧵 Asenkron Çalışma:** Tarama sırasında arayüz donmaz (BackgroundWorker/Task yapısı).

## 🛠️ Kullanılan Teknolojiler

* **Dil:** C#
* **Platform:** .NET Framework 4.7.2 (veya üzeri)
* **Arayüz:** Windows Forms (WinForms)
* **IDE:** Visual Studio 2022


---

## 💻 Kurulum ve Çalıştırma

1. Projeyi bilgisayarınıza indirin veya klonlayın:
   ```bash
   git clone [https://github.com/oguzyl11/Virus-Antivirus.git](https://github.com/oguzyl11/Virus-Antivirus.git)
