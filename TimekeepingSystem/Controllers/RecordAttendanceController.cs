using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


    }
}