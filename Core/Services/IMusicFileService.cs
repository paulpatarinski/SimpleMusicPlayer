using System;
using Core.Models;
using Core.Models.EventArgs;

namespace Core.Services
{
  public interface IMusicFileService
  {
    void LoadMusicFiles();
    event EventHandler<MusicFileLoadedEventArgs> MusicFileLoaded;
    event EventHandler<AllFilesLoadedEventArgs> AllMusicFilesLoaded;
  }
}