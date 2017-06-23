using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetFollowerController : TweetGenericController
    {
        [HttpGet]
        public ActionResult Create(int id)
        {
            if(this.IsAuthenticated)
            {
                Tweet tweet = this.Db.Tweets.Where(i => i.Id == id).FirstOrDefault();
                if (tweet != null)
                {
                    TweetFollower tweetFollower = new TweetFollower {
                        TweetId = id,
                        TweetUserId = this.TweetUser.Id,
                        FollowTime = DateTime.Now };
                    tweetFollower.SetTimeStamps();
                    this.Db.TweetFollowers.Add(tweetFollower);
                    this.Db.SaveChanges();
                }
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        public ActionResult Delete(int id)
        {
            if (this.IsAuthenticated)
            {
                TweetFollower tweetFollower = this.Db.TweetFollowers.Where(i => i.Id == id).FirstOrDefault();
                if (tweetFollower != null)
                {
                    this.Db.TweetFollowers.Remove(tweetFollower);
                    this.Db.SaveChanges();
                }
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}