using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyonMVC.Models.Siniflar
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        
        Context context=new Context();

        [Authorize]
        public ActionResult Index()//Profil kısmı
        {
            var mail = (string)Session["CariMail"];
            var cari_bilgiler = context.Carilers.FirstOrDefault(x=>x.CariMail==mail);
            return View(cari_bilgiler);
        }

        public ActionResult ProfilGuncelle(Cariler c)
        {
            var mail = (string)Session["CariMail"];
            c.CariID = context.Carilers.Where(x => x.CariMail == mail).Select(x => x.CariID).FirstOrDefault();
            var cari = context.Carilers.Find(c.CariID);
            cari.CariAd = c.CariAd;
            cari.CariSoyad = c.CariSoyad;
            cari.CariMail = c.CariMail;
            cari.CariResim = c.CariResim;
            cari.CariSehir = c.CariSehir;
            cari.CariSifre = c.CariSifre;
            

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}