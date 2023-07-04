using Practical12_Test1.Data;
using Practical12_Test1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12_Test1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Connection _conn = new Connection();
        public ActionResult Index()
        {
            List<Employee> employees = _conn.FetchEmployees();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            int rowAffected = _conn.AddEmployee(employee);

            if(rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AddTestData()
        {
            int rowAffected = _conn.AddDummyEmployees();

            if (rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult ChangeFirstName()
        {
            int rowAffected = _conn.ChangeFirstNameForFirstRecord();

            if (rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult ChangeMiddleName()
        {
            int rowAffected = _conn.ChangeAllMiddleName();

            if (rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult DeleteEmployeeWithIdLessThan2()
        {
            int rowAffected = _conn.DeleteRecordWithIdLessThan2();

            if (rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult DeleteAll()
        {
            int rowAffected = _conn.DeleteAllEmployee();

            if(rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }            
        }
    }
}