using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Tweet : TweetModel, IAuthorized
    {
        [Required]
        [StringLength(50)]
        [UniqueTweetName]
        [Index(IsUnique =true)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public int? TweetUserId { get; set; }
        public int? TweetChannelId { get; set; }
        public bool Enabled { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public virtual TweetChannel TweetChannel { get; set; }
        public virtual ICollection<TweetComment> TweetComments { get; set; }
        public virtual ICollection<TweetFollower> TweetFollowers { get; set; }
        public Tweet()
        {
            this.TweetComments = new HashSet<TweetComment>();
            this.TweetFollowers = new HashSet<TweetFollower>();
        }
        public bool HasFollower(int TweetUserId)
        {
            return this.TweetFollowers.Where(i => i.TweetUserId == TweetUserId).Count() > 0;
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }
    }
}