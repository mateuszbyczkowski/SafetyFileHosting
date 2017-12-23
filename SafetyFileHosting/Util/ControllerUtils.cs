using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace SafetyFileHosting.Util
{
    public static class ControllerUtils
    {
        public static string GetUserName(this Controller controller)
        {
            return controller.HttpContext.User.Identity.Name;
        }

        public static bool IsAuthenticated(this Controller controller)
        {
            return controller.HttpContext.User.Identity.IsAuthenticated;
        }
        
    }
}