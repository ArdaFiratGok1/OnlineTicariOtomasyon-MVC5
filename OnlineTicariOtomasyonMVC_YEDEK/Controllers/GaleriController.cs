using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace OnlineTicariOtomasyonMVC.Controllers
{
    [Authorize]

    public class GaleriController : Controller
    {
        // GET: Galeri

        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Uruns.ToList();
            return View(degerler);
        }
    }
}