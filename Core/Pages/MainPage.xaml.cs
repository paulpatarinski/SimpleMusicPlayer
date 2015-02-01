using System.Linq;
using Core.Models;
using Core.Pages.Base;
using Core.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Core.Pages
{
  public partial class MainPage : BasePage
  {
    public MainPage()
    {
      InitializeComponent();
      musicFilesListView.ItemSelected += MusicFilesListViewOnItemSelected;
    }

    private async void MusicFilesListViewOnItemSelected(object sender,
      SelectedItemChangedEventArgs selectedItemChangedEventArgs)
    {
      var musicPlayerPage = (Page) ViewFactory.CreatePage<MusicPlayerViewModel, MusicPlayerPage>((model, page) =>
      {
        var mainPageViewModel = BindingContext as MainPageViewModel;

        if (mainPageViewModel != null)
          model.Init((MusicFile) selectedItemChangedEventArgs.SelectedItem, mainPageViewModel.MusicFiles.ToList());
      });

      await Navigation.PushAsync(musicPlayerPage);
    }
  }
}