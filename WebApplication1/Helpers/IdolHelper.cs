using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Helpers
{
    public static class IdolHelper
    {
        public static MvcHtmlString IdolsList(this HtmlHelper html, string[] idols, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (var idol in idols)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(idol);
                ul.InnerHtml += li.ToString();
            }
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new MvcHtmlString(ul.ToString());
        }
    }
}