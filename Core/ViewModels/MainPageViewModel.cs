using System.Collections.ObjectModel;
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
      _musicFileService.MusicFilesLoaded += MusicFileServiceOnMusicFilesLoaded;
    }

    private void MusicFileServiceOnMusicFilesLoaded(object sender, MusicFilesLoadedEventArgs eventArgs)
    {
      foreach (var musicFile in eventArgs.MusicFiles)
      {
        MusicFiles.Add(musicFile);
      }
    }

    public ObservableCollection<MusicFile> MusicFiles 
    {
      get { return _musicFiles ?? (_musicFiles = new ObservableCollection<MusicFile>()); }
      set { SetField(ref _musicFiles, value); }
    }

    public ICommand LoadSongsCommand
    {
      get { return _refreshCommand ?? (_refreshCommand = new Command(ExecuteLoadSongs)); }
    }

    public void ExecuteLoadSongs()
    {
      IsBusy = true;

      _musicFileService.LoadMusicFiles();

      IsBusy = false;
    }
  }
}