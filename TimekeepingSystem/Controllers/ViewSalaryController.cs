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

            var list1 =
                          from e in context.Employees
                          join et in context.EmployeeTypes
                          on e.TypeID equals et.ID
                          join er in context.EmployeeRoles
                          on e.RoleID equals er.ID
                          join ad in context.AttendanceDailies
                          on e.ID equals ad.EmployeeID
                          where ad.MonthID == thatMonth.ID && ad.isPresent == true && e.TypeID == 1
                          group new { e, et, er, ad }
                          by new { e.ID, e.Name, Type = et.Name, Role = er.Name } into g
                          select new
                          {
                              ID = g.Key.ID,
                              Name = g.Key.Name,
                              Role = g.Key.Role,
                              Type = g.Key.Type,
                              Salary = g.Count() * 200000
                          };


            var list2 =
                        from ctd in context.CompleteTagDetails
                        join ct in context.CompleteTags
                        on ctd.TagID equals ct.TagID
                        join s in context.Steps
                        on ctd.StepID equals s.ID
                        join e in context.Employees
                        on ctd.EmployeeID equals e.ID
                        join et in context.EmployeeTypes
                        on e.TypeID equals et.ID
                        join er in context.EmployeeRoles
                        on e.RoleID equals er.ID
                        where ct.Date <= thatMonth.ToDate && ct.Date >= thatMonth.FromDate
                        group new { ctd, ct, s, e, et, er }
                        by new
                        {
                            e.ID,
                            e.Name,
                            Type = et.Name,
                            Role = er.Name
                        } into g
                        select new
                        {
                            ID = g.Key.ID,
                            Name = g.Key.Name,
                            Role = g.Key.Role,
                            Type = g.Key.Type,
                            Salary = g.Sum(k => (k.s.Price * k.ct.CompleteQuantity))
                        };


            var list = list1.Concat(list2).ToList();

            return Json(new
            {
                data = list,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}