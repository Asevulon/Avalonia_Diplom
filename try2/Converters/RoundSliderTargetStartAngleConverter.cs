using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace try2.Converters
{
    public class RoundSliderTargetStartAngleConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 4)
            {
                Trace.WriteLine("Exactly four values expected");
                return BindingOperations.DoNothing;
            }

            double range;
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            if (parameter != null) range = Double.Parse((string)parameter, formatter);
            else range = 180;

            double val = values[0] as double? ?? 0;
            double min = values[1] as double? ?? 0;
            double max = values[2] as double? ?? 0;
            double sweep = values[3] as double? ?? 0;

            if (max - min == 0)
            {
                return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
            }
            double res = range * (val - min) / (max - min);
            return res - sweep / 2 + 90;
        }
    }
}
