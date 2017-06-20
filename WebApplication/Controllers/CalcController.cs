using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CalcController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Add(double a, double b)
        {
            this.ViewBag.Operation = "Addition";
            this.ViewBag.Result = a + b;
            return View("Result");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Sub(double a, double b)
        {
            this.ViewBag.Operation = "Substraction";
            this.ViewBag.Result = a - b;
            return View("Result");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Mult(double a, double b)
        {
            this.ViewBag.Operation = "Multiplication";
            this.ViewBag.Result = a * b;
            return View("Result");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Div(double a, double b)
        {
            if (b == 0)
                return View("Error");
            this.ViewBag.Operation = "Division";
            this.ViewBag.Result = a / b;
            return View("Result");
        }
    }
}