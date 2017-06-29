using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class TweetChannel : TweetModel, IAuthorized
    {
        [Required]
        [StringLength(50)]
        [Index(IsUnique =true)]
        [UniqueTweetChannelName]
        public string Name { get; set; }
        public int? TweetUserId { get; set; }
        public bool Enabled { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<TweetChannelFollower> TweetChannelFollowers { get; set; }
        public TweetChannel()
        {
            this.Tweets = new HashSet<Tweet>();
            this.TweetChannelFollowers = new HashSet<TweetChannelFollower>();
        }
        public bool HasFollower(int TweetUserId)
        {
            return this.TweetChannelFollowers.Where(i => i.TweetUserId == TweetUserId).Count() > 0;
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetChannel> TweetChannels { get; set; }
    }
}