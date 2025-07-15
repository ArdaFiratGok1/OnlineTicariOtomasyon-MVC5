using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyonMVC.Controllers
{
    //BU YEDEK LOGİN

    public class LoginController : Controller
    {
        // GET: Login
        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login1()//Cari Kayıt Ol
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login1(Cariler c)//Cari Kayıt Ol
        {
            context.Carilers.Add(c);
            context.SaveChanges();
            return RedirectToAction("Index","Login");
        }

        public ActionResult Login2(Cariler c)//Cari Giriş
        {
            var bilgiler = context.Carilers.FirstOrDefault(x=>x.CariMail == c.CariMail && x.CariSifre==c.CariSifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail,false);//Burada SetAuthCookie içine yazdığım şeyi @HttpContext.Current.User.Identity.Name ile çağırabilirim. Ona verdiğim kullanıcı adı gibi bir şey.
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index","CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
                
        }

        public ActionResult Login3(Admin a)//Personel Giriş //Admin Giriş yani
        {
            var bilgiler = context.Admins.FirstOrDefault(x => x.KullaniciAd == a.KullaniciAd && x.Sifre == a.Sifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);//Burada SetAuthCookie içine yazdığım şeyi @HttpContext.Current.User.Identity.Name ile çağırabilirim. Ona verdiğim kullanıcı adı gibi bir şey.
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


    }
}