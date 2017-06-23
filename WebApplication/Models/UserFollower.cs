using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UserFollower : TweetModel
    {
        public int TweetUserId { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        [ForeignKey("TweetUser")]
        public int FollowedUserId { get; set; }
        [ForeignKey("FollowedUserId")]
        public virtual TweetUser FollowedUser { get; set; }
        public DateTime FollowTime { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<UserFollower> UserFollowers { get; set; }
    }
}