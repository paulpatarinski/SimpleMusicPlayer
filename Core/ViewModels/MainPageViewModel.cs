using System;
using System.Collections.ObjectModel;
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
    private string _message;
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

    public string Message
    {
      get { return _message; }
      set { SetField(ref _message, value); }
    }

    public ObservableCollection<MusicFile> MusicFiles 
    {
      get { return _musicFiles ?? (_musicFiles = new ObservableCollection<MusicFile>()); }
      set { SetField(ref _musicFiles, value); }
    }

    public ICommand LoadMessageCommand
    {
      get { return _refreshCommand ?? (_refreshCommand = new Command(async () => await ExecuteLoadMessageAsync())); }
    }

    public async Task ExecuteLoadMessageAsync()
    {
      IsBusy = true;

      await _musicFileService.LoadMusicFilesAsync();

      IsBusy = false;
    }
  }
}