using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WindowAuthentication.Data;
using WindowAuthentication.Models;

namespace WindowAuthentication.Controllers
{
	public class StudentController : Controller
	{
		private DatabaseContext db = new DatabaseContext();

		public ActionResult Index()
		{
			return View(db.Student.ToList());
		}

		public ActionResult Details(int id)
		{			
			Student student = db.Student.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Standard,Age,EnrollmentNumber")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Student.Add(student);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(student);
		}

		public ActionResult Edit(int id)
		{			
			Student student = db.Student.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Standard,Age,EnrollmentNumber")] Student student)
		{
			if (ModelState.IsValid)
			{
				db.Entry(student).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(student);
		}

		public ActionResult Delete(int id)
		{
			Student student = db.Student.Find(id);
			if (student == null)
			{
				return HttpNotFound();
			}
			return View(student);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Student student = db.Student.Find(id);
			db.Student.Remove(student);
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