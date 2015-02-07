using System;
using System.Globalization;
using Xamarin.Forms;

namespace Core.Helpers.ValueConverters
{
  public class OppositeBooleanValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return !((bool) value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
