using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WindowAuthentication.Models
{
	public class Student
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required]
		public int Standard { get; set; }

		[Required]
		public int Age { get; set; }

		[Required]
		[StringLength(12)]
		public string EnrollmentNumber { get; set; }
	}
}