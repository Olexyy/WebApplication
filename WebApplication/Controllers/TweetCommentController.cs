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
            TweetMessage tweetMessage = this.Db.TweetMessages.Where(i => i.Id == id).FirstOrDefault();
            if(tweetMessage == null)
                return View("Error");
            TweetComment tweetComment = new TweetComment { TweetMessageId = id, TweetMessage = tweetMessage };
            return View(tweetComment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetComment tweetComment)
        {
            TweetMessage tweetMessage = this.Db.TweetMessages.Where(i => i.Id == tweetComment.TweetMessageId).FirstOrDefault();
            // Author is not nessesary
            if (this.IsAuthenticated)
            {
                if(!this.ModelState.IsValid)
                    return View(tweetComment);
                tweetComment.SetTimeStamps();
                tweetComment.TweetUser = this.TweetUser;
                this.Db.TweetComments.Add(tweetComment);
                this.Db.SaveChanges();
                return RedirectToAction("Details", "TweetMessage", new { id = tweetComment.TweetMessageId });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
            if (this.IsAuthenticated && tweetComment != null && this.TweetUser.IsAuthor(tweetComment))
                return View(tweetComment);
            return View("Error");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (this.IsAuthenticated)
            {
                TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
                if (tweetComment != null && this.TweetUser.IsAuthor(tweetComment))
                    return View(tweetComment);
            }
            return View("Error");
        }
        [HttpPost]
        public ActionResult Edit(TweetComment tweetComment)
        {
            if (this.IsAuthenticated && this.TweetUser.IsAuthor(tweetComment))
            {
                if (!this.ModelState.IsValid)
                    return View(tweetComment);
                tweetComment.UpdateTimeStamps();
                this.Db.Entry<TweetComment>(tweetComment).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Details", new { id = tweetComment.Id });
            }
            return View("Error");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (this.IsAuthenticated)
            {
                TweetComment tweetComment = this.Db.TweetComments.Where(i => i.Id == id).FirstOrDefault();
                if (tweetComment != null && this.TweetUser.IsAuthor(tweetComment))
                {
                    this.Db.TweetComments.Remove(tweetComment);
                    this.Db.SaveChanges();
                    return RedirectToAction("Details", "TweetMessage", new { id = tweetComment.TweetMessageId });
                }
            }
            return View("Error");
        }
    }
}