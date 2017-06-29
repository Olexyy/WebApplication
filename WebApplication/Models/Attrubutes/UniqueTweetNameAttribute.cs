using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UniqueTweetNameAttribute : ValidationAttribute
    { 
        public override bool IsValid(object value)
        {
            var tweet = TweetDbContext.Instance().Tweets.SingleOrDefault(i => i.Name == (string)value);
            return tweet == null;
        }
    }
}