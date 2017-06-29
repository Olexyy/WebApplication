using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TweetChannelFollowerController : TweetGenericController
    {
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Create(int id)
        {
            TweetChannel tweetChannel = this.Db.TweetChannels.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannel != null)
            {
                TweetChannelFollower tweetChannelFollower = new TweetChannelFollower {
                    TweetChannelId = id,
                    TweetUserId = this.TweetUser.Id,
                    FollowTime = DateTime.Now, LastFollowTime = DateTime.Now };
                tweetChannelFollower.SetTimeStamps();
                this.Db.TweetChannelFollowers.Add(tweetChannelFollower);
                this.Db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
        [HttpGet]
        [TweetAuthorize]
        public ActionResult Delete(int id)
        {
            TweetChannelFollower tweetChannelFollower = this.Db.TweetChannelFollowers.Where(i => i.Id == id).FirstOrDefault();
            if (tweetChannelFollower != null)
            {
                this.Db.TweetChannelFollowers.Remove(tweetChannelFollower);
                this.Db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}