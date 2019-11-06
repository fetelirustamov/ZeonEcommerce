using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class PagesController : AdminControllerBase
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(db.Pages.ToList());
        }

        public ActionResult AddPages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPages(Pages pages)
        {
            if (pages!=null)
            {
                db.Pages.Add(pages);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditPages(int id)
        {
            var oldPages = db.Pages.FirstOrDefault(x=>x.Id==id);
            return View(oldPages);
        }

        [HttpPost]
      public ActionResult EditPages(Pages pages)
        {
            var oldPages = db.Pages.FirstOrDefault(x=>x.Id==pages.Id);
            if (oldPages!=null)
            {
                oldPages = pages;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var page = db.Pages.FirstOrDefault(x => x.Id == id);
            if (page != null)
            {
                db.Pages.Remove(page);
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