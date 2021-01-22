using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.Interfaces.Service.Validation
{
    public interface IValidations
    {
        bool IsNumber(string value);
        public IEnumerable<ValidationResult> ValidateModel<T>(T model) where T : class;
        public IEnumerable<ValidationResult> ValidateLatitudeLongitude(bool latitude, bool longitude);
        public IEnumerable<ValidationResult> ValidateCityName(string cityName);
    }
}
