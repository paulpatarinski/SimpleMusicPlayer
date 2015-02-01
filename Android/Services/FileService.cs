using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.EventArgs;
using Core.Services;
using Core.Services.Native;
using XLabs.Ioc;
using File = Core.Models.EventArgs.File;

namespace SimpleMusicPlayer.Android.Services
{
  public class FileService : IFileService
  {
    public event EventHandler<FileLoadedEventArgs> FileLoaded;
    public event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;

    public void LoadFiles(string searchPattern)
    {
      if (FileLoaded == null)
        throw new Exception("You must subscribe to FileLoaded");

      if (AllFilesLoaded == null)
        throw new Exception("You must subscribe to AllFilesLoaded");

      try
      {
        Task.Run(() =>
        {
          var root = Environment.CurrentDirectory;
          var documentsPath = Path.Combine(root, "storage/emulated/0/Music");

          var numberOfFilesLoaded = LoadFilesRecursiveAsync(documentsPath, searchPattern);

          AllFilesLoaded(this, new AllFilesLoadedEventArgs(numberOfFilesLoaded));
        });
      }
      catch (Exception ex)
      {
        Resolver.Resolve<IExceptionHandlingService>().Handle(ex);
      }
    }

    private int LoadFilesRecursiveAsync(string path, string searchPattern)
    {
      if (FileLoaded == null)
        throw new Exception("You must subscribe to FileLoaded");

      var rootDirectoryInfo = new DirectoryInfo(path);

      var fileInfos = rootDirectoryInfo.EnumerateFiles(searchPattern).ToList();

      var numberOfFiles = fileInfos.Count;

      foreach (var file in fileInfos.Select(fileInfo => new File {Name = fileInfo.Name, Path = fileInfo.FullName}))
      {
        FileLoaded(this, new FileLoadedEventArgs(file));
      }

      var directories = rootDirectoryInfo.EnumerateDirectories();

      numberOfFiles += directories.Sum(directoryInfo => LoadFilesRecursiveAsync(directoryInfo.FullName, searchPattern));

      return numberOfFiles;
    }
  }
}