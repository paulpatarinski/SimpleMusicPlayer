using System;
using System.Collections.Generic;

namespace Core.Models
{
  public class MusicFileLoadedEventArgs : EventArgs
  {
    public MusicFile MusicFile { get; private set; }

    public MusicFileLoadedEventArgs(MusicFile musicFile)
    {
      MusicFile = musicFile;
    }
  }
}
