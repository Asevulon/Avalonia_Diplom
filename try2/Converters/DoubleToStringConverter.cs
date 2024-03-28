using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace try2.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var val = value as double? ?? 0;
            var res = val.ToString("N2");
            return res;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
