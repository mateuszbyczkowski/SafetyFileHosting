using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SafetyFileHosting.Models;

namespace SafetyFileHosting.Util
{
    public static class ControllerUtils
    {
        public static string GetLoggedUserId(this Controller controller)
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public static string GetLoggedUserName(this Controller controller)
        {
            return controller.HttpContext.User.Identity.Name;
        }

        public static bool IsAuthenticated(this Controller controller)
        {
            return controller.HttpContext.User.Identity.IsAuthenticated;
        }

        public static ApplicationUser GetCurrentApplicationUser(this Controller controller)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(HttpContext.Current.User.Identity.GetUserId());
        }
    }
}