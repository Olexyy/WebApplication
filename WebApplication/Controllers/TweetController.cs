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
        {
            ViewBag.Message = "All tweets";
            IEnumerable<Tweet> tweets = this.Db.Tweets.Take(10);
            return View(tweets);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (!this.IsAuthenticated)
                return View("Error");
            ViewBag.Message = "Create tweet";
            return View(new Tweet());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tweet tweet)
        {
            if (this.IsAuthenticated)
            {
                if(!this.ModelState.IsValid)
                    return View(tweet);
                tweet.SetTimeStamps();
                tweet.TweetUser = this.TweetUser;
                this.Db.Tweets.Add(tweet);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if (tweet != null)
                return View(tweet);
            return View("Error");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if(this.IsAuthenticated)
            {
                Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
                ViewBag.Message = "Edit tweet";
                if (tweet != null && this.TweetUser.IsAuthor(tweet))
                    return View(tweet);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult Edit(Tweet tweet)
        {
            if (this.IsAuthenticated && this.TweetUser.IsAuthor(tweet))
            {
                if (!this.ModelState.IsValid)
                    return View(tweet);
                tweet.UpdateTimeStamps();
                this.Db.Entry<Tweet>(tweet).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Details", new { id = tweet.Id });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (this.IsAuthenticated)
            {
                Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
                if (tweet != null && this.TweetUser.IsAuthor(tweet))
                {
                    this.Db.Tweets.Remove(tweet);
                    this.Db.SaveChanges();
                    return RedirectToAction("Details", new { id = tweet.Id });
                }
            }
            return View("Error");
        }
    }
}