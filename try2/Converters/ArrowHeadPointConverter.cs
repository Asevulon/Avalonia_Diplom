using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace try2.Converters
{
    public class ArrowHeadPointConverter : IMultiValueConverter
    {
        const double DecreaseCoeff = 0.95;

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 5)
            {
                Trace.WriteLine("Exactly five values expected");
                return BindingOperations.DoNothing;
            }

            double val = values[0] as double? ?? 0;
            double min = values[1] as double? ?? 0;
            double max = values[2] as double? ?? 0;
            double width = values[3] as double? ?? 0;
            double height = values[4] as double? ?? 0;
            if (max - min == 0)
            {
                return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
            }
            if (parameter != null)
            {
                var param = parameter as string;
                if (param == "decrease")
                {
                    width *= DecreaseCoeff;
                    height *= DecreaseCoeff;
                }
            }
            double rad = double.Pi * val/(max -  min);
            double x = width/2 - width * Math.Cos(rad) / 2;
            double y = height * Math.Sin(rad) / 2;
            Avalonia.Point p = new Avalonia.Point(x, -y);
            return p;
        }
    }
}
