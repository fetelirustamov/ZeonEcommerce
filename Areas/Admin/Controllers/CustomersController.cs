using ZeonEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class CustomersController : AdminControllerBase
    {

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public ActionResult AddOrEdit(int id)
        {
            var customers = db.Customers.FirstOrDefault(x => x.CustomersId == id);
            return View(customers);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrEdit(Customers customers)
        {
            var oldCustomers = db.Customers.FirstOrDefault(x => x.CustomersId == customers.CustomersId);
            oldCustomers.Name = customers.Name;
            oldCustomers.Email = customers.Email;
            oldCustomers.City = customers.City;
            oldCustomers.Street = customers.Street;
            oldCustomers.Phone = customers.Phone;
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var customers = db.Customers.FirstOrDefault(x => x.CustomersId == id);
            if (customers != null)
            {
                db.Customers.Remove(customers);
                return RedirectToAction("index");
            }
            else
            {
                return HttpNotFound();

            }

        }
    }
}