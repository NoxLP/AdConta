using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    #region converters
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
    /// <summary>
    /// Converter for TabbedExpander Asiento datagrid height.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class AsientoDGridHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (value == null || targetType == null)
                return null;
            else if (parameter == null)
                return value;

            switch (param)
            {
                case "AS":
                    double dValue = (double)value;
                    return dValue - 110;
                case "TABBEDDIARIO":
                    dValue = (double)value;
                    return dValue - 83;
                case "WINDOWED":
                    dValue = (double)value;
                    return dValue - 86;
                case "VMSECHEIGHT":
                    //GridLength gValue = (GridLength)value;
                    //return gValue.Value;
                    dValue = (double)value;
                    return dValue;
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Converter for Ribbon width stretcher.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class RibbonActualWidthToRGroupWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (value == null || targetType == null)
                return null;
            else if (parameter == null)
                return value;
            else if (param.Contains("NORMAL"))
            {
                double dValue = (double)value;
                return dValue - 5;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Converter for Ribbon height expander behaviour.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class BoolToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (value == null || targetType == null)
                return null;
            else if (parameter == null)
                return value;

            switch (param)
            {
                case "TogToGrid":
                    bool bValue = (bool)value;
                    return bValue ?
                        new System.Windows.GridLength(13.5d, System.Windows.GridUnitType.Pixel) :
                        new System.Windows.GridLength(85d, System.Windows.GridUnitType.Pixel);
                case "TEIsExpandedToSplitterHeight":
                    bValue = (bool)value;
                    return bValue ? 4 : 0;
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class BoolHeightToHeightMulticonverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (values == null || targetType == null)
                return null;
            else if (parameter == null)
                return values;

            bool bValue = (bool)values[0];
            double gridValue = (double)values[1];
            double TEValue = (double)values[2];
            switch (param)
            {
                case "RowToTE":
                    return bValue ?
                        gridValue :
                        TEValue;
                case "TEToRow":
                    return bValue ?
                        new GridLength(gridValue, GridUnitType.Pixel) :
                        new GridLength(TEValue + 5, GridUnitType.Pixel);
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Converter for Splitter visibility.
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;

            if (value == null || targetType == null)
                return null;
            else if (parameter == null)
                return null;

            switch (param)
            {
                case "TESplitter":
                    bool bValue = (bool)value;
                    return bValue ? Visibility.Visible : Visibility.Collapsed;
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    #endregion
}
