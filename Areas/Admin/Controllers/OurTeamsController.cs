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
    public class OurTeamsController : AdminControllerBase
    {
        // GET: Admin/OurTeams
        public ActionResult Index()
        {
            return View(db.OurTeam.ToList());
        }

        public ActionResult AddTeams()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeams(OurTeam ourTeam, HttpPostedFileBase Picture)
        {
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                ourTeam.Picture = "Public/Images/OurTeams/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/OurTeams/"), fileName);
                Picture.SaveAs(fileName);
            }
            db.OurTeam.Add(ourTeam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditTeams(int id)
        {
            var teams = db.OurTeam.FirstOrDefault(x => x.OurTeamId == id);
            return View(teams);
        }

        [HttpPost]
        public ActionResult EditTeams(OurTeam ourTeam, HttpPostedFileBase Picture)
        {
            var oldTeams = db.OurTeam.FirstOrDefault(x => x.OurTeamId == ourTeam.OurTeamId);
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                oldTeams.Picture = "Public/Images/OurTeams/" + fileName;
                var fileNameRoad = Path.Combine(Server.MapPath("~/Public/Images/OurTeams/"), fileName);
                if (System.IO.File.Exists("~/" + oldTeams.Picture))
                {
                    System.IO.File.Delete("~/" + oldTeams.Picture);
                }
                Picture.SaveAs(fileNameRoad);

            }
            oldTeams.Name = ourTeam.Name;
            oldTeams.About = ourTeam.About;
            oldTeams.Job = ourTeam.Job;
            oldTeams.Facebook = ourTeam.Facebook;
            oldTeams.Twitter = ourTeam.Twitter;
            oldTeams.Linkedin = ourTeam.Linkedin;
            oldTeams.Google = ourTeam.Google;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var teams = db.OurTeam.FirstOrDefault(x => x.OurTeamId == id);
            if (teams != null)
            {
                if (System.IO.File.Exists("~/" + teams.Picture))
                {
                    System.IO.File.Delete("~/" + teams.Picture);
                }
                db.OurTeam.Remove(teams);
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