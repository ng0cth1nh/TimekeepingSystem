using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimekeepingSystem.Controllers
{
    public class ViewAttendanceController : Controller
    {
        private TimekeepingDbContext context;

        public ViewAttendanceController()
        {
            context = new TimekeepingDbContext();
        }

        // GET: ViewAttendance
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string month, string year)
        {
            context.Configuration.ProxyCreationEnabled = false;

            int monthInt = Convert.ToInt32(month);
            int yearInt = Convert.ToInt32(year);

            var firstDayOfMonth = new DateTime(yearInt, monthInt, 1);
            var thatMonth = context.AttendanceMonthlies.FirstOrDefault(d => d.FromDate == firstDayOfMonth);


            var days = (from am in context.AttendanceMonthlies.Where(t => t.ID == thatMonth.ID)
                        join ad in context.AttendanceDailies
                        on am.ID equals ad.MonthID
                        join e in context.Employees
                        on ad.EmployeeID equals e.ID
                        select new
                        {
                            name = e.Name,
                            id = e.ID,
                            day = ad.Date.Day,
                            isPresent = ad.isPresent
                        }
                        ).ToList();
            return Json(new
            {
                data = days,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}