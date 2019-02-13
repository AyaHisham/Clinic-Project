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
    public class PortifolioController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();        
        // GET: Portifolio
        public ActionResult Index()
        {
            return View(db.About.ToList());
        }

        // GET: Portifolio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Portifolio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portifolio/Create
        [HttpPost]
        public ActionResult Create(About about, HttpPostedFileBase ImageUpload)
        {
                if (ModelState.IsValid)
                {
                    if (ImageUpload != null)
                    {
                        if (ImageUpload.ContentType == "image/jpg" || ImageUpload.ContentType == "image/png" || ImageUpload.ContentType == "image/jpeg")
                        {
                            about.ProfilePicURL = ImageUpload.FileName;
                            db.About.Add(about);
                            db.SaveChanges();
                            ImageUpload.SaveAs(Server.MapPath("/") + "/Images/AboutImages/" + about.ID.ToString() + ImageUpload.FileName);
                            return RedirectToAction("Index");
                       
                        }
                    }
                }

                return View(about);
        }

        // GET: Portifolio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Portifolio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, About Nabout , HttpPostedFileBase ImageUpload)
        {
            About about = db.About.Find(id);
            if (ModelState.IsValid)
            {
                // Edit in case Image Change
                if (ImageUpload != null)
                {
                    // Edit New Data
                    if (ImageUpload.ContentType == "image/jpg" || ImageUpload.ContentType == "image/png" || ImageUpload.ContentType == "image/jpeg")
                    {
                        //First Remove Old Image From Server
                        string FullPath = Request.MapPath(("/") + "/Images/AboutImages/" + about.ID.ToString() + about.ProfilePicURL);
                        if (System.IO.File.Exists(FullPath))
                        {
                            System.IO.File.Delete(FullPath);
                        }
                        about.ProfilePicURL = ImageUpload.FileName;
                        about.Mission = Nabout.Mission;
                        about.Vision = Nabout.Vision;
                        about.Potrifolio = Nabout.Potrifolio;
                        db.SaveChanges();
                        //Add New Image to The Server
                        ImageUpload.SaveAs(Server.MapPath("/") + "/Images/AboutImages/" + Nabout.ID.ToString() + ImageUpload.FileName);
                        return RedirectToAction("Index");

                    }

                }
                //Edit in case image not change
                else
                {
                    about.Mission = Nabout.Mission;
                    about.Vision = Nabout.Vision;
                    about.Potrifolio = Nabout.Potrifolio;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(about);
        }

        // GET: Portifolio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Portifolio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            About about = db.About.Find(id);
            db.About.Remove(about);
            db.SaveChanges();
            string FullPath = Request.MapPath(("/") + "/Images/AboutImages/" + about.ID.ToString() + about.ProfilePicURL);
            if (System.IO.File.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath);
            }
            return RedirectToAction("Index");
        }
    }
}
