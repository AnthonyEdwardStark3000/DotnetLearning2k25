using System;
using System.ComponentModel.DataAnnotations; // for model validation

namespace Services.Helpers
{
    /// <summary>
    /// Helper method to perform model validations
    /// </summary>
    public class ValidationHelper
    {
        public static void ModelValidation(object obj)
        {
            // Model validations
            ValidationContext validationContext = new ValidationContext(obj);
            // Errors that happened during validation
            List<ValidationResult> validationResults = new List<ValidationResult>();
            // validate
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}