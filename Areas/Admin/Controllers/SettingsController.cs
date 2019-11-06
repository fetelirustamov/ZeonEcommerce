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
    public class SettingsController : AdminControllerBase
    {
        // GET: Admin/Settings
        public ActionResult Index()
        {
            var settings = db.Settings.First();
            Session["Title"] = settings.Title;
            return View(settings);
        }

        public ActionResult Edit(int id)
        {
            var settings = db.Settings.FirstOrDefault(x => x.Id == id);
            return View(settings);
        }

        [HttpPost]
        public ActionResult Edit(Settings settings, HttpPostedFileBase logo)
        {
            var setting = db.Settings.FirstOrDefault(x => x.Id == settings.Id);
            if (logo!=null)
            {
                var fileName = Path.GetFileNameWithoutExtension(logo.FileName);
                var fileExtension = Path.GetExtension(logo.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                setting.Logo = "Public/Images/Logo/"+fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Logo/"), fileName);
                logo.SaveAs(fileName);
            }
            Session["Title"]= settings.Title;
            setting.Adress = settings.Adress;
            setting.City = settings.City;
            setting.Phone = settings.Phone;
            setting.Fax = settings.Fax;
            setting.Title = settings.Title;
            setting.Email = settings.Email;
            setting.Facebook = settings.Facebook;
            setting.Twitter = settings.Twitter;
            setting.Instagram = settings.Instagram;
            setting.Google = settings.Google;
            setting.Youtube = settings.Youtube;
            
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}