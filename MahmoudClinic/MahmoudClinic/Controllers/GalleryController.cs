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
    public class GalleryController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();

        // GET: Gallery
        public ActionResult Index()
        {
            return View(db.Gallery.ToList());
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery gallery, HttpPostedFileBase ImageUploadBefore, HttpPostedFileBase ImageUploadAfter)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadBefore != null && ImageUploadAfter != null)
                {
                    if ((ImageUploadBefore.ContentType == "image/jpg" || ImageUploadBefore.ContentType == "image/png" || ImageUploadBefore.ContentType == "image/jpeg")&&(ImageUploadAfter.ContentType == "image/jpg" || ImageUploadAfter.ContentType == "image/png" || ImageUploadAfter.ContentType == "image/jpeg"))
                    {
                        ImageUploadBefore.SaveAs(Server.MapPath("/") + "/Images/GalleryImages/" + ImageUploadBefore.FileName);
                        ImageUploadAfter.SaveAs(Server.MapPath("/") + "/Images/GalleryImages/" + ImageUploadAfter.FileName);
                        gallery.BeforePicURL = ImageUploadBefore.FileName;
                        gallery.AfterPicURL = ImageUploadAfter.FileName;
                        db.Gallery.Add(gallery);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(gallery);
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase ImageUploadBefore, HttpPostedFileBase ImageUploadAfter)
        {
            if (ModelState.IsValid)
            {
                //Change Before Image
                if (ImageUploadBefore != null)
                {
                    if (ImageUploadBefore.ContentType == "image/jpg" || ImageUploadBefore.ContentType == "image/png" || ImageUploadBefore.ContentType == "image/jpeg")
                    {
                        string BeforFullPath = Request.MapPath("~/Images/GalleryImages/" + gallery.BeforePicURL);
                        if (System.IO.File.Exists(BeforFullPath))
                        {
                            System.IO.File.Delete(BeforFullPath);
                            ImageUploadBefore.SaveAs(Server.MapPath("/") + "/Images/GalleryImages/" + ImageUploadBefore.FileName);
                            gallery.BeforePicURL = ImageUploadBefore.FileName;
                        }
                    }
                }

                //Change After Image
                if (ImageUploadAfter != null)
                {
                    if (ImageUploadAfter.ContentType == "image/jpg" || ImageUploadAfter.ContentType == "image/png" || ImageUploadAfter.ContentType == "image/jpeg")
                    {
                        string AfterFullPath = Request.MapPath("~/Images/GalleryImages/" + gallery.AfterPicURL);
                        if (System.IO.File.Exists(AfterFullPath))
                        {
                            System.IO.File.Delete(AfterFullPath);
                            ImageUploadAfter.SaveAs(Server.MapPath("/") + "/Images/GalleryImages/" + ImageUploadAfter.FileName);
                            gallery.AfterPicURL = ImageUploadAfter.FileName;
                        }

                    }
                }

                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Gallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Gallery.Find(id);
            string BeforFullPath = Request.MapPath("~/Images/GalleryImages/" + gallery.BeforePicURL);
            string AfterFullPath = Request.MapPath("~/Images/GalleryImages/" + gallery.AfterPicURL);
            if (System.IO.File.Exists(BeforFullPath))
            {
                System.IO.File.Delete(BeforFullPath);
            }
            if (System.IO.File.Exists(AfterFullPath))
            {
                System.IO.File.Delete(AfterFullPath);
            }
            db.Gallery.Remove(gallery);
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
