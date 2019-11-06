using ZeonEcommerce.Models;
using ZeonEcommerce.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class CategoriesController : AdminControllerBase
    {


        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }


        public ActionResult AddCategories()
        {
                return View(db.Categories.ToList());
        }


        public ActionResult EditCategories(int id)
        {
            AdminCategories ViewModel = new AdminCategories();
            ViewModel.Categories = db.Categories.ToList();
            ViewModel.CategoriesOne = db.Categories.FirstOrDefault(x => x.CategoriesId == id);
                return View(ViewModel);
          
        }
        
        [HttpPost]
        public ActionResult AddCategories(Categories categories)
        {
            db.Categories.Add(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult EditCategories(Categories categories)
        {
            var oldCategories = db.Categories.FirstOrDefault(x => x.CategoriesId == categories.CategoriesId);
            oldCategories.Name = categories.Name;
            oldCategories.Description = categories.Description;
            oldCategories.SubCategoryID = categories.SubCategoryID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
        public ActionResult Delete(int id)
        {
            var categories = db.Categories.FirstOrDefault(x => x.CategoriesId == id);
            if (categories != null)
            {
                db.Categories.Remove(categories);
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