using HelloAutocadPlugIn.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HelloAutocadPlugIn.Converters
{
    /// <summary>
    /// Конвертр инвертирующий bool и bool?
    /// </summary>
    [ValueConversion(typeof(bool?), typeof(bool))]
    public class InverseBooleanConverter : ConvertorBase<InverseBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                throw new InvalidOperationException("The target must be a nullable boolean");
            return (bool?)value == false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");
            return (bool)value == false;
        }
    }
}
