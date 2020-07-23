using shopee_tedu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;

namespace shopee_tedu.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if(session==null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["Alert_message"] = message;
            if(type == "success")
            {
                TempData["Alert_Type"] = "alert-success";
            }
            if(type == "delete")
            {
                TempData["Alert_Type"] = "alert-warning";
            }
        }
    }
}