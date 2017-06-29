using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class TweetAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TweetGenericController controller = filterContext.Controller as TweetGenericController;
            if (!controller.IsAuthenticated)
                filterContext.Result = new ViewResult { ViewName = "Denied",
                    ViewData = controller.ViewData,
                    TempData = controller.TempData };
        }
    }
}