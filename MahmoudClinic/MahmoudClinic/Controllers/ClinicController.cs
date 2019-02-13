using MahmoudClinic.Models;
using MahmoudClinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahmoudClinic.Controllers
{
    public class ClinicController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();

        // GET: Clinic
        public ActionResult Index()
        {
            return View();
        }

        //Mission&Vision Partial View
        public ActionResult MissionVision()
        {
            About MissionVision = db.About.Where(a=>a.ID == db.About.Max(aa=>aa.ID)).FirstOrDefault();
            MissionVisionViewModel MissionVisionVM = new MissionVisionViewModel();
            MissionVisionVM.ID = MissionVision.ID;
            MissionVisionVM.Mission = MissionVision.Mission;
            MissionVisionVM.Vision = MissionVision.Vision;
            return View(MissionVisionVM);
        }

        //Appointment Partial View
        // GET: Apointment
        public ActionResult Apointment()
        {
            List<VisitCause> vistCause = db.VisitCause.ToList();
            ViewBag.Causes = new SelectList(vistCause, "ID", "Cause");
            return View();
        }

        // POST: Apointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apointment(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<VisitCause> vistCause = db.VisitCause.ToList();
            ViewBag.Causes = new SelectList(vistCause, "ID", "Cause");
            return View(patient);
        }

        //Portfolio Partial View
        public ActionResult Portfolio()
        {
            About Portfolio = db.About.Where(a => a.ID == db.About.Max(aa => aa.ID)).FirstOrDefault();
            PorfolioViewModel PortfolioVM = new PorfolioViewModel();
            PortfolioVM.ID = Portfolio.ID;
            PortfolioVM.Potrifolio = Portfolio.Potrifolio;
            PortfolioVM.ProfilePicURL = Portfolio.ProfilePicURL;
            return View(PortfolioVM);
        }

        //Offers Partial View
        public ActionResult Offers()
        {
            List<Offers> offers = db.Offers.OrderByDescending(o=>o.ID).Take(10).ToList();
            return View(offers);
        }

        //Gallery Partial View
        public ActionResult Gallery()
        {
            List<Gallery> Gallery = db.Gallery.OrderByDescending(g=>g.ID).Take(8).ToList();
            return View(Gallery);
        }

        //Reviews Partial View
        public ActionResult Reviews()
        {
            List<Review> Reviews = db.Review.OrderByDescending(r=>r.ID).Where(r => r.Approved == true).Take(6).ToList();
            return View(Reviews);
        }

        //News Partial View
        public ActionResult News()
        {
            List<News> News = db.News.Take(20).ToList();
            return View(News);
        }

        //ContactUs Partial View
        public ActionResult Contact()
        {
            ContactUs Contact = db.ContactUs.Where(c => c.ID == (db.ContactUs.Max()).ID).FirstOrDefault();
            return View(Contact);
        }
    }
}