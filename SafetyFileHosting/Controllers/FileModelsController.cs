using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafetyFileHosting.Models;
using SafetyFileHosting.db;

namespace SafetyFileHosting.Controllers
{
    public class FileModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileModels
        public ActionResult Index()
        {
            return View(db.FileModels.ToList());
        }

        // GET: FileModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileModel fileModel = db.FileModels.Find(id);
            if (fileModel == null)
            {
                return HttpNotFound();
            }
            return View(fileModel);
        }

        // GET: FileModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Author,CreationDate")] FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                db.FileModels.Add(fileModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileModel);
        }

        // GET: FileModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileModel fileModel = db.FileModels.Find(id);
            if (fileModel == null)
            {
                return HttpNotFound();
            }
            return View(fileModel);
        }

        // POST: FileModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Author,CreationDate")] FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileModel);
        }

        // GET: FileModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileModel fileModel = db.FileModels.Find(id);
            if (fileModel == null)
            {
                return HttpNotFound();
            }
            return View(fileModel);
        }

        // POST: FileModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileModel fileModel = db.FileModels.Find(id);
            db.FileModels.Remove(fileModel);
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
