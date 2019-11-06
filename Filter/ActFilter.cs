using ZeonEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZeonEcommerce.Filter
{
    public class ActFilter : FilterAttribute, IActionFilter
    {


        ECommerceContext db = new ECommerceContext();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ipAdd = filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            }
            
            var logs = new Logs()
            {
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Info = "OnActionExecuting",
                Time = DateTime.Now,
                UserIp = ipAdd,

            };
        db.Logs.Add(logs);
            db.SaveChanges();
        }
}
}