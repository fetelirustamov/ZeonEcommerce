using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        protected ECommerceContext db = new ECommerceContext();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(string Email, string Password)
        {
           //var password = Crypto.Hash(Password);
            var data = db.Suppliers.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (data !=null)
            {
                Session["AdminLoginUser"] = data;
                Session["AdminLoginUserId"] = data.SuppliersId;
                return RedirectToAction("Index","AdminHome");
            }
            else
            {
                return Redirect("/Admin/AdminLogin");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}