using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class TweetComment : TweetModel, IAuthorized
    {
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public int? TweetUserId { get; set; }
        public int TweetMessageId { get; set; }
        public bool Enabled { get; set; }
        public virtual TweetMessage TweetMessage { get; set; }
        public virtual TweetUser TweetUser { get; set; }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetComment> TweetComments { get; set; }
    }
}