using System;
using System.IO;
using System.Linq;
using Core.Models;
using Core.Services;
using File = Core.Models.File;

namespace SimpleMusicPlayer.Android.Services
{
  public class FileService : IFileService
  {
    public event EventHandler<FilesLoadedEventArgs> FilesLoaded;

    public void LoadFiles()
    {
      if (FilesLoaded == null)
        throw new Exception("You must subscribe to FilesLoaded");

      try
      {
        var root = Environment.CurrentDirectory;
        var documentsPath = Path.Combine(root, "storage/emulated/0/Music");

        var directoryInfo = new DirectoryInfo(documentsPath);

        var fileInfos = directoryInfo.EnumerateFiles();

        var files = fileInfos.Select(fileInfo => new File {Name = fileInfo.Name, Path = fileInfo.FullName}).ToList();

        FilesLoaded(this, new FilesLoadedEventArgs(files));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }
}