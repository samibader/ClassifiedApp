using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ClassifiedApp.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
        public static MvcHtmlString StatusDropDownList(this HtmlHelper htmlHelper, string selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>(){
                new SelectListItem(){Text="Active", Value="1", Selected=selectedValue=="1"},
                new SelectListItem(){Text="Blocked", Value="0", Selected=selectedValue=="0"}
            };
            return htmlHelper.DropDownList(
                "status",
                list, "Any", new { @class = "form-control" }
                );
        }
    }
}