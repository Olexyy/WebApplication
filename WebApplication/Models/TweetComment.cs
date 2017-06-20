using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TweetComment : TweetModel
    {
        public string Text { get; set; }
        public int TweetUserId { get; set; }
        public int TweetId { get; set; }
        public bool Enabled { get; set; }
        public virtual Tweet Tweet { get; set; }
        public virtual TweetUser TweetUser { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetComment> TweetComments { get; set; }
    }
}