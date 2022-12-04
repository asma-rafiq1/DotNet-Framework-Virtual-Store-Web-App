using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.CustomHtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        /// <summary>
        /// Create an img tag for displaying an image
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static IHtmlString Image(this HtmlHelper helper,string src, string alt)
        {
            TagBuilder tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tagBuilder.Attributes.Add("alt", alt);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}