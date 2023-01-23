using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Helpers
{
    public static class MoviesHelper
    {
        public static MvcHtmlString MoviesList(this HtmlHelper html, string[] movies, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");

            foreach (var movie in movies)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(movie);
                ul.InnerHtml += li.ToString();
            }

            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return new MvcHtmlString(ul.ToString());
        }
    }
}