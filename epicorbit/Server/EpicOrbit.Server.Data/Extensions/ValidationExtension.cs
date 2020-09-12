using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Server.Data.Extensions {
    public static class ValidationExtension {

        public static bool IsValid<T>(this T obj, out string message) {
            message = null;

            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(obj, context, results, true)) {
                message = results[0].ErrorMessage;
                return false;
            }
            return true;
        }

    }
}
