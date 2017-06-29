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
        [ReturnUrl(Set = true)]
        [TweetAuthorize]
        public ActionResult Create(int id)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
            if(tweet == null)
                return View("Error");
            TweetComment tweetComment = new TweetComment { TweetId = id, Tweet = tweet };
            return View(tweetComment);
        }
        [HttpPost]
        [TweetAuthorize]
        [ReturnUrl(Get = true)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetComment tweetComment)
        {
            Tweet tweet = this.Db.Tweets.Where(i => i.Id == tweetComment.TweetId).FirstOrDefault();
            // Author is not nessesary
            if(!this.ModelState.IsValid)
                return View(tweetComment);
            tweetComment.SetTimeStamps();
            tweetComment.TweetUser = this.TweetUser;
            this.Db.TweetComments.Add(tweetComment);
            this.Db.SaveChanges();
            if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);
            return RedirectToAction("Details", new { id = tweetComment.Id });
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Details(int id)
        {
            TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
            if (this.TweetUser.IsAuthor(tweetComment))
                return View(tweetComment);
            return View("Error");
        }
        [HttpGet]
        [TweetAuthorize]
        [ReturnUrl(Set = true)]
        public ActionResult Edit(int id)
        {
            TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
            if (this.TweetUser.IsAuthor(tweetComment))
                return View(tweetComment);
            return View("Error");
        }
        [HttpPost]
        [TweetAuthorize]
        [ReturnUrl(Get = true)]
        public ActionResult Edit(TweetComment tweetComment)
        {
            if (this.TweetUser.IsAuthor(tweetComment))
            {
                if (!this.ModelState.IsValid)
                    return View(tweetComment);
                tweetComment.UpdateTimeStamps();
                this.Db.Entry<TweetComment>(tweetComment).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
                    return Redirect(ViewBag.ReturnUrl);
                return RedirectToAction("Details", new { id = tweetComment.Id });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Delete(int id, string returnUrl = null)
        {
            TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
            if (this.TweetUser.IsAuthor(tweetComment))
            {
                this.Db.TweetComments.Remove(tweetComment);
                this.Db.SaveChanges();
                if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Details", "Tweet", new { id = tweetComment.TweetId });
            }
            return View("Error");
        }
    }
}