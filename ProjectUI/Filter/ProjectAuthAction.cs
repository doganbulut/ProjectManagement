using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ProjectUI
{
    public class ProjectAuthAction: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (((DataLayer.tblDeveloper)filterContext.HttpContext.Session["activeUser"]).YETKI == 0)
                base.OnActionExecuting(filterContext);
            else
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Exception");
                redirectTargetDictionary.Add("controller", "Project");
                redirectTargetDictionary.Add("Message", "Bu işlem için yetkiniz bulunmamaktadır....");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
                
        }
      
    }
}