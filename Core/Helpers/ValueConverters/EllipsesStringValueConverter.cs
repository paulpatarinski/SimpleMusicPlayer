using System;
using System.Globalization;
using Core.Helpers.Extensions;
using Xamarin.Forms;

namespace Core.Helpers.ValueConverters
{
  public class EllipsesStringValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return string.Empty;

      var length = 25;

      if (parameter != null)
      {
        length = Int32.Parse(parameter.ToString());
      }

      return value.ToString().TruncateWithEllipsis(length);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
