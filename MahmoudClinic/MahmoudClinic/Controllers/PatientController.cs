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
    public class PatientController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();
        // GET: Patient
        public ActionResult Index()
        {
            return View(db.Patient.ToList());
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            List<VisitCause> vistCause = db.VisitCause.ToList();
            ViewBag.Causes = new SelectList(vistCause, "ID", "Cause");
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
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

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            List<VisitCause> vistCause = db.VisitCause.ToList();
            ViewBag.Causes = new SelectList(vistCause, "ID", "Cause", patient.CauseID);
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<VisitCause> vistCause = db.VisitCause.ToList();
            ViewBag.Causes = new SelectList(vistCause, "ID", "Cause", patient.CauseID);
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
