using System;
using System.Collections.Generic;

namespace Core.Models
{
  public class FilesLoadedEventArgs : EventArgs
  {
    public readonly List<File> Files;

    public FilesLoadedEventArgs(List<File> files)
    {
      Files = files;
    }
  }

  public class File
  {
    public string Name { get; set; }
    public string Path { get; set; }
  }
}
