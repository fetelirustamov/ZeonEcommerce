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
    public class BlogController : AdminControllerBase
    {
        // GET: Admin/Blog
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        public ActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBlog(Blogs blogs,HttpPostedFileBase Picture)
        {

            blogs.CreateTime = DateTime.Now;
            blogs.SuppliersID = (int)Session["AdminLoginUserId"];

            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                blogs.Picture = "Public/Images/Blogs/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Blogs/"), fileName);
                Picture.SaveAs(fileName);
            }
            db.Blogs.Add(blogs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditBlog(int id)
        {
            var blog = db.Blogs.FirstOrDefault(x=>x.BlogsId==id);
            return View(blog);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditBlogs(Blogs blogs,HttpPostedFileBase Picture)
        {
            var oldBlog = db.Blogs.FirstOrDefault(x => x.BlogsId == blogs.BlogsId);


            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                oldBlog.Picture = "Public/Images/Blogs/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Blogs/"), fileName);
                Picture.SaveAs(fileName);
                if (System.IO.File.Exists("~/"+oldBlog.Picture))
                {
                    System.IO.File.Delete("~/" + oldBlog.Picture);
                }
            }
            oldBlog.BlogContent = blogs.BlogContent;
            oldBlog.Title = blogs.Title;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var blog = db.Blogs.FirstOrDefault(x => x.BlogsId == id);
            if (blog != null)
            {
                if (System.IO.File.Exists("~/" + blog.Picture))
                {
                    System.IO.File.Delete("~/" + blog.Picture);
                }
                db.Blogs.Remove(blog);
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