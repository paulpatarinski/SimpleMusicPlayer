using System;
using Core.Models;

namespace Core.Services
{
  public interface IMusicFileService
  {
    void LoadMusicFiles();
    event EventHandler<MusicFileLoadedEventArgs> MusicFileLoaded;
    event EventHandler<AllFilesLoadedEventArgs> AllMusicFilesLoaded;
  }
}