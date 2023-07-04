using Practical11_PartialView.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Practical11_PartialView.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public PartialViewResult Index()
        {
            return PartialView(PersonRepository.GetPersonData());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            try
            {
                if (person.Gender == 0)
                {
                    ViewBag.genderErroMessage = "Please select gender";
                    return View(person);
                }
               
                if (ModelState.IsValid)
                {
                    PersonRepository.Add(person);
                    return RedirectToAction("Index");
                }                
            }
            catch(DataException)
            {
                return View("Error");
            }
            return View("Create", person);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var personToEdit = PersonRepository.GetPerson(id);
            return View(personToEdit);
        }

        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    PersonRepository.Edit(id, person);
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                return View();
            }
            return View("Edit", person);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var person = PersonRepository.GetPerson(id);
            return View(person);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var personToDelete = PersonRepository.GetPerson(id);
            return View(personToDelete);
        }

        [HttpPost]
        public ActionResult Delete(int id, Person person)
        {
            bool isDeleted = PersonRepository.Delete(id, person);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
    }
}
