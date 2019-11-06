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
    public class SliderImagesController : AdminControllerBase
    {
        // GET: Admin/SliderImage
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        public ActionResult HomeSlider()
        {
            return View();
        }

        [HttpPost]
        public void AddImages(Images images)
        {
            if (images.ProductsID != null)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase httpPostedFileBase = Request.Files[file];

                    var name = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
                    var fileExtension = Path.GetExtension(httpPostedFileBase.FileName);
                    var nameFull = "Public/Images/Products/" + name + DateTime.Now.ToString("yymmddffff") + fileExtension;
                    Request.Files[file].SaveAs(Server.MapPath("/" + nameFull));
                    images.ProductSlider = nameFull;
                    db.Images.Add(images);
                    db.SaveChanges();
                }
            }else if (images.RowNo!=0)
            {
               
                foreach (string file in Request.Files)
                {
                    images.RowNo++;
                    HttpPostedFileBase httpPostedFileBase = Request.Files[file];

                    var name = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
                    var fileExtension = Path.GetExtension(httpPostedFileBase.FileName);
                    var nameFull = "Public/Images/HomeSlider/" + name + DateTime.Now.ToString("yymmddffff") + fileExtension;
                    Request.Files[file].SaveAs(Server.MapPath("/"+nameFull));
                    images.HomeSlider = nameFull;
                    db.Images.Add(images);
                    db.SaveChanges();
                }
            }
        }

        //BURA BAX TAM DUZELMEYIB
        public ActionResult DeleteImage(int id)
        {
            var image = db.Images.FirstOrDefault(x => x.ImagesId == id);
            if (image != null)
            {
                if (System.IO.File.Exists(image.HomeSlider))
                {
                    System.IO.File.Delete(image.HomeSlider);
                }
                
                if (System.IO.File.Exists(image.ProductSlider))
                {
                    System.IO.File.Delete(image.ProductSlider);
                }
                db.Images.Remove(image);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}