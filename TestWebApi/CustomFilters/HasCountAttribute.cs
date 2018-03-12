using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebApi.Custom
{
    public class HasCountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var exists = ((List<string>)value)?.Any() ?? false;

            if (!exists)
            {
                return new ValidationResult("Less count");
            }

            return ValidationResult.Success;
        }
    }
}