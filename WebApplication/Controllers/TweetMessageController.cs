using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetMessageController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Create(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if(tweet == null || !this.IsAuthenticated)
                return View("Error");
            TweetMessage tweetMessage = new TweetMessage { TweetId = id, Tweet = tweet };
            return View(tweetMessage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetMessage tweetMessage)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == tweetMessage.TweetId).FirstOrDefault();
            if (this.IsAuthenticated && this.TweetUser.IsAuthor(tweet))
            {
                if(!this.ModelState.IsValid)
                    return View(tweetMessage);
                tweetMessage.SetTimeStamps();
                tweetMessage.TweetUser = this.TweetUser;
                this.Db.TweetMessages.Add(tweetMessage);
                this.Db.SaveChanges();
                return RedirectToAction("Details", "Tweet", new { id = tweetMessage.TweetId });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            TweetMessage tweetMessage = this.Db.TweetMessages.Where(i => i.Id == id).FirstOrDefault();
            if (this.IsAuthenticated && this.TweetUser.IsAuthor(tweetMessage))
                return View(tweetMessage);
            return View("Error");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (this.IsAuthenticated)
            {
                TweetMessage tweetMessage = this.Db.TweetMessages.Where(i => i.Id == id).FirstOrDefault();
                if (this.TweetUser.IsAuthor(tweetMessage))
                    return View(tweetMessage);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult Edit(TweetMessage tweetMessage)
        {
            if (this.IsAuthenticated && this.TweetUser.IsAuthor(tweetMessage))
            {
                if (!this.ModelState.IsValid)
                    return View(tweetMessage);
                tweetMessage.UpdateTimeStamps();
                this.Db.Entry<TweetMessage>(tweetMessage).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Details", new { id = tweetMessage.Id });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (this.IsAuthenticated)
            {
                TweetMessage tweetMessage = this.Db.TweetMessages.Where(i => i.Id == id).FirstOrDefault();
                if (this.TweetUser.IsAuthor(tweetMessage))
                {
                    this.Db.TweetMessages.Remove(tweetMessage);
                    this.Db.SaveChanges();
                    return RedirectToAction("Details", "Tweet", new { id = tweetMessage.TweetId });
                }
            }
            return View("Error");
        }
    }
}