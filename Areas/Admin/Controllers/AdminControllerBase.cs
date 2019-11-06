using ZeonEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZeonEcommerce.Areas.Admin.Controllers
{
    public class AdminControllerBase : Controller
    {
            protected ECommerceContext db = new ECommerceContext();
        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin = false;
            if (requestContext.HttpContext.Session["AdminloginUser"]==null)
            {
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");
            }
            else
            {
             IsLogin = true;
            base.Initialize(requestContext);
            }
            
        }
    }
}