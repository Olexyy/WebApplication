using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Fill()
        {
            ViewBag.Message = "Fill in Your data";
            Data data = new Data();
            return View(data);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillAsync()
        {
            ViewBag.Message = "Fill in Your data";
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AjaxAction(string name , string age)
        {
            var data = new
            {
                Name = name,
                Age = age,
            };
            return Json(data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Fill(Data data)
        {
            if(!ModelState.IsValid)
                return View(data);
            ViewBag.Message = "Filled in data";
            return View("Submitted", data);
        }
    }
}