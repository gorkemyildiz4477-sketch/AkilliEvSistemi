using System;
using System.Collections.Generic;

namespace AkilliEvSistemi
{
    public enum CihazTuru
    {
        Lamba = 1,
        Klima = 2,
        GuvenlikSistemi = 3
    }

    public class Cihaz
    {
        public int Kimlik { get; set; }
        public string Ad { get; set; }
        public CihazTuru Tur { get; set; }
        public bool AcikMi { get; private set; }

        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false;
        }

        public void Ac()
        {
            if (AcikMi)
            {
                Console.WriteLine($"{Ad} zaten açık durumda.");
            }
            else
            {
                AcikMi = true;
                Console.WriteLine($"{Ad} açıldı.");
            }
        }

        public void Kapat()
        {
            if (!AcikMi)
            {
                Console.WriteLine($"{Ad} zaten kapalı durumda.");
            }
            else
            {
                AcikMi = false;
                Console.WriteLine($"{Ad} kapatıldı.");
            }
        }

        public void BilgileriGoster()
        {
            string durum = AcikMi ? "Açık" : "Kapalı";
            Console.WriteLine($"Kimlik: {Kimlik} | Ad: {Ad} | Tür: {Tur} | Durum: {durum}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Cihaz> cihazlar = new List<Cihaz>
            {
                new Cihaz(1, "Salon Lambası", CihazTuru.Lamba),
                new Cihaz(2, "Oturma Odası Kliması", CihazTuru.Klima),
                new Cihaz(3, "Ana Kapı Alarmı", CihazTuru.GuvenlikSistemi)
            };

            bool devam = true;

            while (devam)
            {
                Console.WriteLine("\n--- Evdeki Cihazları Yönetme Sistemi ---");
                Console.WriteLine("1. Cihazları Listele");
                Console.WriteLine("2. Yeni Cihaz Ekle");
                Console.WriteLine("3. Cihaz Aç");
                Console.WriteLine("4. Cihaz Kapat");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.WriteLine("\n--- Kayıtlı Cihazlar ---");
                        if (cihazlar.Count == 0)
                        {
                            Console.WriteLine("Sistemde kayıtlı cihaz bulunmamaktadır.");
                        }
                        else
                        {
                            foreach (Cihaz cihaz in cihazlar)
                            {
                                cihaz.BilgileriGoster();
                            }
                        }
                        break;

                    case "2":
                        Console.Write("\nCihaz Kimliği (Sayı): ");
                        if (!int.TryParse(Console.ReadLine(), out int yeniKimlik))
                        {
                            Console.WriteLine("Hata: Geçerli bir sayısal kimlik giriniz.");
                            break;
                        }

                        bool kimlikVarMi = false;
                        foreach (Cihaz cihaz in cihazlar)
                        {
                            if (cihaz.Kimlik == yeniKimlik)
                            {
                                kimlikVarMi = true;
                                break;
                            }
                        }

                        if (kimlikVarMi)
                        {
                            Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz zaten mevcut.");
                            break;
                        }

                        Console.Write("Cihaz Adı: ");
                        string yeniAd = Console.ReadLine();

                        Console.WriteLine("Cihaz Türünü Seçiniz (1: Lamba, 2: Klima, 3: GuvenlikSistemi): ");
                        if (!int.TryParse(Console.ReadLine(), out int turSecim) || !Enum.IsDefined(typeof(CihazTuru), turSecim))
                        {
                            Console.WriteLine("Hata: Geçersiz cihaz türü seçtiniz.");
                            break;
                        }

                        CihazTuru yeniTur = (CihazTuru)turSecim;
                        cihazlar.Add(new Cihaz(yeniKimlik, yeniAd, yeniTur));
                        Console.WriteLine("Yeni cihaz başarıyla eklendi.");
                        break;

                    case "3":
                        Console.Write("\nAçmak istediğiniz cihazın kimliği: ");
                        if (!int.TryParse(Console.ReadLine(), out int acilacakKimlik))
                        {
                            Console.WriteLine("Hata: Geçerli bir kimlik giriniz.");
                            break;
                        }

                        Cihaz acilacakCihaz = null;
                        foreach (Cihaz cihaz in cihazlar)
                        {
                            if (cihaz.Kimlik == acilacakKimlik)
                            {
                                acilacakCihaz = cihaz;
                                break;
                            }
                        }

                        if (acilacakCihaz == null)
                        {
                            Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz bulunamadı.");
                        }
                        else
                        {
                            acilacakCihaz.Ac();
                        }
                        break;

                    case "4":
                        Console.Write("\nKapatmak istediğiniz cihazın kimliği: ");
                        if (!int.TryParse(Console.ReadLine(), out int kapatilacakKimlik))
                        {
                            Console.WriteLine("Hata: Geçerli bir kimlik giriniz.");
                            break;
                        }

                        Cihaz kapatilacakCihaz = null;
                        foreach (Cihaz cihaz in cihazlar)
                        {
                            if (cihaz.Kimlik == kapatilacakKimlik)
                            {
                                kapatilacakCihaz = cihaz;
                                break;
                            }
                        }

                        if (kapatilacakCihaz == null)
                        {
                            Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz bulunamadı.");
                        }
                        else
                        {
                            kapatilacakCihaz.Kapat();
                        }
                        break;

                    case "5":
                        devam = false;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen 1 ile 5 arasında bir değer giriniz.");
                        break;
                }
            }
        }
    }
}
