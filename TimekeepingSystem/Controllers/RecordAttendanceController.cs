using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimekeepingSystem.Controllers
{
    public class RecordAttendanceController : Controller
    {
        // GET: RecordAttendance
     
        public ActionResult Index()
        {
            return View();
        }
    }
}