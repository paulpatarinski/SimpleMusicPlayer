using System;
using System.Collections.Generic;

namespace Core.Models
{
  public class MusicFilesLoadedEventArgs : EventArgs
  {
    public List<MusicFile> MusicFiles { get; private set; }

    public MusicFilesLoadedEventArgs(List<MusicFile> musicFiles)
    {
      MusicFiles = musicFiles;
    }
  }
}
