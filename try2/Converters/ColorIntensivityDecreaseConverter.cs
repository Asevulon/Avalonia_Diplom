using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace try2.Converters
{
    public class ColorIntensivityDecreaseConverter : IValueConverter
    {
        const double DecreaseCoeff = 0.6;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)return BindingOperations.DoNothing;

            var val = (Avalonia.Media.Immutable.ImmutableSolidColorBrush)value;
            byte r = (byte)((double)val.Color.R * DecreaseCoeff);
            byte g = (byte)((double)val.Color.G * DecreaseCoeff);
            byte b = (byte)((double)val.Color.B * DecreaseCoeff);
            var col = new Avalonia.Media.Color(255, r, g, b);
            return new SolidColorBrush(col);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
