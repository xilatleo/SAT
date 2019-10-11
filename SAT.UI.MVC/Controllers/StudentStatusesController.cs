using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;

namespace SAT.UI.MVC.Controllers
{
    public class StudentStatusesController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: StudentStatuses
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Index()
        {
            return View(db.StudentStatuses.ToList());
        }

        // GET: StudentStatuses/Details/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatuses studentStatuses = db.StudentStatuses.Find(id);
            if (studentStatuses == null)
            {
                return HttpNotFound();
            }
            return View(studentStatuses);
        }

        // GET: StudentStatuses/Create
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSID,SSName,SSDescription")] StudentStatuses studentStatuses)
        {
            if (ModelState.IsValid)
            {
                db.StudentStatuses.Add(studentStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentStatuses);
        }

        // GET: StudentStatuses/Edit/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatuses studentStatuses = db.StudentStatuses.Find(id);
            if (studentStatuses == null)
            {
                return HttpNotFound();
            }
            return View(studentStatuses);
        }

        // POST: StudentStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSID,SSName,SSDescription")] StudentStatuses studentStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentStatuses);
        }

        // GET: StudentStatuses/Delete/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentStatuses studentStatuses = db.StudentStatuses.Find(id);
            if (studentStatuses == null)
            {
                return HttpNotFound();
            }
            return View(studentStatuses);
        }

        // POST: StudentStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentStatuses studentStatuses = db.StudentStatuses.Find(id);
            db.StudentStatuses.Remove(studentStatuses);
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
