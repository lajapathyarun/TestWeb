using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestWebApi.Attributes;
using TestWebApi.Custom;

namespace TestWebApi.VIewModels
{
    public class EmployeeViewModel : IValidatableObject
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        [AgeRange(18, 30)]
        public int? Age { get; set; }
        public DateTime Dob { get; set; }
        public decimal Salary { get; set; }

        //[HasCount]
        public List<string> Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Salary < 1000)
            {
                yield return new ValidationResult("InvalidPrice");
            }
        }
    }
}