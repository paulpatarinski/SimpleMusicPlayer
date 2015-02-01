using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Core.Helpers.Codes;
using Core.Models.EventArgs;
using Core.Services;
using Core.Services.Native;
using XLabs.Ioc;

namespace WinPhone.Services
{
  public class FileService_WinPhone8 : IFileService
    {
    public event EventHandler<FileLoadedEventArgs> FileLoaded;
    public event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;
    public void LoadFiles(List<FileType> fileTypes)
    {
      throw new NotImplementedException();
    }


    private async Task<int> LoadFilesRecursiveAsync(StorageFolder storageFolder, List<FileType> fileTypes)
    {
      if (FileLoaded == null)
        throw new Exception("You must subscribe to FileLoaded");

      var numberOfLoadedFiles = 0;
      const uint maxNumFilesToRetrieve = 10;

      for (uint i = 0; i < int.MaxValue; i = i + maxNumFilesToRetrieve)
      {
        var storageFiles = await GetStorageFilesByRangeAsync(storageFolder, ConvertFileTypesToStrings(fileTypes), i, maxNumFilesToRetrieve);

        numberOfLoadedFiles += storageFiles.Count;

        if (storageFiles.Count == 0)
          break;

        foreach (
          var file in storageFiles.Select(storageFile => new File { Name = storageFile.Name, Path = storageFile.Path }))
        {
          FileLoaded(this, new FileLoadedEventArgs(file));
        }
      }

      return numberOfLoadedFiles;
    }

    private List<string> ConvertFileTypesToStrings(List<FileType> fileTypes)
    {
      //todo add other types support
      return new List<string> { ".mp3" };
    }

    private async Task<List<StorageFile>> GetStorageFilesByRangeAsync(StorageFolder storageFolder, List<string> fileTypes, uint startIndex, uint maxItemsToRetrieve)
    {
      try
      {
        var storageFiles =
          (await storageFolder.GetFilesAsync(CommonFileQuery.OrderByMusicProperties, startIndex, maxItemsToRetrieve))
            .ToList();

        //Get only the music files
        var musicFiles = storageFiles.Where(x => fileTypes.Contains(x.FileType)).ToList();

        return musicFiles;
      }
      catch (Exception ex)
      {
        Resolver.Resolve<IExceptionHandlingService>().Handle(ex);
        return new List<StorageFile>();
      }
    }
    }
}
