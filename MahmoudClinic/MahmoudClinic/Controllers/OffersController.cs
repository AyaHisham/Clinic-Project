using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MahmoudClinic.Models;

namespace MahmoudClinic.Controllers
{
    public class OffersController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();

        // GET: Offers
        public ActionResult Index()
        {
            return View(db.Offers.ToList());
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Offers offers, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null)
                {
                    if (ImageUpload.ContentType == "image/jpg" || ImageUpload.ContentType == "image/png" || ImageUpload.ContentType == "image/jpeg")
                    {
                        ImageUpload.SaveAs(Server.MapPath("/") + "/Images/OffersImages/" + ImageUpload.FileName);
                        offers.OfferPicURL = ImageUpload.FileName;
                        db.Offers.Add(offers);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(offers);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offers offers = db.Offers.Find(id);
            if (offers == null)
            {
                return HttpNotFound();
            }
            return View(offers);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offers offers, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null)
                {
                    if (ImageUpload.ContentType == "image/jpg" || ImageUpload.ContentType == "image/png" || ImageUpload.ContentType == "image/jpeg")
                    {
                        string fullPath = Request.MapPath("~/Images/OffersImages/" + offers.OfferPicURL);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            ImageUpload.SaveAs(Server.MapPath("/") + "/Images/OffersImages/" + ImageUpload.FileName);
                            offers.OfferPicURL = ImageUpload.FileName;
                        }
                    }
                }
                db.Entry(offers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offers);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offers offers = db.Offers.Find(id);
            if (offers == null)
            {
                return HttpNotFound();
            }
            return View(offers);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offers offers = db.Offers.Find(id);
            string fullPath = Request.MapPath("~/Images/OffersImages/" + offers.OfferPicURL);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Offers.Remove(offers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
