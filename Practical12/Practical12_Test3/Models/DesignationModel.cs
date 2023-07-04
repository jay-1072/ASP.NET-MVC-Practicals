using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical12_Test3.Models
{
    public class DesignationModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please enter designation.")]
        public string Designation { get; set; }
    }
}