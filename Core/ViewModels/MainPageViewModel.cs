using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Models;
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
      IsLoadButtonEnabled = true;
    }

    private void MusicFileServiceOnAllMusicFilesLoaded(object sender, AllFilesLoadedEventArgs eventArgs)
    {
      IsLoadButtonEnabled = true;
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

    private bool _isLoadButtonEnabled;

    public bool IsLoadButtonEnabled
    {
      get { return _isLoadButtonEnabled; }
      set
      {
        SetProperty(ref _isLoadButtonEnabled, value);
      }
    }

    public ICommand LoadSongsCommand
    {
      get { return _refreshCommand ?? (_refreshCommand = new Command(async () =>
      {

        await ExecuteLoadSongs();
      })); }
    }

    public async Task ExecuteLoadSongs()
    {
      IsLoadButtonEnabled = false;
      IsBusy = true;

      _musicFileService.LoadMusicFiles();

      IsBusy = false;
    }
  }
}