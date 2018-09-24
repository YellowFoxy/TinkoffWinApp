using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace TinkoffWinApp.Support.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        private static string hexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
        private static Regex regexHexMatch = new Regex(hexPattern);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var revers = parameter != null && parameter.ToString() == "Revers";
            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;
            var brush = revers ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);

            string colorStr = value as string;

            if (string.IsNullOrEmpty(colorStr) || !regexHexMatch.IsMatch(colorStr))
                return brush;

            var hex = colorStr.Replace("#", string.Empty);

            bool withAlfa = hex.Length == 8;
            int start = 0;
            if (withAlfa)
            {
                if (!byte.TryParse(hex.Substring(start, 2), NumberStyles.HexNumber, null, out a))
                    return brush;
                start += 2;
            }

            if (!byte.TryParse(hex.Substring(start, 2), NumberStyles.HexNumber, null, out r))
                return brush;
            start += 2;
            if (!byte.TryParse(hex.Substring(start, 2), NumberStyles.HexNumber, null, out g))
                return brush;
            start += 2;
            if (!byte.TryParse(hex.Substring(start, 2), NumberStyles.HexNumber, null, out b))
                return brush;

            if (parameter != null && parameter.ToString() == "Revers")
            {
                brush.Color = Color.FromArgb(a, (byte)(255 - r), (byte)(255 - g), (byte)(255 - b));
            }
            else
            {
                brush.Color = Color.FromArgb(a, r, g, b);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return string.Empty;
        }
    }
}
