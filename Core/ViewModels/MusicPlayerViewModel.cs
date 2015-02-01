using Core.Models;
using Core.Services.Native;
using Core.ViewModels.Base;
using Xamarin.Forms;

namespace Core.ViewModels
{
  public class MusicPlayerViewModel : BaseViewModel
  {
    private readonly IMediaPlayerService _mediaPlayerService;

    public MusicPlayerViewModel(IMediaPlayerService mediaPlayerService)
    {
      _mediaPlayerService = mediaPlayerService;
    }

    private MusicFile _musicFile;

    public MusicFile MusicFile
    {
      get { return _musicFile; }
      set
      {
        SetProperty(ref _musicFile, value); 
      }
    }

    public void Init(MusicFile selectedMusicFile)
    {
      MusicFile = selectedMusicFile;

      ExecutePlay(MusicFile);
    }

    private Command _togglePlayPauseCommand;

    public Command TogglePlayPauseCommand
    {
      get
      {
        return _togglePlayPauseCommand ?? (_togglePlayPauseCommand = new Command(async () =>
        {
          if (_mediaPlayerService.IsPlaying)
          {
            ExecutePause();
          }
          else
          {
            ExecuteResume();
          }
        }));
      }
    }

    private void ExecutePlay(MusicFile musicFileToPlay)
    {
      _mediaPlayerService.Play(musicFileToPlay.FileName, musicFileToPlay.FilePath);

    }

    private void ExecuteResume()
    {
      _mediaPlayerService.Resume();
    }

    private void ExecutePause()
    {
      _mediaPlayerService.Pause();
    }
  }
}