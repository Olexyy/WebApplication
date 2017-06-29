using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication.Controllers
{
    public class ReturnUrlAttribute : ActionFilterAttribute
    {
        public bool Set { get; set; }
        public bool Get { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.Get && filterContext.HttpContext.Session["ReturnUrl"] != null)
            {
                filterContext.Controller.ViewBag.ReturnUrl = filterContext.HttpContext.Session["ReturnUrl"];
                filterContext.HttpContext.Session["ReturnUrl"] = null;
            }
            if(this.Set)
                filterContext.HttpContext.Session["ReturnUrl"] = filterContext.HttpContext.Request.UrlReferrer.OriginalString;

        }
    }
}