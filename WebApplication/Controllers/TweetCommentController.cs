using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetCommentController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Create(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if(tweet == null)
                return View("Error");
            ViewBag.Message = "Add comment";
            TweetComment tweetComment = new TweetComment { TweetId = id, Tweet = tweet };
            return View(tweetComment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetComment tweetComment)
        {
            if (this.IsAuthenticated)
            {
                if(!this.ModelState.IsValid)
                    return View(tweetComment);
                tweetComment.SetTimeStamps();
                tweetComment.TweetUser = this.TweetUser;
                this.Db.TweetComments.Add(tweetComment);
                this.Db.SaveChanges();
                return RedirectToAction("Details", "Tweet", new { id = tweetComment.TweetId });
            }
            return View("Error");
        }
    }
}