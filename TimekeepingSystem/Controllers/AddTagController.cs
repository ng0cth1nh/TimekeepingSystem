using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimekeepingSystem.Controllers
{
    public class AddTagController : Controller
    {
        private TimekeepingDbContext context;

        public AddTagController()
        {
            context = new TimekeepingDbContext();
        }
        // GET: AddTag
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadTag(int id)
        {
            context.Configuration.ProxyCreationEnabled = false;

            var tagInfors = (from t in context.Tags.Where(t => t.ID == id)
                             join p in context.Products
                             on t.ProductID equals p.ID
                             join c in context.Colors
                             on p.ColorID equals c.ID
                             join s in context.Sizes
                             on p.SizeID equals s.ID
                             join pd in context.ProductDetails
                             on p.ID equals pd.ProductID
                             join st in context.Steps
                             on pd.StepID equals st.ID
                             select new TagInfor
                             {
                                 ID = t.ID,
                                 Size = s.Name,
                                 Color = c.Name,
                                 ProductID = t.ProductID,
                                 Quantity = t.Quantity,
                                 StepID = st.ID,
                                 Step = st.Name
                             }
                            ).ToList();

            return Json(new
            {
                data = tagInfors,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult LoadEmployee(int id)
        {
            context.Configuration.ProxyCreationEnabled = false;

            var employee = context.Employees.Find(id);

            if (employee == null)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new
                {
                    data = employee,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}