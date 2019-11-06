using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class ProductCommentsController : AdminControllerBase
    {
        // GET: Admin/ProductComments
        public ActionResult Index()
        {
            return View(db.ProductsComments.ToList());
        }

        public ActionResult Delete(int id)
        {
            var comment = db.ProductsComments.FirstOrDefault(x => x.ProductCommentsId == id);
            if (comment != null)
            {
                db.ProductsComments.Remove(comment);
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