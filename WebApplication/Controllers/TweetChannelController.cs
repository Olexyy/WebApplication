using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetChannelController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<TweetChannel> tweetsChannels = this.Db.TweetChannels;
            return View(tweetsChannels);
        }
        [HttpGet]
        [TweetAuthorize]
        [ReturnUrl(Set = true)]
        public ActionResult Create()
        {
            return View(new TweetChannel());
        }
        [HttpPost]
        [TweetAuthorize]
        [ReturnUrl(Get = true)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetChannel tweetChannel)
        {
            if(!this.ModelState.IsValid)
                return View(tweetChannel);
            tweetChannel.SetTimeStamps();
            tweetChannel.TweetUser = this.TweetUser;
            this.Db.TweetChannels.Add(tweetChannel);
            this.Db.SaveChanges();
            if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);
            return RedirectToAction("Details", new { id = tweetChannel.Id });
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Details(int id)
        {
            TweetChannel tweetChannel = this.Db.TweetChannels.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannel != null)
                return View(tweetChannel);
            return View("Error");
        }
        [HttpGet]
        public ActionResult View(int id)
        {
            TweetChannel tweetChannel = this.Db.TweetChannels.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannel != null)
                return View(tweetChannel);
            return View("Error");
        }
        [HttpGet]
        [TweetAuthorize]
        [ReturnUrl(Set = true)]
        public ActionResult Edit(int id)
        {
            TweetChannel tweetChannel = this.Db.TweetChannels.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannel != null && this.TweetUser.IsAuthor(tweetChannel))
                return View(tweetChannel);
            return View("Error");
        }
        [HttpPost]
        [TweetAuthorize]
        [ReturnUrl(Get = true)]
        public ActionResult Edit(Tweet tweetChannel)
        {
            if (this.TweetUser.IsAuthor(tweetChannel))
            {
                if (!this.ModelState.IsValid)
                    return View(tweetChannel);
                tweetChannel.UpdateTimeStamps();
                this.Db.Entry<Tweet>(tweetChannel).State = System.Data.Entity.EntityState.Modified;
                this.Db.SaveChanges();
                if (!String.IsNullOrEmpty(ViewBag.ReturnUrl))
                    return Redirect(ViewBag.ReturnUrl);
                return RedirectToAction("Index", "TweetDashboard");
            }
            return View("Error");
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Delete(int id)
        {
            TweetChannel tweetChannel = this.Db.TweetChannels.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannel != null && this.TweetUser.IsAuthor(tweetChannel))
            {
                this.Db.TweetChannels.Remove(tweetChannel);
                this.Db.SaveChanges();
                return RedirectToAction("Index", "TweetDashboard");
            }
            return View("Error");
        }
    }
}