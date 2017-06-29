using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UniqueTweetUserAttribute : ValidationAttribute
    { 
        public override bool IsValid(object value)
        {
            var user = TweetDbContext.Instance().TweetUsers.SingleOrDefault(i => i.Email == (string)value);
            return user == null;
        }
    }
}