using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace HelloAutocadPlugIn.Converters
{
    /// <summary>
    /// Конвертр цвета м/у АвтоКАДом и <see cref="ColorPicker"/>
    /// </summary>
    [ValueConversion(typeof(Color?), typeof(Autodesk.AutoCAD.Colors.Color))]
    public class AutocadColorConverter : ConvertorBase<AutocadColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Color?))
                throw new InvalidOperationException("The target must be a nullable Media.Color");
            var adColor = (Autodesk.AutoCAD.Colors.Color) value;
            var color = new Color {A=255 ,R = adColor.Red, G = adColor.Green, B = adColor.Blue};
            return ((Color?) color);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Autodesk.AutoCAD.Colors.Color))
                throw new InvalidOperationException("The target must be a AutoCAD.Colors.Color");
            if (value == null)
                throw new InvalidCastException("Color must be no null");
            Color color = ((Color?) value).Value;
            return Autodesk.AutoCAD.Colors.Color.FromRgb(color.R, color.G, color.B);
        }
    }
}
