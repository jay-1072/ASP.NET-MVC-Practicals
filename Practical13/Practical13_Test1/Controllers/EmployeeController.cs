using Practical13_Test1.DAL;
using Practical13_Test1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Practical13_Test1.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeContext _db = new EmployeeContext();
                
        public ActionResult Index()
        {
            var employees = from e in _db.Employees select e;
            return View(employees);
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
                    _db.Employees.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View("Create", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Employee model)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = _db.Employees.Find(id);
            if (TryUpdateModel(employeeToUpdate, "", new string[] { "Name", "DOB", "Age" }))
            {
                try
                {
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View("Create", employeeToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }
            var employee = _db.Employees.Find(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Employee employee = _db.Employees.Find(id);
                _db.Employees.Remove(employee);
                _db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
