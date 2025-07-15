using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyonMVC.Controllers
{
    [Authorize]

    public class FaturaController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Faturas.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Fatura f)
        {
            f.FaturaTarih = DateTime.Parse(DateTime.Now.ToShortDateString());

            context.Faturas.Add(f);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = context.Faturas.Find(id);
            return View("FaturaGetir",fatura);
        }

        public ActionResult FaturaGuncelle(Fatura f)
        {
            var fatura = context.Faturas.Find(f.FaturaID);
            fatura.FaturaTarih = f.FaturaTarih;
            fatura.FaturaSiraNo = f.FaturaSiraNo;
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.Saat = f.Saat;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.Toplam = f.Toplam;
            fatura.VergiDairesi = f.VergiDairesi;

            context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FaturaDetay(int id)
        {
            var fatura = context.Faturas.Find(id);
            var faturakalem_degerler = context.FaturaKalems.Where(x=>x.FaturaID == id).ToList();
            ViewBag.FaturaId = id;
            return View("FaturaDetay",faturakalem_degerler);
        }

        [HttpGet]
        public ActionResult FaturaKalemEkle(int id)
        {
            ViewBag.FaturaId = id;
            return View();
            
        }

        [HttpPost]
        public ActionResult FaturaKalemEkle(FaturaKalem fk)
        {
            fk.Tutar = fk.BirimFiyat * fk.Miktar;
            context.FaturaKalems.Add(fk);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}