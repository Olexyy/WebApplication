using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public interface IAuthorized
    {
        int TweetUserId { get; set; }
    }
    public abstract class TweetModel
    {
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