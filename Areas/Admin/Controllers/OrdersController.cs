using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class OrdersController : AdminControllerBase
    {
        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Delete(int id)
        {
            var orders = db.Orders.FirstOrDefault(x => x.OrdersId == id);
            if (orders==null)
                return RedirectToAction(nameof(Index));
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}