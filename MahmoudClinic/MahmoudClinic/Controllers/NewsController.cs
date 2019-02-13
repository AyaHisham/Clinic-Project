using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MahmoudClinic.Models;
using MahmoudClinic.ViewModels;

namespace MahmoudClinic.Controllers
{
    public class NewsController : Controller
    {
        private MahmoudClinicDBEntities db = new MahmoudClinicDBEntities();

        #region News

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,VideoURL")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Images

        //GET: Display and Add
        public ActionResult DisplayImages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            NewsPictureViewModel NewsVM = new NewsPictureViewModel();
            NewsVM.ID = news.ID;
            NewsVM.Content = news.Content;
            NewsVM.VideoURL = news.VideoURL;
            NewsVM.NewsPicture = db.NewsPicture.Where(p => p.NewsID == news.ID).ToList();

            return View(NewsVM);
        }
        
        //POST: Add
        [HttpPost]
        public ActionResult DisplayImages(NewsPictureViewModel NewsVM, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null)
            {
                if (ImageUpload.ContentType == "image/jpg" || ImageUpload.ContentType == "image/png" || ImageUpload.ContentType == "image/jpeg")
                {
                    ImageUpload.SaveAs(Server.MapPath("/") + "/Images/NewsImages/" + ImageUpload.FileName);

                    NewsPicture NewsPic = new NewsPicture();
                    NewsPic.NewsID = NewsVM.ID;
                    NewsPic.PicURL = ImageUpload.FileName;
                    db.NewsPicture.Add(NewsPic);
                    db.SaveChanges();
                    return RedirectToAction("DisplayImages",new { id = NewsVM.ID});
                }
            }

            return RedirectToAction("DisplayImages", new { id = NewsVM.ID });
        }

        //POST:Delete
        [HttpPost]
        public ActionResult DeleteImage(int ImageID)
        {
            if (ImageID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NewsPicture NewsPic = db.NewsPicture.Where(p => p.ID == ImageID).FirstOrDefault();
            if (NewsPic == null)
            {
                return HttpNotFound();
            }

            db.NewsPicture.Remove(NewsPic);
            db.SaveChanges();

            string fullPath = Request.MapPath("~/Images/NewsImages/" + NewsPic.PicURL);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            return RedirectToAction("DisplayImages", new { id = NewsPic.NewsID});
        }
        #endregion

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
