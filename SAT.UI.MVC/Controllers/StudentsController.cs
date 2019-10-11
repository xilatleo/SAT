using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using SAT.UI.MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace SAT.UI.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID,Major")] Student student, HttpPostedFileBase fupImage)
        {
            if (ModelState.IsValid)
            {
                #region FileUpload FOR CREATE
                //use default image if none is provided
                string imgName = "noImage.png";
                if (fupImage != null)//if it has a value then they uploaded file.
                {
                    //get image & assign it a variable
                    imgName = fupImage.FileName;

                    //declare and assign ext value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//get the extension including '.'

                    //declare list of valid extensions
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //check the ext variable in lowercase vs valid list

                    if (goodExts.Contains(ext.ToLower()) && (fupImage.ContentLength <= 4194304))
                    {
                        //if it is in the list , rename it using a GUID (Unique is vital to avoid overwrite)
                        imgName = Guid.NewGuid() + ext;

                        //save to the webserver at correct location
                        fupImage.SaveAs(Server.MapPath("~/Content/img/Students/" + imgName));
                    }
                    else
                    {
                        //if you landed here, something went wrong
                        //either filesize iss too big or unacceptable file type
                        //we have options - throw an error (cathch or dont) or  default
                        imgName = "noImage.png";
                    }
                }

                student.PhotoUrl = imgName;
                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID,Major")] Student student, HttpPostedFileBase fupImage)
        {
            if (ModelState.IsValid)
            {
                #region FileUpload FOR EDIT
                if (fupImage != null)
                {
                    //Get image and assign to variable
                    string imgName = fupImage.FileName;



                    //declare and assign ext value
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//get the extension including '.'

                    //declare list of valid extensions
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //check the ext variable in lowercase vs valid list

                    if (goodExts.Contains(ext.ToLower()) && (fupImage.ContentLength <= 4194304))
                    {
                        //if it is in the list , rename it using a GUID (Unique is vital to avoid overwrite)
                        imgName = Guid.NewGuid() + ext;

                        //save to the webserver at correct location
                        fupImage.SaveAs(Server.MapPath("~/Content/img/Students/" + imgName));

                        //Housekeeping for edit: delete old file on record if not the default
                        if (student.PhotoUrl != null && student.PhotoUrl != "noImage.png")
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/img/Students/" + student.PhotoUrl));
                        }



                        //ONLY if file upload ok, change the new db record's file field to reflect
                        student.PhotoUrl = imgName;
                    }
                    else
                    {
                        //if you landed here, something went wrong
                        //either filesize iss too big or unacceptable file type
                        //we have options - throw an error (cathch or dont) or  default
                        //imgName = "noImage.png";

                        throw new ApplicationException("Incorrect file type (use PNG, JPG or GIF), or file should not exceeds 4MB)");
                    }//end if tree for good extesion and good file size


                }//end if fup exists


                #endregion
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin,Instructor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            
            student.SSID = 3;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ReactiveDel(int id)
        {
            Student student = db.Students.Find(id);
            student.SSID = 1;
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
