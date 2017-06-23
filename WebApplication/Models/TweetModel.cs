using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public interface IAuthorized
    {
        int? TweetUserId { get; set; }
    }
    public abstract class TweetModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public int Weight { get; set; }

        public void SetTimeStamps()
        {
            this.Created = this.Changed = DateTime.Now;
        }
        public void UpdateTimeStamps()
        {
            this.Changed = DateTime.Now;
        }
    }
}