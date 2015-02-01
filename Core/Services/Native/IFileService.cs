using System;
using System.Collections.Generic;
using Core.Helpers.Codes;
using Core.Models.EventArgs;

namespace Core.Services.Native
{
  public interface IFileService
  {
    event EventHandler<FileLoadedEventArgs> FileLoaded;
    event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;
    void LoadFiles(List<FileType> fileTypes);
  }
}
