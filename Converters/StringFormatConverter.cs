using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// General converter for strings.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (value == null || targetType == null)
                return null;
            else if (parameter == null)
                return value.ToString();
            else if (param.Contains("PADLEFT"))
            {
                int n = 0;

                if (int.TryParse(param.Substring(7), out n))
                    return value.ToString().PadLeft(n, '0');
                else
                    return value.ToString().PadLeft(4, '0');
            }
            else if (param.Contains("DATE"))
            {
                string format = param.Substring(4);

                if (value is DateTime)
                    return ((DateTime)value).ToString(format, System.Globalization.CultureInfo.CurrentCulture);
                else throw new Exception("Converter value supossed to be a DateTime is NOT a DateTime");
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
