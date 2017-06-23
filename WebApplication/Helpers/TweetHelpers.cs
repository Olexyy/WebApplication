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
        public static MvcHtmlString TweetFor(this HtmlHelper helper, Tweet tweet)
        {
            string html = "<span class='tweet'>" + tweet.Name + "</span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetUserFor(this HtmlHelper helper, TweetUser user)
        {
            string html = "<span class='tweet-user'>" + user.Email + "</span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetMessageFor(this HtmlHelper helper, TweetMessage message)
        {
            string html = "<div class='tweet-comment'>" + message.Text + "</div>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetDescription(this HtmlHelper helper, string description)
        {
            string html = "<div class='tweet-description text-muted'>" + description + "</div>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetYesNoFor(this HtmlHelper helper, bool selected)
        {
            string html = "";
            if (selected)
                html = "<span class='tweet-description text-muted'> Yes </span>";
            else
                html = "<span class='tweet-description text-muted'> No </span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetFollowWidget(this HtmlHelper helper, Tweet tweet, TweetUser user)
        {
            if(user == null || tweet.TweetUserId == user.Id)
                return new MvcHtmlString(String.Empty);
            StringBuilder html = new StringBuilder();
            var basePath = HttpRuntime.AppDomainAppVirtualPath;
            if(!tweet.HasFollower(user.Id))
            {
                html.AppendFormat(@"<a href = '{0}/TweetFollower/Create/{1}' class='btn btn-default btn-lg' title='Follow'>
                                <span class='glyphicon glyphicon-bell' aria-hidden='true'></span>
                                </a>", basePath, tweet.Id);
            }
            else
            {
                html.AppendFormat(@"<a href = '{0}/TweetFollower/Delete/{1}' class='btn btn-default btn-lg' title='Follow'>
                                <span class='glyphicon glyphicon-remove' aria-hidden='true'></span>
                                </a>", basePath, tweet.TweetFollowers.First(i => i.TweetUserId == user.Id).Id);
            }
            return new MvcHtmlString(html.ToString());
        }
                    

        public static MvcHtmlString TweetDashboardTable(this HtmlHelper helper, TweetUser user)
        {
            StringBuilder html = new StringBuilder();
            var basePath = HttpRuntime.AppDomainAppVirtualPath;

            html.Append(@"<table class='table dashboard user - dashboard'><thead>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Messages
                                </th>
                                <th>
                                    Created
                                </th>
                                <th>
                                    Changed
                                </th>
                                <th>
                                    Actions
                                </th>
                            </tr>
                        </thead>
                        <tbody>");
            foreach(var item in user.Tweets)
            {
                html.Append(@"<tr><td>");
                html.Append(item.Name);
                html.Append(@"</td><td>");
                html.Append(item.TweetMessages.Count.ToString());
                html.Append(@"</td><td>");
                html.Append(item.Created.ToUniversalTime());
                html.Append(@"</td><td>");
                html.Append(item.Changed.ToUniversalTime());
                html.Append(@"</td><td>");
                html.AppendFormat(@"<a href='{0}/Tweet/Edit/{1}' class='btn btn-default'>Edit</a>",basePath, item.Id);
                html.AppendFormat(@"<a href='{0}/Tweet/Details/{1}' class='btn btn-default'>Details</a>", basePath, item.Id);
                html.AppendFormat(@"<a href='{0}/Tweet/Delete/{1}' class='btn btn-default'>Delete</a>", basePath,item.Id);
                html.Append(@"</td><tr>");
            }
            html.Append(@"</tbody></table>");
            return new MvcHtmlString(html.ToString());
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
        public static MvcHtmlString TweetCard(this HtmlHelper helper, Tweet tweet, TweetUser user)
        {
            StringBuilder html = new StringBuilder();
            var basePath = HttpRuntime.AppDomainAppVirtualPath;
            var text = (tweet.TweetMessages.FirstOrDefault() != null) ? tweet.TweetMessages.FirstOrDefault().Text : String.Empty;
            html.AppendFormat(
                @"<div class='card card-tweet'>
                    <div class='card-header'>#{0}</div>
                    <div class='card-block'>
                        <h4 class='card-title'>Last message: {1}</h4>
                        <p class='card-text'>Total messages: {2}.</p>
                        <p class='card-text'>Author: {3}.</p>
                        <p class='card-text'>Created: {4}.</p>
                        {5}<a href='{6}/Tweet/View/{7}' class='btn btn-default'>View</a>
                    </div>
                </div>", tweet.Name, text, tweet.TweetMessages.Count.ToString(), tweet.TweetUser.Email, 
                tweet.Created.ToUniversalTime(), helper.TweetFollowWidget(tweet, user).ToString(), basePath, tweet.Id);
            return new MvcHtmlString(html.ToString());
        }
    }
}