using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MahmoudClinic.Models;
using System.Net;
using System.Data.Entity;

namespace MahmoudClinic.Controllers
{
    public class ReviewController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities(); 
        // GET: Review
        public ActionResult Index()
        {
            return View(db.Review.ToList());
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(Review review)
        {
            if(ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Review review = db.Review.Find(id);
            if(review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Review review = db.Review.Find(id);
            db.Review.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
