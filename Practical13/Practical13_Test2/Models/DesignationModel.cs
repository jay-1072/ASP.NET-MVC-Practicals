using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical13_Test2.Models
{
    public class DesignationModel
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(50)]
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please enter designation.")]
        public string Designation { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; }
    }
}