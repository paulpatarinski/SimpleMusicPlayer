namespace Core.Models.EventArgs
{
  public class AllFilesLoadedEventArgs : System.EventArgs
  {
    public int NumberOfFiles { get; private set; }

    public AllFilesLoadedEventArgs(int numberOfFiles)
    {
      NumberOfFiles = numberOfFiles;
    }
  }
}
