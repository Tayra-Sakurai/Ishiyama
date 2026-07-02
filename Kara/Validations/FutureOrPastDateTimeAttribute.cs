using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Validations
{
    public sealed class FutureOrPastDateTimeAttribute : ValidationAttribute
    {
        public bool IsFuture { get; }

        public FutureOrPastDateTimeAttribute(bool isFuture)
        {
            IsFuture = isFuture;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTimeOffset dateTimeOffset)
                return new(ErrorMessage ?? "Is not a date and time value.");

            if (IsFuture && dateTimeOffset < DateTimeOffset.Now)
                return new(ErrorMessage ?? "This is not a future date and time.");

            if (!IsFuture && dateTimeOffset > DateTimeOffset.Now)
                return new(ErrorMessage ?? "This is not in the past.");

            return ValidationResult.Success;
        }
    }
}
