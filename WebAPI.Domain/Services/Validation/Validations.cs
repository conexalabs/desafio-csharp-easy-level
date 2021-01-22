using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebAPI.Domain.Interfaces.Service.Validation;

namespace WebAPI.Domain.Services.Validation
{
    public class Validations : IValidations
    {
        public bool IsNumber(string value)
        {
            return Double.TryParse(value, out _);
        }
        public IEnumerable<ValidationResult> ValidateLatitudeLongitude(bool latitude, bool longitude)
        {
            var resultValidation = new List<ValidationResult>();
            List<string> members = new List<string>() { "latitude", "longitude" };
            if(!latitude && !longitude)
            {
                resultValidation.Add(item: new ValidationResult("Erro ao converter latitude/longitude!", members));
            }

            return resultValidation;
        }
        public IEnumerable<ValidationResult> ValidateModel<T>(T model) where T : class
        {
            var resultValidation = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, resultValidation, true);
            return resultValidation;
        }
        public IEnumerable<ValidationResult> ValidateCityName(string cityName)
        {
            var resultValidation = new List<ValidationResult>();
            List<string> members = new List<string>() { "cityname" };
            if (cityName == null)
            {
                resultValidation.Add(item: new ValidationResult("Preencha o nome da cidade!", members));
            }

            return resultValidation;
        }
    }
}
