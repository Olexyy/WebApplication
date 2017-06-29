using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public partial class TweetDbContext : DbContext
    {
        private static TweetDbContext Db { get; set; }
        public TweetDbContext(): base("DefaultConnection") { }
        public static TweetDbContext Instance()
        {
            if (TweetDbContext.Db == null)
                TweetDbContext.Db = new TweetDbContext();
            return TweetDbContext.Db;
        }
    }
}