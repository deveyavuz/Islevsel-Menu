using System.Text.Json;

using HttpClient client = new();


decimal bakiye = 1000.00m;

if (File.Exists("bakiye.json"))
{
    string json = File.ReadAllText("bakiye.json");
    bakiye = JsonSerializer.Deserialize<decimal>(json);
}


while (true)
{
    Console.WriteLine(DateTime.Now);
    Console.WriteLine("\n=== Menü V1'e hoşgeldiniz ===");
    Console.WriteLine("1. Hesap Makinesi");
    Console.WriteLine("2. Sayı Tahmin Oyunu");
    Console.WriteLine("3. VKİ Hesaplama");
    Console.WriteLine("4. Not Ortalaması Hesaplama");
    Console.WriteLine("5. Dolar - TL Çevirme");
    Console.WriteLine("6. Saat Dönüştürme");
    Console.WriteLine("7. Burç Bulma");
    Console.WriteLine("8. Mini ATM");
    Console.WriteLine("9. Yardım");
    Console.WriteLine("0. Çıkış");
    Console.Write("Bir seçenek girin (1-9): ");
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
            Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
            Console.Clear();
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
            {
                Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                Console.Clear();
                break;

            }
        }

    }
    else if (secim == "3")
    {
        Console.WriteLine("=== VKİ Hesaplama ===");
        Console.Write("Lütfen kilonuzu (kg) girin (örn=78,5): ");
        double kilo = Convert.ToDouble(Console.ReadLine());
        Console.Write("Lütfen boyunuzu (m veya cm) girin (örn=1,75): ");
        double boy = Convert.ToDouble(Console.ReadLine());
        if (boy > 3)
        {
            boy /= 100;
        }

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
        Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
        continue;

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
        Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
        continue;
    }
    else if (secim == "5")
    {
        string Dolarurl = "https://api.exchangerate-api.com/v4/latest/USD";
        string Eurourl = "https://api.exchangerate-api.com/v4/latest/EUR";

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
        if (kurSecim == "1")
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
        Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
        continue;
    }
    else if (secim == "8")
    {
        bool devam = true;
        
        while (devam)
        {
            Console.WriteLine("\n=== Mini ATM ===");
            Console.WriteLine("\n1 - para yatırma");
            Console.WriteLine("2 - para çekme");
            Console.WriteLine("3 - bakiye sorgulama");
            Console.WriteLine("4 - çıkış");
            Console.Write("\nLütfen yapmak istediğiniz işlemi seçiniz: ");
            string atmsecim = Console.ReadLine();
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
                    Console.ReadKey();
                    continue;
                case "4":
                    devam = false;
                    Console.WriteLine("Çıkış yapılıyor...");
                    File.WriteAllText("bakiye.json", JsonSerializer.Serialize(bakiye));
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    Console.ReadKey();
                    continue;
            }
        }
    }
    else if (secim == "9")
    {
        ConsoleColor orjin = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Yardım Sayfası ===");
        Console.WriteLine("- Menü arasında seçimler yaparak istediğiniz işleve gidebilirsiniz ");
        Console.WriteLine("- Program sonsuz döngüdedir çıkmak için ana menüden '0' tuşlayıp çıkabilirsiniz ");
        Console.WriteLine("- Herhangi bir tuşa basarak ana menüye dönebilirsiniz ");
        Console.ReadKey();
        Console.ForegroundColor = orjin;
        Console.WriteLine("\nAna menüye dönmek için bir tuşa basın...");
        Console.ReadKey();
        Console.Clear();
        continue;
    }
    else if (secim == "0")
    {
        Console.WriteLine("Çıkış yapılıyor...");
        File.WriteAllText("bakiye.json", JsonSerializer.Serialize(bakiye));
        break;
    }
    else
    {
        Console.WriteLine("Geçersiz seçim. Lütfen 0-9 arasında bir sayı girin.");
        Console.Clear();
        continue;
    }
}
