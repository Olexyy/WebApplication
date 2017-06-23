using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class TweetDashboardController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if(this.IsAuthenticated)
                return View(this.TweetUser);
            return View("Error");
        }
    }
}