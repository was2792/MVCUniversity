using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCUniversity.DAL;
using MVCUniversity.Models;

namespace MVCUniversity.Controllers
{
    public class CartsController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: Carts
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.Course).Include(c => c.Student);
            return View(carts.ToList());
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? studentID, int? courseID)
        {
            if (studentID == null || courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(new object[] { studentID, courseID });
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,CourseID")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Title", cart.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", cart.StudentID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? studentID, int? courseID)
        {
            if (studentID == null || courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(new object[] { studentID, courseID });
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Title", cart.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", cart.StudentID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,CourseID")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Title", cart.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", cart.StudentID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? studentID, int? courseID)
        {
            if (studentID == null || courseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(new object[] { studentID, courseID });
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int studentID, int courseID)
        {
            Cart cart = db.Carts.Find(new object[] { studentID, courseID });
            db.Carts.Remove(cart);
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
