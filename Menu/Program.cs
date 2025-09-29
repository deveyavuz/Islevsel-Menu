using System.Text.Json;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

while (true)
{
    Console.WriteLine("\n=== Menü V1'e hoşgeldiniz ===");
    Console.WriteLine("1. Hesap Makinesi");
    Console.WriteLine("2. Sayı Tahmin Oyunu");
    Console.WriteLine("3. VKİ Hesaplama");
    Console.WriteLine("4. Not Ortalaması Hesaplama");
    Console.WriteLine("5. Dolar - TL Çevirme");
    Console.WriteLine("6. Saat Dönüştürme");
    Console.WriteLine("7. Burç Bulma");
    Console.WriteLine("8. Mini ATM");
    Console.WriteLine("0. Çıkış");
    Console.Write("Bir seçenek girin (1-8): ");
    string secim = Console.ReadLine();
    if (secim == "1")
    {
        while (true)
        {
            Console.WriteLine("\n=== Hesap Makinesi ===");
            Console.WriteLine("1. Toplama");
            Console.WriteLine("2. Çıkarma");
            Console.WriteLine("3. Çarpma");
            Console.WriteLine("4. Bölme");
            Console.Write("Bir işlem seçin (1-4): ");
            string islem = Console.ReadLine();
            Console.Write("Birinci sayıyı girin: ");
            double sayi1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("İkinci sayıyı girin: ");
            double sayi2 = Convert.ToDouble(Console.ReadLine());
            double sonuc = islem switch
            {
                "1" => sayi1 + sayi2,
                "2" => sayi1 - sayi2,
                "3" => sayi1 * sayi2,
                "4" => sayi1 / sayi2,
            };
            Console.WriteLine($"\nSonuç: {sonuc}");
            break;

        }





        Console.ReadKey();
    }
    else if (secim == "2")
    {
        Console.WriteLine("=== Sayı tahmin oyunu ===");
        Random rnd = new();
        int PCsayi = rnd.Next(1, 101);
        while (true)
        {
            Console.Write("\nTahmininizi girin (1-100): ");
            int kullanicisayi = Convert.ToInt32(Console.ReadLine());
            string sonuc = kullanicisayi switch
            {
                _ when kullanicisayi < PCsayi => "Daha yüksek bir sayı girin.",
                _ when kullanicisayi > PCsayi => "Daha düşük bir sayı girin.",
                _ when kullanicisayi == PCsayi => "Tebrikler! Doğru tahmin ettiniz.",
                _ => "Geçersiz sayı"
            };
            Console.WriteLine(sonuc);

            if (kullanicisayi == PCsayi)
                break;
        }

        Console.ReadKey();
    }
    else if (secim == "3")
    {
        Console.WriteLine("=== VKİ Hesaplama ===");
        Console.Write("Lütfen kilonuzu (kg) girin (örn=78,5): ");
        double kilo = Convert.ToDouble(Console.ReadLine());
        Console.Write("Lütfen boyunuzu (m veya cm) girin (örn=1,75): ");
        double boy = Convert.ToDouble(Console.ReadLine());

        double vki = kilo / (boy * boy);

        string sonuc = vki switch
        {
            < 18.5 => "Zayıf",
            >= 18.5 and <= 24.9 => "Normal (Sağlıklı)",
            >= 25 and <= 29.9 => "Fazla Kilolu",
            >= 30 and <= 34.9 => "Obez (1. derece)",
            >= 35 and <= 39.9 => "Obez (2. derece)",
            >= 40 => "Morbid Obez (3. derece)",
            _ => "Dinazor"
        };
        Console.WriteLine($"Vücut Kitle İndeksiniz: {Math.Round(vki, 2)} yani {sonuc}");
        Console.ReadKey();
        break;

    }
    else if (secim == "4")
    {
        Console.WriteLine("=== Not ortalama hesaplama ===");
        Console.Write("\nBirinci notu girin: ");
        double not1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("\nİkinci notu girin: ");
        double not2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("\nBirinci sözlüyü girin: ");
        double not3 = Convert.ToDouble(Console.ReadLine());
        Console.Write("\nİkinci sözlüyü girin: ");
        double not4 = Convert.ToDouble(Console.ReadLine());
        double ortalama = (not1 + not2 + not3 + not4) / 4;
        string sonuc;
        if (ortalama < 50)
        {
            sonuc = "Kaldınız.";
        }
        else
        {
            sonuc = "Geçtiniz.";
        }
        Console.WriteLine($"\nNot ortalamanız: {Math.Round(ortalama, 2)}, yani {sonuc}");
        Console.ReadKey();
        break;
    }
    else if (secim == "5")
    {
        string Dolarurl = "https://api.exchangerate-api.com/v4/latest/USD";
        string Eurourl = "https://api.exchangerate-api.com/v4/latest/EUR";

        using HttpClient client = new();
        string jsonDolar = await client.GetStringAsync(Dolarurl);

        string jsonEuro = await client.GetStringAsync(Eurourl);

        var dolarData = JsonDocument.Parse(jsonDolar);
        var euroData = JsonDocument.Parse(jsonEuro);
        double Dolarkur = dolarData.RootElement
            .GetProperty("rates")
            .GetProperty("TRY")
            .GetDouble();
        double Eurokur = euroData.RootElement
            .GetProperty("rates")
            .GetProperty("TRY")
            .GetDouble();

        Console.WriteLine("=== Kur Çevirici ===");
        Console.WriteLine("\n1 - Dolar - TL");
        Console.WriteLine("2 - Euro - TL");
        Console.WriteLine("\n3 - TL - Dolar");
        Console.WriteLine("4 - TL - Euro");
        Console.Write("Ne hesaplamak istiyorsunuz: ");
        string kurSecim = Console.ReadLine();
        if (secim == "1")
        {
            Console.Write("Kaç Dolar bozdurmak istiyorsunuz: ");
            double dolar = Convert.ToDouble(Console.ReadLine());
            double tl = dolar * Dolarkur;
            Console.WriteLine($"{dolar} Dolar = {tl} TL");
        }
        else if (kurSecim == "2")
        {
            Console.Write("Kaç Euro bozdurmak istiyorsunuz: ");
            double euro = Convert.ToDouble(Console.ReadLine());
            double tl = euro * Eurokur;
            Console.WriteLine($"{euro} Euro = {tl} TL");
        }
        else if (kurSecim == "3")
        {
            Console.Write("Kaç TL bozdurmak istiyorsunuz: ");
            double tl = Convert.ToDouble(Console.ReadLine());
            double dolar = tl / Dolarkur;
            Console.WriteLine($"{tl} TL = {dolar} Dolar");
        }
        else if (kurSecim == "4")
        {
            Console.Write("Kaç TL bozdurmak istiyorsunuz: ");
            double tl = Convert.ToDouble(Console.ReadLine());
            double euro = tl / Eurokur;
            Console.WriteLine($"{tl} TL = {euro} Euro");

        }
        else
        {
            Console.WriteLine("Geçersiz seçim.");

        }
        Console.ReadKey();


    }
    else if (secim == "6")
    {
        Console.WriteLine("=== Dakika - Saat dönüştürme ===");
        Console.WriteLine("\n1 - Dakika - Saat");
        Console.WriteLine("2 - Saat - Dakika");
        Console.Write("Seçim: ");
        string saatSecim = Console.ReadLine();
        if (saatSecim == "1")
        {
            Console.Write("\nKaç dakika dönüştüreceksiniz: ");
            int dakika = Convert.ToInt32(Console.ReadLine());
            int saat = dakika / 60;
            int kalanDakika = dakika % 60;
            Console.WriteLine($"{dakika} dakika = {saat} saat ve {kalanDakika} dakika");
        }
        else if (saatSecim == "2")
        {
            Console.Write("\nKaç saat dönüştüreceksiniz: ");
            int saat = Convert.ToInt32(Console.ReadLine());
            int dakika = saat * 60;
            Console.WriteLine($"{saat} saat = {dakika} dakika");
        }
        else
        {
            Console.WriteLine("Geçersiz seçim 1 veya 2 girin.");
        }


        Console.ReadKey();
    }
    else if (secim == "7")
    {
        Console.Write("Doğum gününüzü girin (1-31): ");
        int gun = Convert.ToInt32(Console.ReadLine());

        Console.Write("Doğum ayınızı girin (1-12): ");
        int ay = Convert.ToInt32(Console.ReadLine());

        string burc = "";

        if ((gun >= 21 && ay == 3) || (gun <= 20 && ay == 4))
            burc = "Koç";
        else if ((gun >= 21 && ay == 4) || (gun <= 21 && ay == 5))
            burc = "Boğa";
        else if ((gun >= 22 && ay == 5) || (gun <= 21 && ay == 6))
            burc = "İkizler";
        else if ((gun >= 22 && ay == 6) || (gun <= 22 && ay == 7))
            burc = "Yengeç";
        else if ((gun >= 23 && ay == 7) || (gun <= 22 && ay == 8))
            burc = "Aslan";
        else if ((gun >= 23 && ay == 8) || (gun <= 22 && ay == 9))
            burc = "Başak";
        else if ((gun >= 23 && ay == 9) || (gun <= 23 && ay == 10))
            burc = "Terazi";
        else if ((gun >= 24 && ay == 10) || (gun <= 22 && ay == 11))
            burc = "Akrep";
        else if ((gun >= 23 && ay == 11) || (gun <= 21 && ay == 12))
            burc = "Yay";
        else if ((gun >= 22 && ay == 12) || (gun <= 20 && ay == 1))
            burc = "Oğlak";
        else if ((gun >= 21 && ay == 1) || (gun <= 19 && ay == 2))
            burc = "Kova";
        else if ((gun >= 20 && ay == 2) || (gun <= 20 && ay == 3))
            burc = "Balık";
        else
            burc = "Geçersiz tarih girdiniz!";

        Console.WriteLine($"Burcunuz: {burc}");
        Console.ReadKey();
    }
    else if (secim == "8")
    {
        Console.WriteLine("\n=== Mini ATM ===");
        Console.WriteLine("\n1 - para yatırma");
        Console.WriteLine("2 - para çekme");
        Console.WriteLine("3 - bakiye sorgulama");
        Console.WriteLine("4 - çıkış");
        Console.Write("\nLütfen yapmak istediğiniz işlemi seçiniz: ");
        string atmsecim = Console.ReadLine();
        decimal bakiye = 1000.00m;
        bool devam = true;
        while (devam)
        {
            switch (atmsecim)
            {
                case "1":
                    Console.Write("Yatırmak istediğiniz tutarı giriniz: ");
                    decimal yatirilanTutar = Convert.ToDecimal(Console.ReadLine());
                    bakiye += yatirilanTutar;
                    Console.WriteLine($"Yeni bakiyeniz: {bakiye:C}");
                    break;
                case "2":
                    Console.Write("Çekmek istediğiniz tutarı giriniz: ");
                    decimal cekilenTutar = Convert.ToDecimal(Console.ReadLine());
                    if (cekilenTutar > bakiye)
                    {
                        Console.WriteLine("Yetersiz bakiye.");
                    }
                    else
                    {
                        bakiye -= cekilenTutar;
                        Console.WriteLine($"Yeni bakiyeniz: {bakiye:C}");
                    }
                    break;
                case "3":
                    Console.WriteLine($"Mevcut bakiyeniz: {bakiye:C}");
                    break;
                case "4":
                    devam = false;
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
            if (devam)
            {
                Console.Write("\nLütfen yapmak istediğiniz işlemi seçiniz: ");
                atmsecim = Console.ReadLine();
            }
        }


    }
    else if (secim == "0")
    {
        Console.WriteLine("Çıkış yapılıyor...");
        break;
    }
    else
    {
        Console.WriteLine("Geçersiz seçim. Lütfen 1-5 arasında bir sayı girin.");
        continue;
    }
}
