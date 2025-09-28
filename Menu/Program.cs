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
    Console.Write("Bir seçenek girin (1-5): ");
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
        Console.WriteLine("=== Dolar Çevirici ===");
        Console.Write("Kaç Dolar bozdurmak istiyorsunuz: ");
        double dolar = Convert.ToDouble(Console.ReadLine());

        string url = "https://api.exchangerate-api.com/v4/latest/USD";

        using HttpClient client = new();
        string json = await client.GetStringAsync(url);

        var data = JsonDocument.Parse(json);
        double kur = data.RootElement
            .GetProperty("rates")
            .GetProperty("TRY")
            .GetDouble();
        double tl = dolar * kur;
        Console.WriteLine($"{dolar} Dolar = {tl} TL");
        Console.ReadKey();
        break;

    }
    else
    {
        Console.WriteLine("Geçersiz seçim. Lütfen 1-5 arasında bir sayı girin.");
        continue;
    }
}