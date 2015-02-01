using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Native;

namespace Core.Services
{
  public class MusicFileService : IMusicFileService
  {
    private readonly IFileService _fileService;
    private readonly IId3TagService _id3TagService;

    public MusicFileService(IFileService fileService, IId3TagService id3TagService)
    {
      _fileService = fileService;
      _id3TagService = id3TagService;
      _fileService.FilesLoaded += FileServiceOnSongsLoaded;
    }

    private async void FileServiceOnSongsLoaded(object sender, FilesLoadedEventArgs eventArgs)
    {
      if(MusicFilesLoaded == null)
        throw new Exception("You must subscribe to MusicFilesLoaded");

      var musicFiles = new List<MusicFile>();

      foreach (var file in eventArgs.Files)
      {
        var id3Tag = await _id3TagService.GetId3TagAsync(file);

        musicFiles.Add(new MusicFile
        {
          FileName = file.Name,
          ArtistName = id3Tag.Artist,
          AlbumName = id3Tag.Album,
          Genre = id3Tag.Genre,
          SongTitle = id3Tag.Title,
        });
      }

      MusicFilesLoaded(this, new MusicFilesLoadedEventArgs(musicFiles));
    }

    public void LoadMusicFiles()
    {
      _fileService.LoadFiles();
    }

    public event EventHandler<MusicFilesLoadedEventArgs> MusicFilesLoaded;
  }
}