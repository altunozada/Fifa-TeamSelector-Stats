using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Formats.Asn1;
using ConsoleTables;



// Bütün Takımların Listesi
List<Takim> takimlar = new List<Takim>();




// A,B,C,D ve E overallarındakii takımlar listeleri bu listelere takımlar TakımVerisiÇek metoduyla eklenmiştir.
List<Takim> A = new List<Takim>();
List<Takim> B = new List<Takim>();
List<Takim> C = new List<Takim>();
List<Takim> D = new List<Takim>();
List<Takim> E = new List<Takim>();
//json verisini çeker ve takımları oluşturur
TakimVerisiCek();


Program();


List<Takim> TakimVerisiCek()
{
    //json verisini çeker ve takımları oluşturur
    string url = "vertopal.com_takimlar.json";
    using StreamReader reader = new(url);
    var json = reader.ReadToEnd();

    takimlar = JsonConvert.DeserializeObject<List<Takim>>(json);

    

    
    // takımları overallarına göre A,B,C,D,E sınıflarına ayırır ve sınıfların listesini oluşturur.
    foreach (var itemt in takimlar)
    {
        if (itemt.ovr > 82)
        {
            A.Add(itemt);
        }

        else if(itemt.ovr < 83 & itemt.ovr > 80)
        {
            B.Add(itemt);
        }
        else if (itemt.ovr > 77 & itemt.ovr < 81)
        {
            C.Add(itemt);
        }
        else if (itemt.ovr == 77 || itemt.ovr == 76)
        {
            D.Add(itemt);
        }
        else
        {
            E.Add(itemt);
        }



    }

    return takimlar;
}


int IntegerInputTaker(int type)
{
    int ret = 0;
    Console.WriteLine("");
    if (type == 1)
    {
        Console.Write("Kişi sayisi giriniz:  ");
        string input = Console.ReadLine();

        bool kontrol = int.TryParse(input, out ret);
        while (ret < 2 || kontrol ==false)
        {
            Console.Write("1'den büyük bir tam sayi girmeniz gerekmektedir.:  ");
            input = Console.ReadLine();
            kontrol = int.TryParse(input, out ret);
        }
    }

    else if (type == 2)
    {
        Console.Write("Tur sayisi giriniz:  ");
        string input = Console.ReadLine();

        bool kontrol = int.TryParse(input, out ret);
        while (ret < 1 || kontrol == false)
        {
            Console.Write("0'dan büyük bir tam sayi girmeniz gerekmektedir.:  ");
            input = Console.ReadLine();
            kontrol = int.TryParse(input, out ret);
        }


    }


        
    return ret;  
    

   

}




string OneZeroInputTaker(int type)
{
    Console.WriteLine("");
    if (type == 1) { Console.Write("Averaj Durumu Giriniz:  "); Console.Write("Puan esitligi durumunda averaj uygulanmasini istiyorsaniz 'E' yazin istemiyorsaniz 'H' yazin:");}
    if (type == 2) { Console.WriteLine("Verileri silmek istediğinize eminseniz E'ye istemiyorsanız H'ye basınız: "); }
    string ret = Console.ReadLine().ToUpper();

   


    while (ret!="E" && ret!="H")
    {
        Console.WriteLine("Lütfen sadece E veya H yazın: ");
        ret = Console.ReadLine().ToUpper();
    }
    return ret;
}

string GorSil()
{
    Console.WriteLine("Verileri görmek için G'ye verileri silmek için S'te geri dönmek için R'ye basınız:   ");
    string ret = Console.ReadLine().ToUpper();

    while (ret != "G" && ret != "S" && ret != "R")
    {
        Console.WriteLine("Lütfen sadece G,S veya R yazın: ");
        ret = Console.ReadLine().ToUpper();
    }
    return ret;


}




//girilen kişi sayısına göre isim girilmesini ister ve girilen isimleri kişi listesine ekler
List<kisi> KisiGir(int kisisayisi)
{
    List<kisi> kisiler = new List<kisi>();
    for (int i = 1; i <= kisisayisi; i++)
    {
        int kayitdurumu;
        Console.WriteLine("");
        Console.Write(i + "'nci Kisi: ");
        string s = Console.ReadLine();
        s = s.ToUpper();

        kisi k = new kisi(s);
        kisiler.Add(k);
    }

    return kisiler;


}



//Kişi İsimleriyle maçlar oluşturup fikstür oluşturur ve fikstür listesine döndürür.

List<mac> FiksturOlustur(int kisisayisi,int tursayisi,List<kisi> kisiler)
{
    List<mac> fikstür= new List<mac>();
    while (fikstür.Count < PermutationsAndCombinations.nCr(kisisayisi, 2))
    {
        int control = 0;
        while (control == 0)
        {
            Random rnd = new Random();
            int n1 = rnd.Next(kisiler.Count);
            int n2 = rnd.Next(kisiler.Count);

            var mac = new mac(kisiler[n1], kisiler[n2]);
            var mac2 = new mac(kisiler[n2], kisiler[n1]);
            bool c = false;

            foreach (var item in fikstür)
            {
                if (item.m1.isim == kisiler[n1].isim && item.m2.isim == kisiler[n2].isim)
                {

                    c = true;
                    break;

                }

                else if (item.m2.isim == kisiler[n1].isim && item.m1.isim == kisiler[n2].isim)
                {

                    c = true;
                    break;
                }



            }



            if (c == true) { }


            else if (kisiler[n1].ust == 2 || kisiler[n2].ust == 2) { }

            else if (n1 == n2) { }

            else
            {
                fikstür.Add(mac);
                kisiler[n1].ust++;
                kisiler[n2].ust++;


                foreach (var item in kisiler)
                {
                    if (item.isim != kisiler[n1].isim && item.isim != kisiler[n2].isim)
                    {
                        item.ust = 0;

                    }



                }
                control = 1;
            }




        }


    }




    List<mac> fikstüryeni = new List<mac>();
    for (int i = 1; i < tursayisi+1; i++)
    {
        
        int j = 0;
        
        foreach (var item in fikstür)
        {
            j++;
            mac bos = new mac(item.m1,item.m2);
            bos.tur = i;
            bos.sira = j;


            fikstüryeni.Add(bos);
            

        }
       
    }

    
    PuanDurumuFikstür(kisiler, fikstüryeni, tursayisi);
    return fikstüryeni;

}



// class seçimi yapılmasını sağlar ve seçimi kontrol eder..
List<Takim> ClassSecimi()
{

    string[] takimclasslari = { "A", "a", "B", "b", "C", "c", "D", "d", "E", "e" };

    string x = Convert.ToString(Console.ReadLine());
    x = x.ToUpper();
    int c = 0;

    while (c == 0)
    {
        if (Array.IndexOf(takimclasslari, x) == -1)
        {

            Console.WriteLine("Lütfen 'a,b,c,d,e' harflerinden birini girin. ");
            x = Console.ReadLine().ToUpper();

        }

        else
        {
            c = 1;

        }

    }


    if (x == "A") { return A; }
    else if (x == "B") { return B; }
    else if (x == "C") { return C; }
    else if (x == "D") { return D; }
    else { return E; }

}

// class seçimi metodunda oluşan class stringine göre takimclassı döndürür..



//takım seçimini yapar eğer takım seçimi beğenilmezse tekrardan seçtirir.
static void takimsec(List<Takim> a, List<Takim>b, string kisi1, string kisi2)
{
    string kontrol = "C";

    while (kontrol == "C")
    {
        Random rnd = new Random();
        int sayi1 = rnd.Next(a.Count - 1);
        int sayi2 = rnd.Next(b.Count - 1);

        while (sayi1 == sayi2)
        {
            sayi1 = rnd.Next(a.Count - 1);
            sayi2 = rnd.Next(b.Count - 1);
        }
        Console.WriteLine("------------------------------------------");
        Console.WriteLine(kisi1 + ": " + a[sayi1].takimadi + " vs " + kisi2 + ": " + b[sayi2].takimadi);
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("");
        Console.WriteLine("");

        Console.WriteLine("Tekrar çevirmek için C'ye devam etmek için D'ye basın: ");
        kontrol = Console.ReadLine();
         
        string input = kontrol.ToUpper();
        while (input != "D" && input != "C")
        {
            Console.WriteLine("Sadece C veya D'yi tuslayiniz.");
            input = Console.ReadLine().ToUpper();

        }
        
        kontrol = input;
    }

}



// mac itemı alarak kişilerin gol sayısını sorar ardından mac itemini return eder.
mac MacSonucu(mac item)
{
    bool kontrol = false;
    bool kontrol2 = false;
    int gol1 = -1;
    int gol2 = -1;
    while (gol1 < 0 || gol2 < 0 || kontrol==false || kontrol2==false)
    {

        Console.Write(Convert.ToString(item.m1.isim) + " kaç gol atti : ");
        string gol1Input = Console.ReadLine().ToUpper();
        Console.WriteLine("");
        kontrol = int.TryParse(gol1Input, out gol1);


        Console.Write(Convert.ToString(item.m2.isim + " kaç gol atti : "));
        string gol2Input = Console.ReadLine();
        Console.WriteLine("");
        kontrol2 = int.TryParse(gol2Input, out gol2);


        if (gol1 < 0 || gol2<0 || kontrol == false || kontrol2 == false) { Console.WriteLine("Lütfen 0'dan büyük bir tam sayı girin..."); }
        Console.WriteLine("");
    }



    if (gol1 > gol2)
    {
        item.m1.puan = item.m1.puan + 3;
        item.m1.averaj = item.m1.averaj + gol1 - gol2;
        item.m2.averaj = item.m2.averaj + gol2 - gol1;
        item.m1.oynanan = item.m1.oynanan + 1;
        item.m2.oynanan = item.m2.oynanan + 1;
    }

    else if (gol2 > gol1)
    {
        item.m2.puan = item.m2.puan + 3;
        item.m2.averaj = item.m2.averaj + gol2 - gol1;
        item.m1.averaj = item.m1.averaj + gol1 - gol2;
        item.m1.oynanan = item.m1.oynanan + 1;
        item.m2.oynanan = item.m2.oynanan + 1;

    }

    else
    {
        item.m1.puan = item.m1.puan + 1;
        item.m2.puan = item.m2.puan + 1;
        item.m1.oynanan = item.m1.oynanan + 1;
        item.m2.oynanan = item.m2.oynanan + 1;

    }

    item.gol1 = gol1;
    item.gol2 = gol2;
    


    return item;



}





List<kisi> PuanDurumu(List<kisi> kisiler)
{
    //kişiler2 listesiyle puan durumu olışturulur ve alttaki for döngüsüyle her bir takım sırayla okunur.

    
    kisiler = kisiler.OrderByDescending(x => x.puan).ThenByDescending(x => x.averaj).ThenBy(x=>x.oynanan).ToList();


    
   
    

    var puandurumuTablosu = new ConsoleTable("  ", "Puan", "Averaj", "Oynanan");
    if (kisiler.Count > 0)
    {
        Console.WriteLine("    TÜM ZAMANLAR PUAN DURUMU      ");
        foreach (var item in kisiler)
        {
            puandurumuTablosu.AddRow(item.isim, item.puan, item.averaj, item.oynanan);
        }
        puandurumuTablosu.Write();
    }

    else
    {
        Console.WriteLine("Kisi verileri silinmiş.");
    }


    return kisiler;



}


List<kisi> PuanDurumuFikstür(List<kisi> kisiler, List<mac> maclar, int tursayisi)
{
    //kişiler2 listesiyle puan durumu olışturulur ve alttaki for döngüsüyle her bir takım sırayla okunur.


    kisiler = kisiler.OrderByDescending(x => x.puan).ThenByDescending(x => x.averaj).ThenBy(x => x.oynanan).ToList();


    Console.WriteLine("-------------------------------------");

    Console.WriteLine("          PUAN DURUMU      ");


    var puandurumuTablosu = new ConsoleTable("  ", "Puan", "Averaj", "Oynanan");
    var fiksturTablosu = new ConsoleTable("Sıra", "Maç", "Skor");

    foreach (var item in kisiler)
    {
        

        puandurumuTablosu.AddRow(item.isim, item.puan, item.averaj, item.oynanan);
    }

    

    

    
        foreach (var item in maclar)
        {
           
            if (item.oynandi == false)
            {
            fiksturTablosu.AddRow(item.tur + "." + item.sira , item.m1.isim + " vs " + item.m2.isim, " --:--");
            }
            else
            {
                fiksturTablosu.AddRow(item.tur + "." + item.sira, item.m1.isim + " vs " + item.m2.isim, item.gol1 + " : " + item.gol2);

            }
            
        }
    
    

    puandurumuTablosu.Write(Format.Default);
    Console.WriteLine("");
    Console.WriteLine("----------------------------------");
    Console.WriteLine("             FİKSTÜR      ");
    fiksturTablosu.Write(Format.Default);
    
    

    return kisiler;



}

void FiksturGoster(List<mac> maclar)
{
    var fiksturTablosu = new ConsoleTable("Tarih", "Maç", "Skor");



    if (maclar.Count > 0)
    {
        Console.WriteLine("----------------------------------");
        Console.WriteLine("   TUM ZAMANLARDAKİ MAÇLAR      ");

        foreach (mac item in maclar)
        {

            if (item.oynandi == false)
            {
                fiksturTablosu.AddRow(item.tarih, item.m1.isim + " vs " + item.m2.isim, " --:--");
            }
            else
            {
                fiksturTablosu.AddRow(item.tarih, item.m1.isim + " vs " + item.m2.isim, item.gol1 + " : " + item.gol2);

            }

        }
        fiksturTablosu.Write(Format.Default);
    }

    else
    {
        Console.WriteLine("Maç verileri silinmiş");
    }


}


void SampiyonlukTablosu(List<kisi> kisiler)
{

    kisiler = kisiler.OrderByDescending(x => x.sampiyonluk).ToList();


    

    var SampiyonlukTablosu = new ConsoleTable("İsim","Şampiyonluk");


    if (kisiler.Count > 0)
    {
        Console.WriteLine("-------------------------------------");

        Console.WriteLine("      ŞAMPİYONLUK TABLOSU      ");

        foreach (var item in kisiler)
        {
            SampiyonlukTablosu.AddRow(item.isim, item.sampiyonluk);


        }

        SampiyonlukTablosu.Write();

    }

    else
    {
        Console.WriteLine("Kişi verileri silinmiş.");
    }

}
    

//büyük loop tur sayısı kadar küçük loop da her bir turdaki maç sayısı kadar döner. daha önce oluşturulmuş fikstürdeki maçlar tek tek gösterilerek sonuca göre puan durumu oluşturulur.
mackisi Macyap(List<kisi> kisiler,int kisisayisi,int tursayisi,List<mac> fikstür,string averajdurumu)
{

    List<kisi> kisiler3 = new List<kisi>();
    List<mac> maclar = fikstür;
    int indi = 0;

    int i = 0;


    


    foreach (mac item in fikstür)

        {
             i++;


            
            Console.WriteLine("");
            Console.WriteLine(item.m1.isim + " vs " + item.m2.isim + "  " + (item.tur) + "'NCI TUR   " + item.sira + "'NCI MAC");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");

            Console.WriteLine(item.m1.isim + " class tercihi: ");
            var c1= ClassSecimi();

            Console.WriteLine(item.m2.isim + " class tercihi: ");
            var c2 = ClassSecimi();

           

            takimsec(c1,c2,item.m1.isim, item.m2.isim);
            Console.WriteLine("");
            mac bos = item;
            bos = MacSonucu(bos);

            maclar[indi].sira = item.sira;
            maclar[indi].tur = item.tur;
            maclar[indi].gol1 = bos.gol1;
            maclar[indi].gol2 = bos.gol2;
            maclar[indi].oynandi = true;
            
            indi++;


            kisiler =PuanDurumuFikstür(kisiler,maclar,tursayisi);
            int q = 0;
            // bu döngü de eğer şampiyonluğun kalan maç olmasına rağmen kesinleşip kesinleşmediğini kontrol eder.
            foreach (var itemsam in kisiler)
            {

                var tmacsayisi = (kisisayisi - 1) * tursayisi;
                int cpuan = ((tmacsayisi - itemsam.oynanan) * 3) + itemsam.puan;


                if (kisiler[0].isim != itemsam.isim)
                {
                    if (cpuan < kisiler[0].puan)
                    {
                        q++;
                    }

                }
            }


           

            Console.WriteLine("--------------------------------");
            if (q == kisisayisi - 1 & fikstür.Count !=i )
            {
                Console.WriteLine("Kalan maclar sonunda sonuc degismeyecegi icin " + kisiler[0].isim + " SAMPIYONNN!!!!!!");
                
                item.tur = tursayisi;
                break;


            }



        }

    



    List<kisi> kisilerturnuva = SampiyonBelirle(kisiler, kisisayisi, tursayisi, averajdurumu);

    kisiler[0].sampiyonluk = kisilerturnuva[0].sampiyonluk;
    
    mackisi ret = new mackisi(kisiler,maclar);
    


    return ret;
}






//Şampiyon Belirlenir.
List<kisi> SampiyonBelirle(List<kisi> kisiler,int kisisayisi, int tursayisi,string averajdurumu)
{
   int d = 0;
   foreach (var item in kisiler)
   {
       if (item.oynanan == (kisisayisi - 1)*(tursayisi)) { d++; }

   }




    if(d==kisiler.Count())
    {

        if (kisiler[0].puan == kisiler[1].puan)
        {
            if (averajdurumu == "H")
            {
                Console.WriteLine("SAMPIYON CIKMADI !!!!!!");
            }

            if (averajdurumu == "E")
            {
                if (kisiler[0].averaj== kisiler[1].averaj)
                {Console.WriteLine( "Averajlar da eşit olduğu için şampiyon çıkmadı"); }
                else { Console.WriteLine("ŞAMPIYON " + kisiler[0].isim + " !!!!!!!!!!!!!!!"); kisiler[0].sampiyonluk=1; }
                
            }
        }



        else
        {
            Console.WriteLine(kisiler[0].isim + " ŞAMPIYON!!!!!!!!!!!!!!!!!!!!!");
            kisiler[0].sampiyonluk=1;
        }
    }
    else { Console.WriteLine("Tebrikler Yine Bekleriz...."); kisiler[0].sampiyonluk=1; }
    return kisiler;

}









void Turnuva()
{

    

        // IntegerInputTaker() metodu 1 parametresi alarak kişisayısını sorar ve return eder.

        int kisisayisi = IntegerInputTaker(1);

        // IntegerInputTaker() metodu 2 parametresi alarak tursayısını sorar ve return eder.
        int tursayisi = IntegerInputTaker(2);

        // averaj durumu girilir
        string averajdurumu = OneZeroInputTaker(1);

        // bu metod kişi isimlerinin girilmesini sağlar
        List<kisi> kisiler =KisiGir(kisisayisi);

        //Fikstürdeki maçları tutan listeyi FiksturOlustur metoduyla oluşturur.
       
        List<mac> fikstür = FiksturOlustur(kisisayisi,tursayisi,kisiler);

        //Macyap metoduyla bütün maçların yapılması sağlanır.
        mackisi turnuva= Macyap(kisiler,kisisayisi,tursayisi,fikstür,averajdurumu);
        KayitEkrani(turnuva);

        
      
    }


    





List<kisi> KisiCek()
{
    List<kisi> tumzamanlarKisi = new List<kisi>();

    string url = "tumzamanlarkisiler.json";
    using StreamReader reader = new(url);
    var json = reader.ReadToEnd();

    tumzamanlarKisi = JsonConvert.DeserializeObject<List<kisi>>(json);

    return tumzamanlarKisi;


}

List<mac> MacCek()
{
   
    List<mac> tumzamanlarMaclar = new List<mac>();

    string url = "tumzamanlarmaclar.json";
    using StreamReader reader = new(url);
    var json = reader.ReadToEnd();

    tumzamanlarMaclar = JsonConvert.DeserializeObject<List<mac>>(json);

    return tumzamanlarMaclar;




}

void KisiSil()
{
    List<kisi> a = new List<kisi>();

    string input = OneZeroInputTaker(2);

    if (input=="E")
    {
        string url = "tumzamanlarkisiler.json";
        using StreamWriter writer = new(url);
        writer.Write(JsonConvert.SerializeObject(a));
        Console.WriteLine("Kişi verileri silindi......");

    }


    



    


}

List<kisi> KisiYaz(List<kisi> kisiler)
{
    List<kisi> tumzamanlarKisi = new List<kisi>();
    tumzamanlarKisi = KisiCek();


        if (tumzamanlarKisi.Count == 0)
        {
            foreach (var item in kisiler)
            {
                tumzamanlarKisi.Add(item);
            }
        }

        else
        {
            foreach (var kisi in kisiler)
            {
                int kontrol = 0;
                foreach (var tum in tumzamanlarKisi)
                {
                    if (kisi.isim == tum.isim)
                    {
                        kontrol++;
                        tum.puan += kisi.puan;
                        tum.averaj += kisi.averaj;
                        tum.oynanan += kisi.oynanan;
                        tum.sampiyonluk += kisi.sampiyonluk;

                    }
                }

                if (kontrol == 0) { tumzamanlarKisi.Add(kisi); }



            }
            

        }



    string url = "tumzamanlarkisiler.json";
    using StreamWriter writer = new(url);
    writer.Write(JsonConvert.SerializeObject(tumzamanlarKisi));
    Console.WriteLine("Kişiler Yazıldı....");


    return kisiler;



}

List<mac> MacYaz(List<mac> items)
{
    List<mac> tumzamanlarMaclar = new List<mac>();
    
    
    tumzamanlarMaclar = MacCek();

    
   
        if (tumzamanlarMaclar == null)
        {
            tumzamanlarMaclar = items;

        }
        else
        {
            foreach (var item in items)
            {
                tumzamanlarMaclar.Add(item);

            }

        }

        string url = "tumzamanlarmaclar.json";
        using StreamWriter writer = new(url);
        writer.Write(JsonConvert.SerializeObject(tumzamanlarMaclar));
        Console.WriteLine("Maçlar Yazıldı....");
    



    return tumzamanlarMaclar;




}

void MacSil()
{

    List<mac> a = new List<mac>();

    string input = OneZeroInputTaker(2);

    if (input == "E")
    {
        string url = "tumzamanlarmaclar.json";
        using StreamWriter writer = new(url);
        writer.Write(JsonConvert.SerializeObject(a));
        Console.WriteLine("Maçlar silindi.....");
    }

 


}

void Program()
{
    Console.WriteLine("-------------Turnuvaya Hoşgeliniz-------------");
    Console.WriteLine("");


    Console.WriteLine("Turnuvayı Başlatmak için B'ye, Tum Zamanlar ile ilgili işlemler için V'ye basınız:   ");
    string input = Console.ReadLine();
    input = input.ToUpper();

    while (input != "B" && input != "V")
    {
        Console.WriteLine("Lütfen sadece B veya V'ye basınız.");
        Console.WriteLine("Turnuvayı Başlatmak için B'ye, Tum Zamanlar ile ilgili işlemler için V'ye basınız:   ");
        input = Console.ReadLine().ToUpper();

    }

    if (input=="B")
    {
        Turnuva();



    }

    else
    {

        VeriEkrani();

    }





}

void VeriEkrani()
{
    Console.WriteLine(" ");
    Console.WriteLine("Puan durumu verileri için P'ye, Maç verileri için M'ye, Şampiyonluk Tablosu için S'ye, Geri dönmek için D'ye, Çıkmak için Q'ya basınız:   ");
    string input = Console.ReadLine();
    input = input.ToUpper();

    while (input != "P" && input != "M" && input != "S" && input != "D" && input != "Q")
    {
        Console.WriteLine("Lütfen sadece P,M,S,D veya Q'ya basınız.");
        Console.WriteLine("Puan durumu verileri için P'ye, Maç verileri için M'ye, Şampiyonluk Tablosu için S'ye, Geri dönmek için D'ye basınız:   ");
        input = Console.ReadLine().ToUpper();

    }

    if (input=="P")
    {
        string input2 = GorSil();
        if (input2 == "G") { PuanDurumu(KisiCek()); } 
        else if (input2 == "S") { KisiSil(); }
        else { VeriEkrani(); }
        
        VeriEkrani();

    }

    else if(input == "M")
    {
        string input2 = GorSil();
        if (input2 == "G") { FiksturGoster(MacCek()); }
        else if (input2 == "S") { MacSil(); }
        else { VeriEkrani(); }

        VeriEkrani();
    }

    else if(input=="S")
    {
        string input2 = GorSil();
        if (input2 == "G") { SampiyonlukTablosu(KisiCek()); }
        else if (input2 == "S") { Console.WriteLine("Şampiyonluk verilerinin puan durumu sekmesinden silinmesi geremektedir."); }
        else { VeriEkrani(); }

        VeriEkrani();
    }

    else if(input=="D")
    {
        Program();
    }
    else
    {
        Cikis();

    }


}


void KayitEkrani(mackisi turnuva)
{
    Console.WriteLine("");

        Console.WriteLine("Verileri kaydetmek için K'ye, Veri Ekranına geçmek için V'ye, Çıkış için Q'ya,Tekrar turnuva yapmak için T'ye basınız:   ");
        string input = Console.ReadLine();
        input = input.ToUpper();

        while (input != "K" && input != "V" && input != "Q" && input != "T")
        {
            Console.WriteLine("Lütfen sadece K,V,T veya Q'ya basınız.");
            Console.WriteLine("Verileri kaydetmek için K'ye, Veri Ekranına geçmek için V'ye, Çıkış için Q'ya,Tekrar turnuva yapmak için T'ye basınız:   ");
            input = Console.ReadLine().ToUpper();

        }


    if(input=="K")
    {

        KisiYaz(turnuva.kisiler);
        MacYaz(turnuva.maclar);
        KayitEkrani2();
    }

    else if(input=="T")
    {
        
        Turnuva();
    }
    else if(input == "V")
    {
        VeriEkrani();
    }

    else
    {
        Cikis();
    }


}

void KayitEkrani2()
{

    Console.WriteLine("");


    Console.WriteLine("Veri Ekranina geçmek için V'ye, Çikiş için Q'ya,Tekrar turnuva yapmak için T'ye basiniz:   ");
    string input = Console.ReadLine();
    input = input.ToUpper();

    while (input != "V" && input != "Q" && input != "T")
    {
        Console.WriteLine("Lütfen sadece V,T veya Q'ya basiniz.");
        Console.WriteLine("Veri Ekranina geçmek için V'ye, Çikiş için Q'ya,Tekrar turnuva yapmak için T'ye basiniz:   ");
        input = Console.ReadLine().ToUpper();

    }

    if (input == "T")
    {

        Turnuva();
    }
    else if (input == "V")
    {
        VeriEkrani();
    }

    else
    {
        Cikis();
    }

}




void Cikis()
{

    Console.WriteLine("---------------Yine Bekleriz--------------");






}


