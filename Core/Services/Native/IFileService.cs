using System;
using Core.Models;

namespace Core.Services
{
  public interface IFileService
  {
    event EventHandler<FileLoadedEventArgs> FileLoaded;
    event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;
    void LoadFiles(string searchPattern);
  }
}
