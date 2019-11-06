using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class ProductParametersController : AdminControllerBase
    {
        // GET: Admin/ProductParameters
        public ActionResult Index()
        {
            return View(db.ProductParameters.ToList());
        }

        public ActionResult AddProductParameters(int? id)
        {
            ProductParamTypeValue viewModel = new ProductParamTypeValue();
            viewModel.Products = db.Products.ToList();
            viewModel.ParameterTypes = db.ParameterType.ToList();
            viewModel.ParameterValues = db.ParameterValue.ToList();
            viewModel.ProductsID = id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddProductParameters(ProductParameters productParameters)
        {
            db.ProductParameters.Add(productParameters);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditProductParameters(int id)
        {
            ProductParamTypeValue viewModel = new ProductParamTypeValue();
            viewModel.Products = db.Products.ToList();
            viewModel.ParameterTypes = db.ParameterType.ToList();
            viewModel.ParameterValues = db.ParameterValue.ToList();
            viewModel.ProductParameters = db.ProductParameters.FirstOrDefault(x => x.ProductParametersId == id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProductParameters(ProductParameters productParameters)
        {
            var prodParam = db.ProductParameters.FirstOrDefault(x=>x.ProductParametersId==productParameters.ProductParametersId);
            prodParam.ProductsID = productParameters.ProductsID;
            prodParam.ParameterValueID = productParameters.ParameterValueID;
            prodParam.ParameterTypeID = productParameters.ParameterTypeID;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var paramsProduct = db.ProductParameters.FirstOrDefault(x => x.ProductParametersId == id);
            if (paramsProduct != null)
            {
                db.ProductParameters.Remove(paramsProduct);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return HttpNotFound();
            }
        }

        public PartialViewResult ParameterTypeListWidget(int id)
        {
            
            Session["CategoryID"] = id;
            var categoryId = 0;
            var products = db.Products.Where(x => x.ProductsId == id).FirstOrDefault();
            if (products.Categories.SubCategoryID==0)
            {
                categoryId = products.CategoriesID;
            }
            else
            {
                categoryId = products.Categories.SubCategoryID;
            }
            var paramType = db.ParameterType.Where(x => x.CategoriesID == categoryId).ToList();
            

            return PartialView(paramType);
        }

        public PartialViewResult ParameterValueListWidget(int id)
        {

            Session["ParamID"] = id;
            var paramValue = db.ParameterValue.Where(x => x.ParameterTypeID == id).ToList();

            return PartialView(paramValue);
        }

    }
}