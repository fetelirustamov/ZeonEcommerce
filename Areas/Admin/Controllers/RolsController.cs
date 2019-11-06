using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class RolsController : AdminControllerBase
    {
        // GET: Admin/Rols
        public ActionResult Index()
        {
            return View(db.Rols.ToList());
        }
        public ActionResult AddOrEdit(int? id)
        {

            if (id != null)
            {

                var rol = db.Rols.FirstOrDefault(x => x.RolsId == id);
                return View(rol);
            }
            else
            {
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrEdit(Rols Rol)
        {
            if (Rol.RolsId != 0)
            {
                var rols = db.Rols.FirstOrDefault(x => x.RolsId == Rol.RolsId);
                rols.Name = Rol.Name;
                rols.Description = Rol.Description;
                db.SaveChanges();
            }
            else
            {
                db.Rols.Add(Rol);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var rol = db.Rols.FirstOrDefault(x => x.RolsId == id);
            if (rol != null)
            {

                db.Rols.Remove(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}