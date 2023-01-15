using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Helpers
{
    public static class ListHelpers
    {
        public static MvcHtmlString CreateList(this HtmlHelper html, string[] items, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach(var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(item);
                ul.InnerHtml += li.ToString();
            }
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return new MvcHtmlString(ul.ToString());
        }
    }
}