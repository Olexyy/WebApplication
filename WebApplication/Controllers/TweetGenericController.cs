using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication.Controllers
{
    public abstract class TweetGenericController : Controller
    {
        public TweetDbContext Db { get; set; }
        public bool IsAuthenticated { get; set; }
        public TweetUser TweetUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Db = TweetDbContext.Instance();
            if (this.User.Identity.IsAuthenticated)
            {
                string id = this.User.Identity.GetUserId();
                string email = this.User.Identity.Name;
                this.TweetUser = this.Db.TweetUsers.Where(i => i.Key == id).FirstOrDefault();
                if(this.TweetUser == null)
                {
                    TweetUser tweetUser = new TweetUser {
                        Key = id, Email = email };
                    tweetUser.SetTimeStamps();
                    this.Db.TweetUsers.Add(tweetUser);
                    this.Db.SaveChanges();
                    this.TweetUser = tweetUser;
                }
                if(this.TweetUser.Email != email)
                {
                    this.TweetUser.Email = email;
                    this.TweetUser.UpdateTimeStamps();
                    Db.Entry<TweetUser>(this.TweetUser).State = EntityState.Modified;
                    this.Db.SaveChanges();
                }
            }
            this.IsAuthenticated = this.TweetUser != null;
            this.ViewBag.TweetUser = this.TweetUser;
            this.ViewBag.IsAuthenticated = this.IsAuthenticated;
        }
    }
}