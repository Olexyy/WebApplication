using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace System.Web.Mvc
{
    public static class TweetHelpers
    {
        public static MvcHtmlString TweetUserFor(this HtmlHelper helper, TweetUser user)
        {
            string html = "<span class='tweet-user'>" + user.Email + "</span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetCommentFor(this HtmlHelper helper, TweetComment comment, string tag = "div")
        {
            string html = "<div class='tweet-comment'>" + comment.Text + "</div>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetDescription(this HtmlHelper helper, string description)
        {
            string html = "<div class='tweet-description text-muted'>" + description + "</div>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString UnorderedList(this HtmlHelper helper,
            string id, List<SelectListItem> items, string classname = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul id='{0}'>", id);
            foreach (SelectListItem item in items)
                sb.AppendFormat("<li><a href='#' id='{0}' class='{1}'>{2}</a></li>", item.Value, classname, item.Text);
            sb.AppendLine("</ul>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}