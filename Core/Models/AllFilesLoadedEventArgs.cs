using System;

namespace Core.Models
{
  public class AllFilesLoadedEventArgs : EventArgs
  {
    public int NumberOfFiles { get; private set; }

    public AllFilesLoadedEventArgs(int numberOfFiles)
    {
      NumberOfFiles = numberOfFiles;
    }
  }
}
