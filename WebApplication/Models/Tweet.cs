using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Tweet : TweetModel, IAuthorized
    {
        public string Name { get; set; }
        public int? TweetUserId { get; set; }
        public bool Enabled { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public virtual ICollection<TweetComment>  TweetComments { get; set; }
        public virtual ICollection<TweetFollower> TweetFollowers { get; set; }
        public Tweet()
        {
            this.TweetComments = new HashSet<TweetComment>();
            this.TweetFollowers = new HashSet<TweetFollower>();
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }
    }
}