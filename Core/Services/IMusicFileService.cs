using System;
using Core.Models;

namespace Core.Services
{
  public interface IMusicFileService
  {
    void LoadMusicFiles();
    event EventHandler<MusicFilesLoadedEventArgs> MusicFilesLoaded;
  }
}