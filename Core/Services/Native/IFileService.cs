﻿using System;
using Core.Models.EventArgs;

namespace Core.Services.Native
{
  public interface IFileService
  {
    event EventHandler<FileLoadedEventArgs> FileLoaded;
    event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;
    void LoadFiles(string searchPattern);
  }
}
