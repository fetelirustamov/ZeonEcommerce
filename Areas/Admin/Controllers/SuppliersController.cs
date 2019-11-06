using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class SuppliersController : AdminControllerBase
    {
        // GET: Admin/Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        public ActionResult AddOrEdit(int? id)
        {

            if (id != null)
            {
                return View(db.Suppliers.FirstOrDefault(x => x.SuppliersId == id));
            }
            else
            {
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrEdit(Suppliers Supplier)
        {
            Supplier.RolsID = 3;

            if (Supplier.SuppliersId != 0)
            {
                var suppliers = db.Suppliers.FirstOrDefault(x => x.SuppliersId == Supplier.SuppliersId);
                suppliers.Name = Supplier.Name;
                suppliers.SurName = Supplier.SurName;
                suppliers.Company = Supplier.Company;
                suppliers.City = Supplier.City;
                suppliers.Street = Supplier.Street;
                suppliers.Email = Supplier.Email;
                suppliers.Phone = Supplier.Phone;
                suppliers.WebSite = Supplier.WebSite;
                if (Supplier.Password != null)
                {
                    suppliers.Password = Crypto.Hash(Supplier.Password);
                }

                suppliers.Token = new Guid();

                db.SaveChanges();
            }
            else
            {
                Supplier.Password = Crypto.Hash(Supplier.Password);
                Supplier.Token = new Guid();
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var suppliers = db.Suppliers.Find(id);
            if (suppliers != null)
            {
                db.Suppliers.Remove(suppliers);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}