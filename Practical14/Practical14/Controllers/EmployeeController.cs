using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Practical14.Models;
using PagedList;

namespace Practical14.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeEntities db = new EmployeeEntities();
        int pageSize = 10;
        
        public ActionResult Index(string searchString, int? page, string partialViewName=null)
        {
           // if (searchString != null)
           // {
           //     page = 1;
           // }
           // else
           // {
                //searchString = currentFilter;
            //    TempData["CurrentFilter"] = searchString;
            //}

            TempData["CurrentFilter"] = searchString;

            int pageNumber = (page ?? 1);
            var employees = db.Employee.OrderBy(emp => emp.Name).Where(emp => String.IsNullOrEmpty(searchString) || emp.Name.Contains(searchString)).ToPagedList(pageNumber, pageSize);

            if(partialViewName != null)
            {
                return PartialView(partialViewName, employees);
            }

            return View(employees);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employee.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to add changes. please check the provided details"); 
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employee.Find(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Employee model)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employee.Find(id);
            if (TryUpdateModel(employeeToUpdate, "", new string[] { "Name", "DOB", "Age" }))
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. please check the provided details.");
                }
            }

            return View(model);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }
            var employee = db.Employee.Find(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, Employee model)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employee.Find(id);

            if(employee == null)
            {
                return HttpNotFound();
            }

            try 
            {
                db.Employee.Remove(employee);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

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