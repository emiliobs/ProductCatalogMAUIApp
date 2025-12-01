using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProductCatalog.Helpers;

/// <summary>
/// Converter to check if value is not null
/// Returns true if value is not null, false otherwise
/// </summary>
public class IsNotNullConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}