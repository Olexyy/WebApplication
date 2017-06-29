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
        [TweetAuthorize]
        public ActionResult Index()
        {
            return View(this.TweetUser);
        }
    }
}