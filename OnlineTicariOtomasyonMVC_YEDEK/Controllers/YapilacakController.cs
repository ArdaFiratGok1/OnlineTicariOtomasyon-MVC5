using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyonMVC.Controllers
{
    [Authorize]

    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        public ActionResult Index()
        {
            return View();
            //To-Do kısmını atladım
        }
    }
}