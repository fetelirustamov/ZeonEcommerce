using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class ParameterTypeController : AdminControllerBase
    {
        // GET: Admin/ParameterType
        public ActionResult Index()
        {
            return View(db.ParameterType.ToList());
        }

        public ActionResult AddParameterType()
        {
            return View(db.Categories.ToList());
        }

        [HttpPost]
        public ActionResult AddParameterType(ParameterType parameterType)
        {
            db.ParameterType.Add(parameterType);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult EditParameterType(int? id)
        {
            ParameterTypeCategories viewModel = new ParameterTypeCategories();
            if (id == null) return HttpNotFound();

            viewModel.ParameterType = db.ParameterType.FirstOrDefault(x => x.ParameterTypeId == id);
            viewModel.Categories = db.Categories.ToList();
            if (viewModel.ParameterType == null) return HttpNotFound();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditParameterType(ParameterType parameterType)
        {
            var paramType = db.ParameterType.FirstOrDefault(x => x.ParameterTypeId == parameterType.ParameterTypeId);
            paramType.Name = parameterType.Name;
            paramType.CategoriesID = parameterType.CategoriesID;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var parameterType = db.ParameterType.FirstOrDefault(x => x.ParameterTypeId == id);
            if (parameterType != null)
            {

                db.ParameterType.Remove(parameterType);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}