﻿using System.Globalization;

namespace PIToolRemake
{
    public class FeatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue switch
                {
                    1 => "[Buster]",
                    2 => "[Arts]",
                    3 => "[Buster][Arts]",
                    4 => "[Study]",
                    5 => "[Buster][Study]",
                    6 => "[Arts][Study]",
                    7 => "[Buster][Arts][Study]",
                    0 => "[Balanced]",
                    _ => "[Balanced]",
                };
            }
            return "[Null]";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PackageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (MauiProgram.Packages.TryGetValue(stringValue, out var packageName))
                    return packageName;
                else
                    return "Single";
            }
            return "Single";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}