using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zerohunger.Models;

namespace Zerohunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            int id = (int)Session["Id"];
            var db = new ZerohungerEntities();

            var req = (from r in db.Requests
                       where r.EmployeeId.Equals(id)
                       select r);

            var reqList = new List<Request>(req);


            return View(reqList);
        }
    }
}