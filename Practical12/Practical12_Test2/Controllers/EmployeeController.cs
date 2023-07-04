using Practical12_Test2.Data;
using Practical12_Test2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12_Test2.Controllers
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

            if (rowAffected != 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EmployeeWithMiddleNameNull()
        {
            int rowAffected = _conn.CountEmployeeHavingMiddleNameNull();

            ViewBag.result = rowAffected;

            return View("EmployeeWithMiddleNameNull");
        }

        [HttpGet]
        public ActionResult TotalSalary()
        {
            decimal sumOfSalaries = _conn.TotalSalaryOfEmployee();

            ViewBag.result = sumOfSalaries;

            return View("TotalSalary");
        }

        [HttpGet]
        public ActionResult EmployeeDobLessThan1_1_2000()
        {
            List<Employee> result = _conn.EmployeeDateOfBirthLessThan1_1_2000();

            return View("EmployeeDobLessThan1_1_2000", result);
        }
    }
}