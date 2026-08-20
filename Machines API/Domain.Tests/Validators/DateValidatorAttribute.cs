using Domain.Tests.Controllers.MachineDatasAggregated;
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
                MachinesDatasAggregatedController.DATE_TIME_FORMAT, null, 
                System.Globalization.DateTimeStyles.None, out dateTime))
            {
                return new ValidationResult("Date is not in valid format.");
            }
            else if(dateTime.Minute % 10 != 0)
            {
                return new ValidationResult("Minutes must result 0 when modulo by 10.");
            }
            else if(dateTime > DateTime.Now.AddMinutes(10))
            {
                return new ValidationResult("Can't aggregate right now.");
            }

            return ValidationResult.Success;
        }
    }
}
