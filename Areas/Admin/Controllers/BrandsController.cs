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
    public class BrandsController : AdminControllerBase
    {
        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        public ActionResult AddBrands()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrands(Brands brands,HttpPostedFileBase Picture)
        {

            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                brands.Picture = "Public/Images/Brands/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Brands/"), fileName);
                Picture.SaveAs(fileName);
            }

            db.Brands.Add(brands);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditBrands(int id)
        {
            var brand = db.Brands.FirstOrDefault(x => x.BrandsId == id);
            return View(brand);
        }

        [HttpPost]
        public ActionResult EditBrands(Brands brands,HttpPostedFileBase Picture)
        {
            var oldBrand = db.Brands.FirstOrDefault(x => x.BrandsId == brands.BrandsId);
            
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                oldBrand.Picture = "Public/Images/Brands/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Brands/"), fileName);
                Picture.SaveAs(fileName);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
            }

            oldBrand.Description = brands.Description;
            oldBrand.Name = brands.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var brand = db.Brands.FirstOrDefault(x => x.BrandsId == id);
            if (brand != null)
            {
                if (System.IO.File.Exists("~/" + brand.Picture))
                {
                    System.IO.File.Delete("~/" + brand.Picture);
                }
                db.Brands.Remove(brand);
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