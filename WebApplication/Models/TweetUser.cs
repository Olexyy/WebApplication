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
        [UniqueTweetUser]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<TweetChannel> TweetChannels { get; set; }
        [InverseProperty("FollowedTweetUser")]
        public virtual ICollection<TweetUserFollower> Followers { get; set; }
        [InverseProperty("FollowerTweetUser")]
        public virtual ICollection<TweetUserFollower> FollowedUsers { get; set; }
        public virtual ICollection<TweetChannelFollower> FollowedChannels { get; set; }
        public virtual ICollection<TweetFollower> FollowedTweets { get; set; }
        public TweetUser()
        {
            this.Tweets = new HashSet<Tweet>();
            this.TweetChannels = new HashSet<TweetChannel>();
            this.Followers = new HashSet<TweetUserFollower>();
            this.FollowedUsers = new HashSet<TweetUserFollower>();
            this.FollowedChannels = new HashSet<TweetChannelFollower>();
            this.FollowedTweets = new HashSet<TweetFollower>();
        }
        public bool IsAuthor(IAuthorized item)
        {
            if (item == null)
                return false;
            return this.Id == item.TweetUserId;
        }
        public bool HasFollower(int TweetUserId)
        {
            return this.Followers.Where(i => i.FollowedTweetUserId == TweetUserId).Count() > 0;
        }
        public bool IsFollowing(int TweetUserId)
        {
            return this.FollowedUsers.Where(i => i.FollowedTweetUserId == TweetUserId).Count() > 0;
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetUser> TweetUsers { get; set; }
    }
}