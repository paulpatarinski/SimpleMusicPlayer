using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        Task.Run(() =>
        {
          var root = Environment.CurrentDirectory;
          var documentsPath = Path.Combine(root, "storage/emulated/0/Music");

          var fileInfos = GetFilesRecursiveAsync(documentsPath);

          var files = fileInfos.Select(fileInfo => new File {Name = fileInfo.Name, Path = fileInfo.FullName}).ToList();

          FilesLoaded(this, new FilesLoadedEventArgs(files));
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    private List<FileInfo> GetFilesRecursiveAsync(string path)
    {
      var rootDirectoryInfo = new DirectoryInfo(path);

      var fileInfos = rootDirectoryInfo.EnumerateFiles("*.mp3").ToList();

      var directories = rootDirectoryInfo.EnumerateDirectories();

      foreach (var directoryInfo in directories)
      {
        fileInfos.AddRange(GetFilesRecursiveAsync(directoryInfo.FullName));
      }

      return fileInfos;
    }
  }
}