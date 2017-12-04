using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyFileHosting.Controllers
{
    public class FilesBrowserController : Controller
    {
        // GET: FilesBrowser
        public ActionResult Index()
        {
            return View();
        }
    }
}