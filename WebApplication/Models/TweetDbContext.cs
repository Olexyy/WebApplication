using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public partial class TweetDbContext : DbContext
    {
        public TweetDbContext(): base("DefaultConnection") { }
        public static TweetDbContext Create()
        {
            return new TweetDbContext();
        }
    }
}