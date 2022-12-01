using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace LitterService.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }
        public ValidationException(List<ValidationResult> validationResults)
        {
            ValidationErrors = new List<string>();
            foreach (var validation in validationResults)
            {
                foreach (var error in validation.Errors)
                    ValidationErrors.Add(error.ErrorMessage);
            }
        }
    }
}