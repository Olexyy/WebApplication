using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication.Controllers
{
    public class StatisticsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string logsPath = HttpContext.Current.Server.MapPath("~/App_Data/Logs/log.txt");
            string client = HttpContext.Current.Request.UserHostAddress;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string time = DateTime.Now.ToString();
            string record = String.Format("{0},{1},{2},{3}" + Environment.NewLine, client, time, controllerName, actionName);
            File.AppendAllText(logsPath, record);
            filterContext.Controller.ViewBag.Statistics = File.ReadAllLines(logsPath);
        }
    }
}