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
        private static string BasePath { get {
                string basePath = HttpRuntime.AppDomainAppVirtualPath;
                if (!basePath.EndsWith("/"))
                    basePath += "/";
                return basePath;
            } }

        public static MvcHtmlString TweetFor(this HtmlHelper helper, Tweet tweet)
        {
            string html = "<span class='tweet'>" + tweet.Name + "</span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetLinkFor(this HtmlHelper helper, Tweet tweet)
        {
            string html = "<a href='"+ TweetHelpers.BasePath +"Tweet/View/"+ tweet.Id + "' class='btn btn-default'>" + tweet.Name + "</a>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetUserFor(this HtmlHelper helper, TweetUser user)
        {
            string html = "<span class='tweet-user'>" + user.Email + "</span>";
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString TweetCommentFor(this HtmlHelper helper, TweetComment comment)
        {
            string html = "<div class='tweet-comment'>" + comment.Text + "</div>";
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
            if(!tweet.HasFollower(user.Id))
            {
                html.AppendFormat(@"<a href = '{0}TweetFollower/Create/{1}' class='btn btn-default btn-xs' title='Follow'>
                                <span class='glyphicon glyphicon-bell' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, tweet.Id);
            }
            else
            {
                html.AppendFormat(@"<a href = '{0}TweetFollower/Delete/{1}' class='btn btn-default btn-xs' title='Follow'>
                                <span class='glyphicon glyphicon-remove' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, tweet.TweetFollowers.First(i => i.TweetUserId == user.Id).Id);
            }
            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString TweetChannelFollowWidget(this HtmlHelper helper, TweetChannel tweetChannel, TweetUser user)
        {
            if (user == null || tweetChannel== null || tweetChannel.TweetUserId == user.Id)
                return new MvcHtmlString(String.Empty);
            StringBuilder html = new StringBuilder();
            if (!tweetChannel.HasFollower(user.Id))
            {
                html.AppendFormat(@"<a href = '{0}TweetChannelFollower/Create/{1}' class='btn btn-default btn-xs' title='Follow'>
                                <span class='glyphicon glyphicon-bell' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, tweetChannel.Id);
            }
            else
            {
                html.AppendFormat(@"<a href = '{0}TweetChannelFollower/Delete/{1}' class='btn btn-default btn-xs' title='Follow'>
                                <span class='glyphicon glyphicon-remove' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, tweetChannel.TweetChannelFollowers.First(i => i.TweetUserId == user.Id).Id);
            }
            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString TweetUserFollowWidget(this HtmlHelper helper, TweetUser author, TweetUser user)
        {
            if (user == null || author.Id == user.Id)
                return new MvcHtmlString(String.Empty);
            StringBuilder html = new StringBuilder();
            if (!user.IsFollowing(author.Id))
            {
                html.AppendFormat(@"<a href = '{0}TweetUserFollower/Create/{1}' class='btn btn-default btn-xs' title='Follow'>
                                <span class='glyphicon glyphicon-bell' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, author.Id);
            }
            else
            {
                html.AppendFormat(@"<a href = '{0}TweetUserFollower/Delete/{1}' class='btn btn-default btn-xs' title='Unfollow'>
                                <span class='glyphicon glyphicon-remove' aria-hidden='true'></span>
                                </a>", TweetHelpers.BasePath, user.FollowedUsers.First(i => i.FollowedTweetUserId == user.Id).Id);
            }
            return new MvcHtmlString(html.ToString());
        }
        public static MvcHtmlString TweetDashboardTable(this HtmlHelper helper, TweetUser user)
        {
            StringBuilder html = new StringBuilder();
          
            html.Append(@"<table class='table dashboard user - dashboard'><thead>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Comments
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
                html.Append(item.TweetComments.Count.ToString());
                html.Append(@"</td><td>");
                html.Append(item.Created.ToUniversalTime());
                html.Append(@"</td><td>");
                html.Append(item.Changed.ToUniversalTime());
                html.Append(@"</td><td>");
                html.AppendFormat(@"<a href='{0}Tweet/Edit/{1}' class='btn btn-default'>Edit</a>", TweetHelpers.BasePath, item.Id);
                html.AppendFormat(@"<a href='{0}Tweet/Details/{1}' class='btn btn-default'>Details</a>", TweetHelpers.BasePath, item.Id);
                html.AppendFormat(@"<a href='{0}Tweet/Delete/{1}' class='btn btn-default'>Delete</a>", TweetHelpers.BasePath, item.Id);
                html.Append(@"</td><tr>");
            }
            html.Append(@"</tbody></table>");
            return new MvcHtmlString(html.ToString());
        }
        public static MvcHtmlString TweetCard(this HtmlHelper helper, Tweet tweet, TweetUser user)
        {
            StringBuilder html = new StringBuilder();
            var channel = (tweet.TweetChannel != null) ? tweet.TweetChannel.Name : "none";
            html.AppendFormat(
                @"<div class='card card-tweet'>
                    <div class='card-header'>#{0} {6}</div>
                    <div class='card-block'>
                        <h5 class='card-text'>Channel: {1} {9}
                        <h4 class='card-title'>Text: {2}</h4>
                        <p class='card-text'>Total comments: {3}.</p>
                        <p class='card-text'>Author: {4}. {10}</p>
                        <p class='card-text'>Created: {5}.</p>
                        <a href='{7}Tweet/View/{8}' class='btn btn-default'>View</a>
                    </div>
                </div>", tweet.Name, channel, tweet.Text, tweet.TweetComments.Count.ToString(), tweet.TweetUser.Email, 
                tweet.Created.ToUniversalTime(), helper.TweetFollowWidget(tweet, user).ToString(), TweetHelpers.BasePath, tweet.Id,
                helper.TweetChannelFollowWidget(tweet.TweetChannel, user).ToString(), helper.TweetUserFollowWidget(tweet.TweetUser, user));
            return new MvcHtmlString(html.ToString());
        }
        public static MvcHtmlString TweetChannelCard(this HtmlHelper helper, TweetChannel tweetChannel, TweetUser user)
        {
            StringBuilder html = new StringBuilder();
            StringBuilder tweetLinks = new StringBuilder();
            foreach (Tweet tweet in tweetChannel.Tweets)
                tweetLinks.Append(helper.TweetLinkFor(tweet));
            html.AppendFormat(
                @"<div class='card card-tweet'>
                    <div class='card-header'>#{0}</div>
                    <div class='card-block'>
                        <h4 class='card-title'>Total tweets: {1}</h4>
                        <p class='card-text'>Total comments: {2}.</p>
                        <p class='card-text'>Author: {3}.</p>
                        <p class='card-text'>Created: {4}.</p>
                        {5}<a href='{6}TweetChannel/View/{7}' class='btn btn-default'>View</a>
                    </div>
                </div>", tweetChannel.Name, tweetChannel.Tweets.Count.ToString(), tweetLinks.ToString(), tweetChannel.TweetUser.Email,
                tweetChannel.Created.ToUniversalTime(), helper.TweetChannelFollowWidget(tweetChannel, user).ToString(), TweetHelpers.BasePath, tweetChannel.Id);
            return new MvcHtmlString(html.ToString());
        }
    }
}