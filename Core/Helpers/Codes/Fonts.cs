using Xamarin.Forms;

namespace Core.Helpers.Codes
{
  public static class Fonts
  {
    public static Font LargeBold
    {
      get
      {
        var font = Font.Default;

        Device.OnPlatform(iOS: () =>
        {
          font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold);
        },
          Android: () =>
          {
            font = Font.SystemFontOfSize(18, FontAttributes.Bold);
          },
          WinPhone: () =>
          {
            font = Font.SystemFontOfSize(24, FontAttributes.Bold);
          });

        return font;
      }
    }

    public static Font Large
    {
      get
      {
        var font = Font.Default;

        Device.OnPlatform(iOS: () =>
        {
          font = Font.SystemFontOfSize(NamedSize.Large);
        },
          Android: () =>
          {
            font = Font.SystemFontOfSize(18);
          },
          WinPhone: () =>
          {
            font = Font.SystemFontOfSize(24);
          });

        return font;
      }
    }

    public static Font Medium
    {
      get
      {
        var font = Font.Default;

        Device.OnPlatform(iOS: () =>
        {
          font = Font.SystemFontOfSize(NamedSize.Medium);
        },
          Android: () =>
          {
            font = Font.SystemFontOfSize(14);
          },
          WinPhone: () =>
          {
            font = Font.SystemFontOfSize(22);
          });

        return font;
      }
    }
  }
}