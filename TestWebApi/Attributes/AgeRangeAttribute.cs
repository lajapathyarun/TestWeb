using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebApi.Attributes
{
    public class AgeRangeAttribute : ValidationAttribute
    {
        public AgeRangeAttribute(int startAge, int endAge)
        {
            StartAge = startAge;
            EndAge = endAge;
        }

        public int StartAge { get; set; }
        public int EndAge { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is int))
            {
                throw new ValidationException("DateRange is valid for DateTime property only.");
            }
            else
            {
                //Better to create DateTime extension method to calculate Age  

                if (20 > StartAge || 20 < EndAge)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.MemberName));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

        }

    }
}