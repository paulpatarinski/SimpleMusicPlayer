using System.Reflection;
using Core.ViewModels;
using SVG.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Core.Pages
{
  public partial class MusicPlayerPage
  {
    public MusicPlayerPage()
    {
      InitializeComponent();

      CreatePlayBtn();
      CreatePauseBtn();
    }

    private void CreatePlayBtn()
    {
      var playBtn = new SvgImage
      {
        SvgAssembly = typeof (App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.PlayButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      };

      playBtn.SetBinding<MusicPlayerViewModel>(IsVisibleProperty, vm => vm.IsPlayBtnVisibile);
      var playBtnTapRecognizer = new TapGestureRecognizer();

      playBtnTapRecognizer.SetBinding<MusicPlayerViewModel>(TapGestureRecognizer.CommandProperty,
        vm => vm.TogglePlayPauseCommand);

      playBtn.GestureRecognizers.Add(playBtnTapRecognizer);

      musicPlayerControlsGrid.Children.Add(playBtn, 2, 0);
    }

    private void CreatePauseBtn()
    {
      var playBtn = new SvgImage
      {
        SvgAssembly = typeof(App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.PauseButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      };

      playBtn.SetBinding<MusicPlayerViewModel>(IsVisibleProperty, vm => vm.IsPauseBtnVisible);
      var playBtnTapRecognizer = new TapGestureRecognizer();

      playBtnTapRecognizer.SetBinding<MusicPlayerViewModel>(TapGestureRecognizer.CommandProperty,
        vm => vm.TogglePlayPauseCommand);

      playBtn.GestureRecognizers.Add(playBtnTapRecognizer);

      musicPlayerControlsGrid.Children.Add(playBtn, 2, 0);
    }
  }
}
