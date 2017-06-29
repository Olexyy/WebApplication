using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TweetUserFollower : TweetModel
    {
        public int? FollowerTweetUserId { get; set; }
        public int? FollowedTweetUserId { get; set; }
        public virtual TweetUser FollowerTweetUser { get; set; }
        public virtual TweetUser FollowedTweetUser { get; set; }
        public DateTime FollowTime { get; set; }
        public DateTime LastFollowTime { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetUserFollower> TweetUserFollowers { get; set; }
    }
}