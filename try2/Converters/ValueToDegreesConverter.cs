using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace try2.Converters
{
    public class ValueToDegreesConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if(values.Count!=3)
            {
                Trace.WriteLine("Exactly three values expected");
                return BindingOperations.DoNothing;
            }

            double val = values[0] as double? ?? 0;
            double min = values[1] as double? ?? 0;
            double max = values[2] as double? ?? 0;

            if (max - min == 0)
            {
                return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
            }
            double res = 180 * val/(max - min);
            return res;
        }
    }
}
