using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminControllerBase
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminAccount()
        {
            var user = Session["AdminLoginUser"] as Suppliers;
            return View(db.Suppliers.Where(x => x.SuppliersId == user.SuppliersId));
        }

        [HttpPost]
        public ActionResult AdminAccount(Suppliers suppliers, string NewPassword, string ConfirmNewPassword)
        {

            var supplier = db.Suppliers.FirstOrDefault(x => x.SuppliersId == suppliers.SuppliersId);
            if (suppliers.Password == supplier.Password)
            {
                if (NewPassword == ConfirmNewPassword)
                {
                    suppliers.Password = Crypto.Hash(NewPassword);
                }
            }
            else
            {
                suppliers.Password = "";
            }
            db.SaveChanges();
            return RedirectToAction("AdminAccount");
        }
    }
}