using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SafetyFileHosting.Models;
using SafetyFileHosting.db;
using SafetyFileHosting.Util;

namespace SafetyFileHosting.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileModels
        public ActionResult Index()
        {
            var loggedUserId = this.GetLoggedUserId();
            var userFiles = db.FileModels.Where(x => x.UserId.Equals(loggedUserId)).ToList();
            return View(userFiles);
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
        public ActionResult Create(FileModel fileModel, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid || file == null || file.ContentLength == 0)
                {
                    return View(fileModel);
                }

                var relativePath = Path.Combine(ApplicationConstants.USER_FILE_DIRECTORY, this.GetLoggedUserName());
                var serverPath = Server.MapPath(relativePath);
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                fileModel.Author = this.GetLoggedUserName();
                fileModel.CreationDate = DateTime.Now;
                fileModel.FileName = file.FileName;
                fileModel.PathToFile = Path.Combine(relativePath, file.FileName);
                fileModel.UserId = this.GetLoggedUserId();
                file.SaveAs(Path.Combine(serverPath, file.FileName));
                db.FileModels.Add(fileModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (IOException e)
            {
                //todo: display error on user page
                ModelState.AddModelError("file.creation.error", e.Message);
                return View(fileModel);
            }
        }

        public void DownloadFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("File with this id doesn't exists");
            }

            var fileModel = db.FileModels.Find(id);
            if (fileModel == null)
            {
                throw new ArgumentException("File doesn't exists");
            }
            var response = this.HttpContext.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition",
                "attachment; filename=" + fileModel.FileName + ";");
            response.TransmitFile(Server.MapPath(fileModel.PathToFile));
            response.Flush();
            response.End();
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