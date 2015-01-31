using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
  public class MusicFileService : IMusicFileService
  {
    private readonly IFileService _fileService;

    public MusicFileService(IFileService fileService)
    {
      _fileService = fileService;
      _fileService.FilesLoaded += FileServiceOnSongsLoaded;
    }

    private void FileServiceOnSongsLoaded(object sender, FilesLoadedEventArgs eventArgs)
    {
      if(MusicFilesLoaded == null)
        throw new Exception("You must subscribe to MusicFilesLoaded");

      var musicFiles = new List<MusicFile>();

      foreach (var file in eventArgs.Files)
      {
        musicFiles.Add(new MusicFile{ArtistName = file.Name});
      }

      MusicFilesLoaded(this, new MusicFilesLoadedEventArgs(musicFiles));
    }

    public async Task LoadMusicFilesAsync()
    {
      _fileService.LoadFiles();
    }

    public event EventHandler<MusicFilesLoadedEventArgs> MusicFilesLoaded;
  }
}