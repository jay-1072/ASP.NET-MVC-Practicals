using Practical11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practical11.Controllers
{
    public static class PersonRepository
    {
        private static List<Person> _people;
        static PersonRepository()
        {
            _people = new List<Person>
            {
                new Person{Id = 1, FirstName = "Preety", LastName = "Patel", Email = "preety@gmail.com", DateOfBirth = DateTime.Parse("1/1/2001"), Gender = GenderList.Female, Address = "abc"}
            };
        }

        public static void Add(Person person)
        {
            person.Id = _people.Count() + 1;
            _people.Add(person);
        }

        public static bool Edit(int id, Person person)
        {
            var personToEdit = _people.FirstOrDefault(p => p.Id == id);
            bool isPersonEdited = false;

            if (personToEdit != null)
            {
                personToEdit.FirstName = person.FirstName;
                personToEdit.LastName = person.LastName;
                personToEdit.Email = person.Email;
                personToEdit.DateOfBirth = person.DateOfBirth;
                personToEdit.Gender = person.Gender;
                personToEdit.Address = person.Address;

                isPersonEdited = true;
            }
            else
            {
                isPersonEdited = false;
            }

            return isPersonEdited;
        }

        public static bool Delete(int id, Person person)
        {
            var personToDelete = _people.FirstOrDefault(p => p.Id == id);
            bool isPersonDeleted = false;

            if(personToDelete != null)
            {
                isPersonDeleted = _people.Remove(personToDelete);
            }

            return isPersonDeleted;
        }

        public static List<Person> GetPersonData()
        {
            return _people;
        }

        public static Person GetPerson(int id)
        {
            Person person = _people.FirstOrDefault(p => p.Id == id);
            return person;
        }

    }
}