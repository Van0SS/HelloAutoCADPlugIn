using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HelloAutocadPlugIn.Validators
{
    /// <summary>
    /// Проверка имени слоя
    /// </summary>
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            // Автокад не проверяет длину имени слоя и позволяет её превысить, потом только при
            // редактировании из программы испраляет, можно написать в багтрекер

            // Длина не должна быть длинее 255 символов
            if (value.ToString().Length > 255)
                return new ValidationResult
                (false, "Name cannot be more than 255 characters long.");
            return ValidationResult.ValidResult;
        }
    }
}
