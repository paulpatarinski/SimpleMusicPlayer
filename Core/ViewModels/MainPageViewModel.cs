using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Models;
using Core.Models.EventArgs;
using Core.Services;
using Core.ViewModels.Base;
using Xamarin.Forms;

namespace Core.ViewModels
{
  public class MainPageViewModel : BaseViewModel
  {
    private readonly IMusicFileService _musicFileService;
    private ICommand _refreshCommand;
    private ObservableCollection<MusicFile> _musicFiles;

    public MainPageViewModel(IMusicFileService musicFileService)
    {
      _musicFileService = musicFileService;
      _musicFileService.MusicFileLoaded += MusicFileServiceOnMusicFileLoaded;
      _musicFileService.AllMusicFilesLoaded += MusicFileServiceOnAllMusicFilesLoaded;
    }

    private void MusicFileServiceOnAllMusicFilesLoaded(object sender, AllFilesLoadedEventArgs eventArgs)
    {
      Debug.WriteLine(eventArgs.NumberOfFiles);
    }

    private void MusicFileServiceOnMusicFileLoaded(object sender, MusicFileLoadedEventArgs eventArgs)
    {
      Device.BeginInvokeOnMainThread(() =>
      {
        MusicFiles.Add(eventArgs.MusicFile);
      });
    }

    public ObservableCollection<MusicFile> MusicFiles 
    {
      get { return _musicFiles ?? (_musicFiles = new ObservableCollection<MusicFile>()); }
      set { SetProperty(ref _musicFiles, value); }
    }

    public void LoadSongs()
    {
      IsBusy = true;

      _musicFileService.LoadMusicFiles();

      IsBusy = false;
    }
  }
}