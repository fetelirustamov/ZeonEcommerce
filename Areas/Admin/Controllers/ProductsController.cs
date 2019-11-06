using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class ProductsController : AdminControllerBase
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }


        public ActionResult AddProducts()
        {
            AdminProduct ViewModel = new AdminProduct();
            ViewModel.Categories = db.Categories.ToList();
            ViewModel.Brands = db.Brands.ToList();
            return View(ViewModel);

        }

        [HttpPost]
        public ActionResult AddProducts(Products products,HttpPostedFileBase Picture)
        {
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                products.Picture = "Public/Images/Products/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Products/"), fileName);
                Picture.SaveAs(fileName);

                products.SuppliersID = (int)Session["AdminLoginUserId"];
                db.Products.Add(products);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult EditProducts(int id)
        {
            AdminProduct ViewModel = new AdminProduct();
            ViewModel.Categories = db.Categories.ToList();
            ViewModel.Brands = db.Brands.ToList();
            ViewModel.Products = db.Products.FirstOrDefault(x => x.ProductsId == id);

            return View(ViewModel);

        }

        [HttpPost]
        public ActionResult EditProducts(Products products,HttpPostedFileBase Picture)
        {
            var oldProduct = db.Products.FirstOrDefault(x => x.ProductsId == products.ProductsId);
            if (Picture != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                var fileExtension = Path.GetExtension(Picture.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmddffff") + fileExtension;
                oldProduct.Picture = "Public/Images/Products/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Public/Images/Products/"), fileName);
                Picture.SaveAs(fileName);
                if (System.IO.File.Exists("~/" + oldProduct.Picture))
                {
                    System.IO.File.Delete("~/" + oldProduct.Picture);
                }

            }

            oldProduct.Name = products.Name;
            oldProduct.Description = products.Description;
            oldProduct.Discont = products.Discont;
            oldProduct.Price = products.Price;
            oldProduct.Stock = products.Stock;
            oldProduct.BrandsID = products.BrandsID;
            oldProduct.CategoriesID = products.CategoriesID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var sliderImages = db.Images.Where(x => x.ProductsID == id).ToList();
            var product = db.Products.FirstOrDefault(x => x.ProductsId == id);
            if (product != null)
            {
                foreach (var item in sliderImages)
                {
                    if (System.IO.File.Exists("~/" + item.ProductSlider))
                    {
                        System.IO.File.Delete("~/" + item.ProductSlider);
                    }
                }
                if (System.IO.File.Exists("~/" + product.Picture))
                {
                    System.IO.File.Delete("~/" + product.Picture);
                }
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult ProductSlider(int id)
        {

            var products = db.Products.FirstOrDefault(x => x.ProductsId == id);
            
            return View(products);
        }
        

    }
}