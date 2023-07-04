using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practical12_Test3.Models
{
    public class EmployeeWithDesignationModel : EmployeeModel
    {
        public string Designation { get; set; }

        public int Count { get; set; }
    }
}