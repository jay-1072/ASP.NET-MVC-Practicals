using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical11_PartialView.Models
{
    public enum GenderList
    {
        Male = 1,
        Female = 2
    };

    public class Person
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Id")]
        [Required(ErrorMessage = "Please enter email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select gender")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public GenderList Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please enter date of birth")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
    }
}