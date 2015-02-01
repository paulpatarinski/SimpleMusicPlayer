using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Helpers.Codes;
using Core.Models.EventArgs;
using Core.Services;
using Core.Services.Native;
using Microsoft.Xna.Framework.Media;
using XLabs.Ioc;

namespace WinPhone.Services
{
  public class FileService : IFileService
  {
    public event EventHandler<FileLoadedEventArgs> FileLoaded;
    public event EventHandler<AllFilesLoadedEventArgs> AllFilesLoaded;

    public void LoadFiles(List<FileType> fileTypes)
    {
      if (FileLoaded == null)
        throw new Exception("You must subscribe to FileLoaded");

      if (AllFilesLoaded == null)
        throw new Exception("You must subscribe to AllFilesLoaded");

      try
      {
        Task.Run(() =>
        {
          var mediaLibrary = new MediaLibrary();

          foreach (var song in mediaLibrary.Songs)
          {
            FileLoaded(this, new FileLoadedEventArgs(new File{Name = song.Name}));
          }

          AllFilesLoaded(this, new AllFilesLoadedEventArgs(mediaLibrary.Songs.Count));
        });
      }
      catch (Exception ex)
      {
        Resolver.Resolve<IExceptionHandlingService>().Handle(ex);
      }
    }

    
  }
}