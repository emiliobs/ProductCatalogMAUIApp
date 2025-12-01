using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProductCatalog.Helpers;

public class BoolToTextConverter : IValueConverter
{
    public string TrueText { get; set; } = "Sí";
    public string FalseText { get; set; } = "No";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? TrueText : FalseText;
        }
        return FalseText;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}