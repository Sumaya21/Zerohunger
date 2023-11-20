using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Zerohunger.Models;
using Zerohunger.Models.db;

namespace Zerohunger.Controllers
{
    public class DonorController : Controller
    {

        // GET: Donor

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Donation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Donation(DonationDto obj)
        {
            var db = new ZerohungerEntities();

            var donation = new Donation()
            {
                Name = obj.Name,
                Qunatity = obj.Quantity,
                ExpiryDate = obj.ExpiryDate,
                Status = "Pending",
                DonorId = (int)Session["Id"]
            };

            db.Donations.Add(donation);
            db.SaveChanges();

            TempData["msg"] = "Donation Added!";
            return View();

        }
        public ActionResult History()
        {
            int id = (int)Session["Id"];
            var db = new ZerohungerEntities();


            var history = (from h in db.Donations
                           where h.DonorId.Equals(id)
                           select h); 

           

            return View(history);


        }

        public ActionResult Logout()
        {
            Session["Id"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "Home");
        }
        DonationDto Convert(Donation donation)
        {
            DonationDto d = new DonationDto()
            {
                Id = donation.Id,
                Name = donation.Name,
                Quantity = donation.Qunatity,
                ExpiryDate = donation.ExpiryDate,
                Status = donation.Status,
                DonorId = donation.DonorId

            };

            return d;


        }
        DonorDto Convert(Donor donor)
        {
            var d = new DonorDto()
            {
                Name = donor.Name,
                Email = donor.Email,
                Address = donor.Address,
                Password = donor.Password
            };

            return d;
        }


        Donor Convert(DonorDto donor)
        {
            var dono = new Donor()
            {
                Name = donor.Name,
                Email = donor.Email,
                Address = donor.Address,
                Password = donor.Password
            };

            return dono;
        }
           

        }
    }
