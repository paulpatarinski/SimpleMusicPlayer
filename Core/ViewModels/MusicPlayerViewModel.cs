using Core.Models;
using Core.ViewModels.Base;
using Xamarin.Forms;

namespace Core.ViewModels
{
  public class MusicPlayerViewModel : BaseViewModel
  {
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

      PlaySong();
    }

    private void PlaySong()
    {
      //throw new System.NotImplementedException();
    }
  }
}