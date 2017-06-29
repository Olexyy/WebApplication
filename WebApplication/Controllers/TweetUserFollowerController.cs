using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetUserFollowerController : TweetGenericController
    {
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Create(int id)
        {
            TweetUser tweetUser = this.Db.TweetUsers.Where(i => i.Id == id).FirstOrDefault();
            if (tweetUser != null)
            {
                TweetUserFollower tweetUserFollower = new TweetUserFollower {
                    FollowedTweetUserId = tweetUser.Id,
                    FollowedTweetUser = tweetUser,
                    FollowerTweetUserId = this.TweetUser.Id,
                    FollowerTweetUser = this.TweetUser,
                    FollowTime = DateTime.Now, LastFollowTime = DateTime.Now };
                tweetUserFollower.SetTimeStamps();
                this.Db.TweetUserFollowers.Add(tweetUserFollower);
                this.Db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Delete(int id)
        {
            TweetUserFollower tweetUserFollower = this.Db.TweetUserFollowers.Where(i => i.Id == id).FirstOrDefault();
            if (tweetUserFollower != null)
            {
                this.Db.TweetUserFollowers.Remove(tweetUserFollower);
                this.Db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}