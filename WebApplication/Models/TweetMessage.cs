using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class TweetMessage : TweetModel, IAuthorized
    {
        [Required]
        [StringLength(200)]
        public string Text { get; set; }
        public int? TweetUserId { get; set; }
        public int TweetId { get; set; }
        public bool Enabled { get; set; }
        public virtual Tweet Tweet { get; set; }
        public virtual TweetUser TweetUser { get; set; }
        public virtual ICollection<TweetComment> TweetComments { get; set; }
        public TweetMessage()
        {
            this.TweetComments = new HashSet<TweetComment>();
        }
    }
    public partial class TweetDbContext : DbContext
    {
        public DbSet<TweetMessage> TweetMessages { get; set; }
    }
}