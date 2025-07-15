using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    [Authorize]

    public class UrunDetayController : Controller
    {
        // GET: UrunDetay

        Context context = new Context();
        public ActionResult Index(int id)
        {
            Class1 cs = new Class1();
            cs.Deger1 = context.Uruns.Where(x=>x.UrunID==id).ToList();
            cs.Deger2 = context.Detays.Where(x => x.DetayID == id).ToList();

            return View(cs);
        }
    }
}