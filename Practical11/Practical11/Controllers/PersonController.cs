using Practical11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace Practical11.Controllers
{
    public class PersonController : Controller
    {       
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(PersonRepository.GetPersonData());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var person = PersonRepository.GetPerson(id);
            return View(person);
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
            catch
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
            catch
            {
                return View("Error");
            }
            return View("Edit", person);
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

            if(isDeleted)
            {
                return RedirectToAction("Index");
            }

            return View("Error");            
        }
    }
}