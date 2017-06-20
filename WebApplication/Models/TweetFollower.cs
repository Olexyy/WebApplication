using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TweetFollower : TweetModel
    {
        public int TweetUserId { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public int TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetFollower> TweetFollowers { get; set; }
    }
}