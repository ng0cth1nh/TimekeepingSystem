using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimekeepingSystem.Common;

namespace TimekeepingSystem.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            return Redirect("login");

        }
    }
}