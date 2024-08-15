using System.Globalization;

#pragma warning disable CS8767 // 参数类型中引用类型的为 Null 性与隐式实现的成员不匹配(可能是由于为 Null 性特性)。

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

    public class ScoreToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue switch
                {
                    >= 90000 => Colors.DarkOrange,
                    >= 75000 => Colors.DarkRed,
                    >= 65000 => Colors.Violet,
                    >= 55000 => Colors.DarkGreen,
                    >= 0 => Colors.DarkCyan,
                    _ => throw new NotImplementedException()
                };
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}