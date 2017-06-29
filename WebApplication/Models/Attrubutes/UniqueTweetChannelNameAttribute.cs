using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UniqueTweetChannelNameAttribute : ValidationAttribute
    { 
        public override bool IsValid(object value)
        {
            var tweetChannel = TweetDbContext.Instance().TweetChannels.SingleOrDefault(i => i.Name == (string)value);
            return tweetChannel == null;
        }
    }
}