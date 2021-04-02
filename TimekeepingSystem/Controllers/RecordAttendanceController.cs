using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TimekeepingSystem.Controllers
{
    public class RecordAttendanceController : Controller
    {
        private TimekeepingDbContext context;

        public RecordAttendanceController()
        {
            context = new TimekeepingDbContext();
        }

        // GET: RecordAttendance

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadDataEmployee()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var listEmployees = from e in context.Employees
                                join er in context.EmployeeRoles on e.RoleID equals er.ID
                                join et in context.EmployeeTypes on e.TypeID equals et.ID
                                select new
                                {
                                    ID = e.ID,
                                    Name = e.Name,
                                    Role = er.Name,
                                    Type = et.Name,
                                };
            return Json(new
            {
                data = listEmployees,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRecordAttendance(string record)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // serializer.Deserialize<At>

            // conver from string to object
            List<AttendanceDaily> ads = serializer.Deserialize<List<AttendanceDaily>>(record);


            bool status = false;
            string message = string.Empty;

            DateTime date = DateTime.Today;
            //var monthID;

            var today = DateTime.Today;
          //  var month = new DateTime(today.Year, today.Month, 1);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            if (date.Day == 1)
            {
                
                context.AttendanceMonthlies.Add(new AttendanceMonthly() { FromDate = firstDayOfMonth, ToDate = lastDayOfMonth });
            }

            ///var month = context.AttendanceMonthlies
            //                     .Where(d => d.ToDate == new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)))
            //                   .Select(;

            //  var month = (from d in context.AttendanceMonthlies
            //      where d.ToDate == new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
            //      select new Month{ 
            //       ID = d.ID 
            //     });
            //var month = from d in context.AttendanceMonthlies
            //            where d.ToDate == new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
            //            select new Month {
            //                ID = d.ID
            //            };
            //DateTime d = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
           

            var month = context.AttendanceMonthlies.FirstOrDefault(d => d.ToDate == lastDayOfMonth);

            //var monthID = context.AttendanceMonthlies.Where(m => m.ToDate == new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)))
            //     .Select(m => m.ID);

                       //month.Select("id");
         //   int k = month.First().ID;
  //          Console.WriteLine(month2.FromDate.Day);
            //int i = 1;
            // context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[AttendanceDaily] OFF");
            foreach (var ad in ads)
            {
               // ad.ID = null;
                ad.Date = date;
                ad.MonthID = month.ID;
                context.AttendanceDailies.Add(ad);
            }
            try
            {
                context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }

            return Json(new
            {
                status = status,
                message = message
            });
        }
    }
}