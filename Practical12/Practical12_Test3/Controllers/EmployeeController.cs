using Practical12_Test3.Data;
using Practical12_Test3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical12_Test3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Connection _conn = new Connection();
        public ActionResult Index()
        {
            List<EmployeeModel> employees = _conn.FetchEmployees();
            return View(employees);
        }

        [HttpGet]
        public ActionResult EmployeeWithDesignation()
        {
            List<EmployeeWithDesignationModel> employeeWithDesignationModels = _conn.EmployeeDataWithDesignation();
            return View(employeeWithDesignationModels);
        }

        [HttpGet]
        public ActionResult DesignationHavingMoreThan1Employee()
        {
            List<EmployeeWithDesignationModel> designationListHavingMoreThan1Employee = _conn.DesignationWithMoreThan1Employee();
            return View(designationListHavingMoreThan1Employee);
        }

        [HttpGet]
        public ActionResult NumberOfRecordsByDesignationName()
        {
            List<EmployeeWithDesignationModel> numberOfRecordByDesignationName = _conn.NumberOfRecordByDesignation();
            return View(numberOfRecordByDesignationName);
        }

        [HttpGet]
        public ActionResult EmployeeWithMaxSalary()
        {
            List<EmployeeModel> employeeWithMaxSalary = _conn.EmployeeHavingMaxSalary();
            return View(employeeWithMaxSalary);
        }

        [HttpGet]
        public ActionResult CreateStoreProcedureToAddEmployee()
        {
            bool result = _conn.CreateStoreProcedureToAddEmployee();
            if(result)
            {
                TempData["Success"] = "Procedure added successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult CreateStoreProcedureToGetEmployeesOrderByDOB()
        {
            bool result = _conn.CreateProcedureToGetEmployeesOrderByDOB();
            if (result)
            {
                TempData["Success"] = "Procedure added successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult CreateStoreProcedureToGetEmployeesOrderByFirstname()
        {
            bool result = _conn.CreateProcedureToGetEmployeesOrderByFname();
            if (result)
            {
                TempData["Success"] = "Procedure added successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult CreateViewToGetEmployees()
        {
            bool result = _conn.CreateViewToGetEmployeeList();
            if (result)
            {
                TempData["Success"] = "View added successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View("Error");
        }

        [HttpGet]
        public ActionResult CreateNonClusteredIndex()
        {
            bool result = _conn.CreateNonClusteredIndexOnDepartmentIdForEmployee();
            if (result)
            {
                TempData["Success"] = "Index added successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View("Error");
        }
    }
}