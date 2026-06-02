
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls;

namespace Budgy;

public class AmountToColorConverter :  IValueConverter
{

    public Color PositiveColor { get; set; } =  Colors.LimeGreen;
    public Color NegativeColor { get; set; } = Colors.Orange;
    public Color NeutralColor { get; set; } = Colors.White;

    public double PositiveThreshold{ get; set; }

    public double NegativeThreshold { get; set; }
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);
    

        if (decimal.TryParse(value.ToString(), out decimal amount))
        {   
            if (amount <= (decimal)PositiveThreshold)
                return PositiveColor;

            if (amount >= (decimal)NegativeThreshold)
                return NegativeColor;

            return NeutralColor;
        }

        throw new ArgumentException("False Value");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
