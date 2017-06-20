using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication.Models
{
    public class TweetUser : TweetModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<UserFollower> Followers { get; set; }
        public virtual ICollection<UserFollower> FollowedUsers { get; set; }
        public virtual ICollection<TweetFollower> FollowedTweets { get; set; }
        public TweetUser()
        {
            this.Tweets = new HashSet<Tweet>();
            this.Followers = new HashSet<UserFollower>();
            this.FollowedUsers = new HashSet<UserFollower>();
            this.FollowedTweets = new HashSet<TweetFollower>();
        }
        public bool IsAuthor(IAuthorized item)
        {
            return this.Id == item.TweetUserId;
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetUser> TweetUsers { get; set; }
    }
}