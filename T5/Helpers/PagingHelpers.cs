using System.Text;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using T5.Models;

//.............................
namespace T5.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.PagesCount; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("button", "");
                tag.MergeAttribute("type", "button");
                tag.MergeAttribute("onclick", $"getPageUsingAjax({i})");
                tag.GenerateId($"button_page_{i}");
                tag.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}