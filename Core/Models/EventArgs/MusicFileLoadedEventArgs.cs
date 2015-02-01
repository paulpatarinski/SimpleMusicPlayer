namespace Core.Models.EventArgs
{
  public class MusicFileLoadedEventArgs : System.EventArgs
  {
    public MusicFile MusicFile { get; private set; }

    public MusicFileLoadedEventArgs(MusicFile musicFile)
    {
      MusicFile = musicFile;
    }
  }
}
