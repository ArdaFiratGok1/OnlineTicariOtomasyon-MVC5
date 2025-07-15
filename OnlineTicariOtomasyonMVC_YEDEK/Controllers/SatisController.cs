using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyonMVC.Controllers
{
    [Authorize]

    public class SatisController : Controller
    {
        // GET: Satis
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.SatisHarekets.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult SatisEkle()
        {
            var urunad = (from x in context.Uruns.ToList() select new SelectListItem
            {
                Text = x.UrunAd,
                Value = x.UrunID.ToString()
            }).ToList();
            ViewBag.urunad1 = urunad;

            var personelad = (from x in context.Personels.ToList()
                          select new SelectListItem
                          {
                              Text = x.PersonelAd+" "+x.PersonelSoyad,
                              Value = x.PersonelID.ToString()
                          }).ToList();
            ViewBag.personelad1 = personelad;

            var cariad = (from x in context.Carilers.ToList()
                          select new SelectListItem
                          {
                              Text = x.CariAd +" "+x.CariSoyad,
                              Value = x.CariID.ToString()
                          }).ToList();
            ViewBag.cariad1 = cariad;

            return View();
        }

        [HttpPost]
        public ActionResult SatisEkle(SatisHareket s)
        {
            s.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            s.ToplamTutar = s.Adet * s.Fiyat;
            context.SatisHarekets.Add(s);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {
            var urunad = (from x in context.Uruns.ToList()
                          select new SelectListItem
                          {
                              Text = x.UrunAd,
                              Value = x.UrunID.ToString()
                          }).ToList();
            ViewBag.urunad1 = urunad;

            var personelad = (from x in context.Personels.ToList()
                              select new SelectListItem
                              {
                                  Text = x.PersonelAd + " " + x.PersonelSoyad,
                                  Value = x.PersonelID.ToString()
                              }).ToList();
            ViewBag.personelad1 = personelad;

            var cariad = (from x in context.Carilers.ToList()
                          select new SelectListItem
                          {
                              Text = x.CariAd + " " + x.CariSoyad,
                              Value = x.CariID.ToString()
                          }).ToList();
            ViewBag.cariad1 = cariad;


            var satis = context.SatisHarekets.Find(id);
            return View("SatisGetir",satis);
        }

        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var satis = context.SatisHarekets.Find(s.SatisID);
            satis.Tarih = s.Tarih;
            satis.CariID = s.CariID;
            satis.Fiyat = s.Fiyat;
            satis.Adet = s.Adet;
            satis.PersonelID = s.PersonelID;
            satis.ToplamTutar = s.ToplamTutar;
            satis.UrunID = s.UrunID;

            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SatisDetay(int id)
        {
            var satis_degerler = context.SatisHarekets.Where(x=>x.SatisID==id).ToList();//bu kısım çok gerekli değil ama olsun bozmaz. hep 1 tane çıkacak zaten id öznel olduğu için.
            return View("SatisDetay", satis_degerler);
        }
    }
}