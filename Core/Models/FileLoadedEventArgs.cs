using System;
using System.Collections.Generic;

namespace Core.Models
{
  public class FileLoadedEventArgs : EventArgs
  {
    public readonly File File;

    public FileLoadedEventArgs(File file)
    {
      File = file;
    }
  }

  public class File
  {
    public string Name { get; set; }
    public string Path { get; set; }
  }
}
