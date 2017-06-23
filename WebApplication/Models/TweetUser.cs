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
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Key { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [EmailAddress]
        [StringLength(50)]
        [Index(IsUnique = true)]
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
            if (item == null)
                return false;
            return this.Id == item.TweetUserId;
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetUser> TweetUsers { get; set; }
    }
}