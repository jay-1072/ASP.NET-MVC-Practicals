using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Practical13_Test2.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(maximumLength:50)]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(maximumLength: 50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Please enter date of birth")]
        public DateTime DOB { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MinLength(10, ErrorMessage = "mobile number must have 10 digits only")]
        [MaxLength(10, ErrorMessage = "mobile number must have 10 digits only")]
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter mobile number")]
        public string MobileNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Please enter salary")]
        public decimal Salary { get; set; }

        [ForeignKey(name: "Designation")]
        [Display(Name = "Designation")]
        public Nullable<int> DesignationId { get; set; }

        public DesignationModel Designation { get; set; }
    }
}
