namespace Core.Models.EventArgs
{
  public class FileLoadedEventArgs : System.EventArgs
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
