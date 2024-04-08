using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace try2.Converters
{
    public class ColorDecreaseSolidColorBrushConverter : IValueConverter
    {
        const double DecreaseCoeff = 0.6;
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return BindingOperations.DoNothing;

            double decrease;
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            if (parameter != null) decrease = Double.Parse((string)parameter, formatter);
            else decrease = DecreaseCoeff;


            var mtype = value.GetType();
            if (mtype == typeof(SolidColorBrush)) 
            {
                var val = (SolidColorBrush)value;
                byte r = (byte)((double)val.Color.R * decrease);
                byte g = (byte)((double)val.Color.G * decrease);
                byte b = (byte)((double)val.Color.B * decrease);
                var col = new Avalonia.Media.Color(255, r, g, b);
                return new SolidColorBrush(col);
            }
            if (mtype == typeof(ImmutableSolidColorBrush))
            {
                var val = (ImmutableSolidColorBrush)value;
                byte r = (byte)((double)val.Color.R * decrease);
                byte g = (byte)((double)val.Color.G * decrease);
                byte b = (byte)((double)val.Color.B * decrease);
                var col = new Avalonia.Media.Color(255, r, g, b);
                return new SolidColorBrush(col);
            }
            return BindingOperations.DoNothing;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
