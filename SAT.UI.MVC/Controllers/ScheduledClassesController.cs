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
    public class ScheduledClassesController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: ScheduledClasses
       [Authorize(Roles = "Admin,Instructor")]
      
        public ActionResult Index()
        {
            var scheduledClasses1 = db.ScheduledClasses1.Include(s => s.Cours).Include(s => s.ScheduledClassStatus);
            return View(scheduledClasses1.ToList());
        }

        // GET: ScheduledClasses/Details/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClasses scheduledClasses = db.ScheduledClasses1.Find(id);
            if (scheduledClasses == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClasses);
        }

        // GET: ScheduledClasses/Create
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.SCID = new SelectList(db.ScheduledClassStatuses, "SCID", "SCSName");
            return View();
        }

        // POST: ScheduledClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduledClassId,CourseId,StartDate,EndDate,InstructorName,Location,SCID")] ScheduledClasses scheduledClasses)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClasses1.Add(scheduledClasses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", scheduledClasses.CourseId);
            ViewBag.SCID = new SelectList(db.ScheduledClassStatuses, "SCID", "SCSName", scheduledClasses.SCID);
            return View(scheduledClasses);
        }

        // GET: ScheduledClasses/Edit/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClasses scheduledClasses = db.ScheduledClasses1.Find(id);
            if (scheduledClasses == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", scheduledClasses.CourseId);
            ViewBag.SCID = new SelectList(db.ScheduledClassStatuses, "SCID", "SCSName", scheduledClasses.SCID);
            return View(scheduledClasses);
        }

        // POST: ScheduledClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduledClassId,CourseId,StartDate,EndDate,InstructorName,Location,SCID")] ScheduledClasses scheduledClasses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledClasses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", scheduledClasses.CourseId);
            ViewBag.SCID = new SelectList(db.ScheduledClassStatuses, "SCID", "SCSName", scheduledClasses.SCID);
            return View(scheduledClasses);
        }

        // GET: ScheduledClasses/Delete/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClasses scheduledClasses = db.ScheduledClasses1.Find(id);
            if (scheduledClasses == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClasses);
        }

        // POST: ScheduledClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduledClasses scheduledClasses = db.ScheduledClasses1.Find(id);
            db.ScheduledClasses1.Remove(scheduledClasses);
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
