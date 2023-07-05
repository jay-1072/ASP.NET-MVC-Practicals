using Practical13_Test2.DAL;
using Practical13_Test2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Practical13_Test2.Controllers
{
    public class EmployeeController : Controller
    {
        private DBContext _db = new DBContext();

        public ActionResult Index()
        {
            var employees = _db.Employees.Include(e => e.Designation);
            return View(employees.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var employee = _db.Employees.Include(e => e.Designation).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return View("Error");
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(_db.Designation, "Id", "Designation", null);
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName, MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId")] EmployeeModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _db.Employees.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.DesignationId = new SelectList(_db.Designation, "Id", "Designation", model.DesignationId);
            return View("Create", model);
        }

        public ActionResult Edit(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(_db.Designation, "Id", "Designation", employee.DesignationId);
            return View("Create", employee);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, FirstName ,MiddleName, LastName, DOB, MobileNumber, Address, Salary, DesignationId")] EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            return View(employee);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again";
            }
            var employee = _db.Employees.Include(e => e.Designation).FirstOrDefault( e => e.Id==id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _db.Employees.Include(e => e.Designation).FirstOrDefault(e => e.Id == id);
                _db.Employees.Remove(employee);
                _db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Query1()
        {
            var employees = _db.Employees.Join(
                _db.Designation,
                emp => emp.DesignationId,
                desig => desig.Id,
                (emp, desig) => new EmployeeDetail()
                {
                    EmployeeId = emp.Id,
                    FirstName = emp.FirstName,
                    MiddleName = string.IsNullOrEmpty(emp.MiddleName) ? "NA" : emp.MiddleName,
                    LastName = emp.LastName,
                    DesignationName = desig.Designation,
                    DOB = emp.DOB,
                    MobileNo = emp.MobileNumber,
                    Address = emp.Address,
                    Salary = emp.Salary
                });

            return View(employees);
        }

        public ActionResult Query2()
        {
            var employeesPerDesignation = from emp in _db.Employees
                            join desig in _db.Designation on emp.DesignationId equals desig.Id
                            select new
                            {
                                emp, desig
                            } into t1
                            group t1 by t1.desig.Designation into g
                            select new EmployeePerDesignation
                            {
                                Designation = g.Key,
                                EmployeeCount = g.Count()
                            };

            return View(employeesPerDesignation);
        }
    }
}
