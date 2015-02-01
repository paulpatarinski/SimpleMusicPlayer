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
        model.Init((MusicFile) selectedItemChangedEventArgs.SelectedItem);
      });

      await Navigation.PushAsync(musicPlayerPage);
    }
  }
}