using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]

    public class CarilerController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.Carilers.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        /*
        [HttpGet]
        public ActionResult CariEkle()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler c)
        {
            db.Carilers.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        *///Cari Ekleme Gerekirse Kullanırsın
        public ActionResult CariSil(int id)
        {
            var cari = db.Carilers.Find(id);
            cari.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = db.SatisHarekets.Where(x => x.CariID == id).ToList();
            var cr = db.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }

    }
}