using Microsoft.AspNetCore.Mvc;
using MidAssignment2.EF;
using MidAssignment2.EF.Tables;
using MidAssignment2.Models;

namespace MidAssignment2.Controllers
{
    public class DonorController : Controller
    {
        BloodBankDbContext db;

        public DonorController(BloodBankDbContext db)
        {
            this.db = db;
        }
        public IActionResult DonorList(string BloodGroup)
        {
            if (string.IsNullOrEmpty(BloodGroup) || BloodGroup == "All")
            {
                var data = (from d in db.Donors
                            join d2 in db.Donations
                            on d.DonorId equals d2.DonorId into donations
                            orderby d.LastDonationDate descending
                            select new
                            {
                                d.DonorId,
                                d.FullName,
                                d.BloodGroup,
                                d.ContactNo,
                                d.City,
                                d.LastDonationDate,
                                DonationCount = donations.Count()

                            }
                            ).ToList();
                return View(data);
            }
            else
            {
                var data = (from d in db.Donors
                            join d2 in db.Donations
                            on d.DonorId equals d2.DonorId into donations
                            where d.BloodGroup == BloodGroup
                            orderby d.LastDonationDate descending
                            select new
                            {
                                d.DonorId,
                                d.FullName,
                                d.BloodGroup,
                                d.ContactNo,
                                d.City,
                                d.LastDonationDate,
                                DonationCount = donations.Count()
                            }
                            ).ToList();
                return View(data);
            }

            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DonorsModel());
        }

        [HttpPost]
        public IActionResult Create(DonorsModel d)
        {
            if(!ModelState.IsValid)
            {
                return View(d);
            } 

            Donor dt = new Donor();
            {
                dt.FullName = d.FullName;
                dt.BloodGroup = d.BloodGroup;
                dt.ContactNo = d.ContactNumber;
                dt.City = d.City;
                dt.LastDonationDate = DateOnly.FromDateTime(d.LastDonatedDate);
            };

            db.Donors.Add(dt);
            db.SaveChanges();
            return RedirectToAction("DonorList");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var donor = db.Donors.Find(id);
            return View(donor);
        }

        [HttpPost]
        public IActionResult Edit(DonorsModel d)
        {
            if(!ModelState.IsValid)
            {
                return View(d);
            }
            var donor = db.Donors.Find(d.DonorId);
            donor.FullName = d.FullName;
            donor.BloodGroup = d.BloodGroup;
            donor.ContactNo = d.ContactNumber;
            donor.City = d.City;
            donor.LastDonationDate = DateOnly.FromDateTime(d.LastDonatedDate);
            db.SaveChanges();
            return RedirectToAction("DonorList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var donor = db.Donors.Find(id);
            return View(donor);
        }

        [HttpPost]
        public IActionResult Delete(string flag, int id)
        {
            if (flag == "yes")
            {
                var donor = db.Donors.Find(id);
                db.Donors.Remove(donor);
                db.SaveChanges();
            }
            return RedirectToAction("DonorList");
        }






    }
}
