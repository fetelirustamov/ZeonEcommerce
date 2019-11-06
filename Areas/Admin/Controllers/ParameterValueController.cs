using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeonEcommerce.Models;
using ZeonEcommerce.ViewModel;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class ParameterValueController : AdminControllerBase
    {
        // GET: Admin/ParameterValue
        public ActionResult Index()
        {
            return View(db.ParameterValue.ToList());
        }

        public ActionResult AddParameterValue()
        {
            return View(db.ParameterType.ToList());
        }

        [HttpPost]
        public ActionResult AddParameterValue(ParameterValue parameterValue)
        {
            db.ParameterValue.Add(parameterValue);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult EditParameterValue(int id)
        {
            ParameterValueType parameterValueType = new ParameterValueType();
            parameterValueType.ParameterType = db.ParameterType.ToList();
            parameterValueType.ParameterValue = db.ParameterValue.FirstOrDefault(x => x.ParameterValueId == id);
            return View(parameterValueType);
        }

        [HttpPost]
        public ActionResult EditParameterValue(ParameterValue parameterValue)
        {
            var ParameterValue = db.ParameterValue.FirstOrDefault(x => x.ParameterValueId == parameterValue.ParameterValueId);
            ParameterValue.Name = parameterValue.Name;
            ParameterValue.ParameterTypeID = parameterValue.ParameterTypeID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var parameterValue = db.ParameterValue.FirstOrDefault(x => x.ParameterValueId == id);
            if (parameterValue != null)
            {

                db.ParameterValue.Remove(parameterValue);
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