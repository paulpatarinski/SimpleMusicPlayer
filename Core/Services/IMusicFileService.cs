using System;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
  public interface IMusicFileService
  {
    Task LoadMusicFilesAsync();
    event EventHandler<MusicFilesLoadedEventArgs> MusicFilesLoaded;
  }
}