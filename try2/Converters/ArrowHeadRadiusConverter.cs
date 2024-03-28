using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace try2.Converters
{
    public class ArrowHeadRadiusConverter : IValueConverter
    {
        const double DecreaseCoeff = 0.95;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            double val = value as double? ?? 0;
            if(parameter != null ) 
            {
                var p = parameter as string;
                if (p == "decrease") val *= DecreaseCoeff;
            }
            Avalonia.Size res = new Avalonia.Size(val / 2, val / 2);
            return res;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
