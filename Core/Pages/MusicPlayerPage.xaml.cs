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

      CreatePreviousBtn();
      CreatePlayBtn();
      CreatePauseBtn();
      CreateNextBtn();
    }

    private void CreatePreviousBtn()
    {
      var previousBtn = new SvgImage
      {
        SvgAssembly = typeof(App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.PreviousButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      };

      var previousBtnTapRecognizer = new TapGestureRecognizer();

      previousBtnTapRecognizer.SetBinding<MusicPlayerViewModel>(TapGestureRecognizer.CommandProperty,
        vm => vm.PlayPreviousSongCommand);

      previousBtn.GestureRecognizers.Add(previousBtnTapRecognizer);

      musicPlayerControlsGrid.Children.Add(previousBtn, 1, 1);
    }

    private void CreatePlayBtn()
    {
      var playBtn = new SvgImage
      {
        SvgAssembly = typeof(App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.PlayButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      };

      playBtn.SetBinding<MusicPlayerViewModel>(IsVisibleProperty, vm => vm.IsPlayBtnVisibile);
      var playBtnTapRecognizer = new TapGestureRecognizer();

      playBtnTapRecognizer.SetBinding<MusicPlayerViewModel>(TapGestureRecognizer.CommandProperty,
        vm => vm.TogglePlayPauseCommand);

      playBtn.GestureRecognizers.Add(playBtnTapRecognizer);

      musicPlayerControlsGrid.Children.Add(playBtn, 3, 1);
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

      musicPlayerControlsGrid.Children.Add(playBtn, 3, 1);
    }


    private void CreateNextBtn()
    {
      var nextBtn = new SvgImage
      {
        SvgAssembly = typeof(App).GetTypeInfo().Assembly,
        SvgPath = "Core.Images.NextButton.svg",
        HeightRequest = 100,
        WidthRequest = 100
      };

      var nextBtnTapRecognizer = new TapGestureRecognizer();

      nextBtnTapRecognizer.SetBinding<MusicPlayerViewModel>(TapGestureRecognizer.CommandProperty,
        vm => vm.PlayNextSongCommand);

      nextBtn.GestureRecognizers.Add(nextBtnTapRecognizer);

      musicPlayerControlsGrid.Children.Add(nextBtn, 5, 1);
    }


    
  }
}
