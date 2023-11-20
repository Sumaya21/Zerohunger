using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zerohunger.Models;
using Zerohunger.Models.db;

namespace Zerohunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(DonorDto Donor) 
        {
            var db = new ZerohungerEntities();

           if(ModelState.IsValid)
            {
                Donor c = new Donor()
                {
                    Name = Donor.Name,
                    Email = Donor.Email,
                    Password = Donor.Password,

                };

                db.Donors.Add(c);
                db.SaveChanges();
                TempData["msg"] = "User Added!";
                return RedirectToAction("Login", "Home");    
            }

           else
            {
                TempData["msg"] = "Signup failed";
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult Login() 
        {
         return View();
        
        }
        [HttpPost]
        public ActionResult Login(LoginDto obj) 
        { 
        var db= new ZerohungerEntities();
            var doner = (from u in db.Donors
                         where u.Email.Equals(obj.Email) &&
                         u.Password.Equals(obj.Password)
                         select u).FirstOrDefault();

            var admin = (from u in db.Admins
                         where u.Email.Equals(obj.Email) &&
                         u.Password.Equals(obj.Password)
                         select u).FirstOrDefault();

            var employee = (from u in db.Employees
                            where u.Email.Equals(obj.Email) &&
                            u.Password.Equals(obj.Password)
                            select u).FirstOrDefault();


            if (doner != null)
            {
                Session["Id"] = doner.Id;
                Session["Name"] = doner.Name;
                Session["Email"] = doner.Email;

                return RedirectToAction("Index", "Donor");
            }


            else if (admin != null)
            {
                Session["Id"] = admin.Id;
                Session["Email"] = admin.Email;
                return RedirectToAction("Index", "Admin");
            }

            else if (employee != null)
            {
                Session["Id"] = employee.Id;
                Session["Email"] = employee.Email;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                TempData["msg"] = "Login Failed!";
                return View(obj);
            }
        }
        }

    }
