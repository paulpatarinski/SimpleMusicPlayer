using System;
using Core.Models;

namespace Core.Services
{
  public interface IFileService
  {
    event EventHandler<FilesLoadedEventArgs> FilesLoaded;
    void LoadFiles();
  }
}
