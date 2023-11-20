using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zerohunger.Models;
using Zerohunger.Models.db;

namespace Zerohunger.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        
        public ActionResult AddEmployee()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddEmployee(EmployeeDto emp)
        {
            if (ModelState.IsValid)
            {
                var db = new ZerohungerEntities();
                db.Employees.Add(Convert(emp));
                db.SaveChanges();

                TempData["msg"] = "Employee Added!";
                return View();

            }

            else
            {
                TempData["msg"] = "Error Occured!";
                return View(emp);
            }
        }

        
        public ActionResult ShowEmployee()
        {
            var db = new ZerohungerEntities();
            var emp = db.Employees.ToList();
            return View(emp);
        }

        [HttpGet]
        
        public ActionResult Assign()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Assign(AssignDto obj)
        {
            var db = new ZerohungerEntities();

            var req = new Request()
            {
                Status = "Employee Assigned!",
                DonorId = obj.DonorId,
                EmployeeId = obj.Id,
            };

            db.Requests.Add(req);
            db.SaveChanges();

            return View(obj);
        }

     
        public ActionResult ViewAll()
        {
            var db = new ZerohungerEntities();

            var all = db.Requests.ToList();

            return View(all);
        }

        
        public ActionResult Pending()
        {
            var db = new ZerohungerEntities();

            var pending = (from p in db.Donations
                           where p.Status.Equals("Pending")
                           select p);

            var pendinglist = new List<Donation>(pending);

            return View(pendinglist);
        }



        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "Home");
        }



        Employee Convert(EmployeeDto emp)
        {
            var e = new Employee()
            {
                Name = emp.Name,
                Email = emp.Email,
                Password = emp.Password
            };

            return e;
        }


    }
}