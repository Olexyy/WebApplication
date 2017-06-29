using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TweetChannelFollower : TweetModel
    {
        public int TweetUserId { get; set; }
        public int TweetChannelId { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public virtual TweetChannel TweetChannel { get; set; }
        public DateTime FollowTime { get; set; }
        public DateTime LastFollowTime { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetChannelFollower> TweetChannelFollowers { get; set; }
    }
}