using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Index()
        { // TODO: pager
            IEnumerable<Tweet> tweets = this.Db.Tweets;
            return View(tweets);
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Create()
        {
            return View(new Tweet());
        }
        [HttpPost]
        [TweetAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tweet tweet)
        {
            if(!this.ModelState.IsValid)
                return View(tweet);
            tweet.SetTimeStamps();
            tweet.TweetUser = this.TweetUser;
            this.Db.Tweets.Add(tweet);
            this.Db.SaveChanges();
            return RedirectToAction("Index", "TweetDashboard");
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Details(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if (tweet != null)
                return View(tweet);
            return View("Error");
        }
        [HttpGet]
        public ActionResult View(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if (tweet != null)
                return View(tweet);
            return View("Error");
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Edit(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if (tweet != null && this.TweetUser.IsAuthor(tweet))
                return View(tweet);
            return View("Error");
        }
        [HttpPost]
        [TweetAuthorize]
        public ActionResult Edit(Tweet tweet)
        {
            if (this.TweetUser.IsAuthor(tweet))
            {
                if (!this.ModelState.IsValid)
                    return View(tweet);
                tweet.UpdateTimeStamps();
                this.Db.Entry<Tweet>(tweet).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index", "TweetDashboard");
            }
            return View("Error");
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Delete(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if (tweet != null && this.TweetUser.IsAuthor(tweet))
            {
                this.Db.Tweets.Remove(tweet);
                this.Db.SaveChanges();
                return RedirectToAction("Index", "TweetDashboard");
            }
            return View("Error");
        }
    }
}