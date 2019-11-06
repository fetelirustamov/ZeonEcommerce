using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class AdvertisingController : AdminControllerBase
    {
        // GET: Admin/Advertising
        public ActionResult Index()
        {
            return View(db.Advertising.ToList());
        }

        public ActionResult AddAdvertising()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdvertising(Advertising advertising,HttpPostedFileBase Picture)
        {
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                advertising.Picture = "Public/Images/Logo/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Logo/"), fileName);
                Picture.SaveAs(fileName);
            }

            db.Advertising.Add(advertising);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditAdvertising(int id)
        {
            var advertising = db.Advertising.FirstOrDefault(x => x.AdvertisingId == id);
            return View(advertising);
        }

        [HttpPost]
        public ActionResult EditAdvertising(Advertising advertising,HttpPostedFileBase Picture)
        {
            var oldAdvertising = db.Advertising.FirstOrDefault(x => x.AdvertisingId == advertising.AdvertisingId);

            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                oldAdvertising.Picture = "Public/Images/Logo/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Logo/"), fileName);
                if (System.IO.File.Exists("~/" + oldAdvertising.Picture))
                {
                    System.IO.File.Delete("~/" + oldAdvertising.Picture);
                }
                Picture.SaveAs(fileName);
            }

            oldAdvertising.RowNo = advertising.RowNo;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var advertising = db.Advertising.FirstOrDefault(x => x.AdvertisingId == id);
            if (advertising != null)
            {
                if (System.IO.File.Exists("~/" + advertising.Picture))
                {
                    System.IO.File.Delete("~/"+advertising.Picture);
                }
                db.Advertising.Remove(advertising);
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

