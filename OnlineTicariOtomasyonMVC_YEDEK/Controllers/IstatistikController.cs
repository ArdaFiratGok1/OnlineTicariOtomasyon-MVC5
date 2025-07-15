using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;



namespace OnlineTicariOtomasyonMVC.Controllers
{
    [Authorize]

    public class IstatistikController : Controller
    {

        // GET: Istatistiklt
        Context context = new Context();
        public ActionResult Index()
        {
            var deger1 = context.Carilers.Count().ToString();
            var deger2 = context.Uruns.Count().ToString();
            var deger3 = context.Personels.Count().ToString();
            var deger4 = context.Kategoris.Count().ToString();

            ViewBag.d1 = deger1;
            ViewBag.d2 = deger2;
            ViewBag.d3 = deger3;
            ViewBag.d4 = deger4;


            var deger5 = context.Uruns.Sum(x => x.Stok).ToString();
            var deger6 = context.Uruns.Select(x=>x.Marka).Distinct().Count().ToString();
            var deger7 = context.Uruns.Where(x=>x.Stok<20).Count().ToString();//Kritik Seviye = Stok Sayısı 20'nin altında olanlar
            var deger8 = (from x in context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();//Direkt Query de yazabiliyorsun böyle

            ViewBag.d5 = deger5;
            ViewBag.d6 = deger6;
            ViewBag.d7 = deger7;
            ViewBag.d8 = deger8;

            var deger9 = context.Uruns.OrderBy(x=>x.SatisFiyat).Select(x=>x.UrunAd).FirstOrDefault().ToString();
            var deger10 = context.Uruns.GroupBy(x=>x.Marka).OrderByDescending(z=>z.Count()).Select(y=>y.Key).FirstOrDefault();
            //Gruplar sayı olarak tutulur örneğin 2 tane arçelik 3 tane samsung marka ürünüm varsa grupta 2 ve 3 numaraları yer alır. Burada Key ifadesi o numaranın denk geldiği Marka'dır. Bizde Marka'yı öğrenmek istiyoruz ondan Select kullandım.
            var deger11 = context.Uruns.Where(x=>x.UrunAd == "Buzdolabı").Sum(x=>x.Stok).ToString();
            var deger12 = context.Uruns.Where(x => x.UrunAd == "Laptop").Sum(x => x.Stok).ToString();

            ViewBag.d9 = deger9;
            ViewBag.d10 = deger10;
            ViewBag.d11 = deger11;
            ViewBag.d12 = deger12;

            var deger13 = context.Uruns.Where(k=>k.UrunID==context.SatisHarekets.GroupBy(x => x.UrunID).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault()).Select(m=>m.UrunAd).FirstOrDefault().ToString(); //En çok satan
            var deger14 = context.SatisHarekets.Sum(x=>x.ToplamTutar).ToString(); //Kasadaki Tutar
            var bugun = DateTime.Today;
            var deger15 = context.SatisHarekets.Where(x => x.Tarih == bugun).Count().ToString();
            var deger16 = (context.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x => (decimal?)x.ToplamTutar) ?? 0).ToString();

            ViewBag.d13 = deger13;
            ViewBag.d14 = deger14;
            ViewBag.d15 = deger15;
            ViewBag.d16 = deger16;

            return View();
        }

        public ActionResult BasitTablolar()
        {
            Class2 cs = new Class2();

            //urunler degil kategoriler olarak isimlendirme daha dogru olur da onemli degil
            var urunler = context.Uruns.GroupBy(x=>x.Kategori.KategoriAd).OrderByDescending(z=>z.Count()).Take(4).Select(y=>new KategoriIstatistik { kategoriad=y.Key, adet = y.Count()}).ToList();
            ViewBag.urun_sayi = urunler.Count();

            cs.kategori_istatistik = urunler;

            var sehirler = context.Carilers.GroupBy(x => x.CariSehir).OrderByDescending(z => z.Count()).Take(4).Select(y => new CariSehir { sehir = y.Key, adet = y.Count()}).ToList();
            ViewBag.sehirler_sayi= sehirler.Count();

            cs.cari_sehir = sehirler;

            return View(cs);
        }

        public PartialViewResult PartialView1()
        {
            var departmanlar = context.Personels.GroupBy(x=>x.Departman.DepartmanAd).OrderByDescending(x=>x.Count()).Take(4).Select(x=> new DepartmanIstatistik {departman_ad = x.Key,adet = x.Count() }).ToList();
            ViewBag.departman_sayi= departmanlar.Count();
            return PartialView(departmanlar);
        }
    }
}