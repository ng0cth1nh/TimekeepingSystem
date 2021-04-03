using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimekeepingSystem.Controllers
{
    public class ViewSalaryController : Controller
    {

        private TimekeepingDbContext context;

        public ViewSalaryController()
        {
            context = new TimekeepingDbContext();
        }
        // GET: ViewSalary
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


            //using (var ctx = new SchoolDBEntities())
            //{
            //    //Get student name of string type
            //    string studentName = ctx.Database.SqlQuery<string>("Select studentname from Student where studentid=1")
            //                            .FirstOrDefault();

            //    //or
            //    string studentName = ctx.Database.SqlQuery<string>("Select studentname from Student where studentid=@id", new SqlParameter("@id", 1))
            //                            .FirstOrDefault();
            //}

            var list1 = (from e in context.Employees
                         join et in context.EmployeeTypes
                          on e.TypeID equals et.ID
                         join er in context.EmployeeRoles
                         on e.RoleID equals er.ID
                         join ad in context.AttendanceDailies
                         on e.ID equals ad.EmployeeID
                         where ad.MonthID == thatMonth.ID && ad.isPresent == true && e.TypeID == 1
                        // group new { e, et, er } by new { e.ID, e.Name } into g
                         select new
                         {
                             ID = e.ID,
                             Name = e.Name,
                             Type = et.Name,
                             Role = er.Name,
                             Num = 1
                         }).ToList();

            //string Sql = "select e.ID,e.Name,et.Name,er.Name,num = COUNT(e.ID) from Employee e\n" +
            //               "join EmployeeType et on et.ID = e.TypeID\n" +
            //               "join EmployeeRole er on er.ID = e.RoleID\n" +
            //               "join AttendanceDaily ad on e.ID = ad.EmployeeID\n" +
            //               "where ad.MonthID = @mid and ad.isPresent = 1 and e.TypeID = 1\n" +
            //              "group by e.ID,e.Name,et.Name,er.Name";


            //var list = context.Database.SqlQuery<string>(Sql, new SqlParameter("@mid", thatMonth.ID)).ToList();
            return Json(new
            {
                data = list1,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}