using System.Reflection;
using SVG.Forms.Plugin.Abstractions;

namespace Core.Pages
{
  public partial class MusicPlayerPage
  {
    public MusicPlayerPage()
    {
      InitializeComponent();
      musicPlayerControlsGrid.Children.Add(new SvgImage
      {
        SvgAssembly = typeof(App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.PlayButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      },2,0);
    }
  }
}
