using Domain.Tests.ViewModels.MachineDetails;
using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Validators
{
    public class DateValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, 
            ValidationContext validationContext)
        {
            DateTime dateTime;
            if(!DateTime.TryParseExact(value?.ToString(),
                MachineDetailsViewModel.DATE_TIME_FORMAT_FOR_RECIEVED_DATA, null, 
                System.Globalization.DateTimeStyles.None, out dateTime))
            {
                return new ValidationResult("Date is not in valid format.");
            }
            else if(dateTime > DateTime.Now.AddMinutes(10))
            {
                return new ValidationResult("Can't aggregate right now.");
            }

            return ValidationResult.Success;
        }
    }
}
