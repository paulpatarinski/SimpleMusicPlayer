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
      _fileService.FileLoaded += FileServiceOnFileLoaded;
      _fileService.AllFilesLoaded += FileServiceOnAllFilesLoaded;
    }

    public void LoadMusicFiles()
    {
      _fileService.LoadFiles("*.mp3");
    }

    private void FileServiceOnAllFilesLoaded(object sender, AllFilesLoadedEventArgs e)
    {
      if (AllMusicFilesLoaded == null)
        throw new Exception("You must subscribe to AllMusicFilesLoaded");

      AllMusicFilesLoaded(sender, e);
    }

    private async void FileServiceOnFileLoaded(object sender, FileLoadedEventArgs eventArgs)
    {
      if (MusicFileLoaded == null)
        throw new Exception("You must subscribe to MusicFileLoaded");

      var id3Tag = await _id3TagService.GetId3TagAsync(eventArgs.File);

      var musicFile = new MusicFile
      {
        FileName = eventArgs.File.Name,
        ArtistName = id3Tag.Artist,
        AlbumName = id3Tag.Album,
        Genre = id3Tag.Genre,
        SongTitle = id3Tag.Title,
      };

      MusicFileLoaded(this, new MusicFileLoadedEventArgs(musicFile));
    }

    public event EventHandler<MusicFileLoadedEventArgs> MusicFileLoaded;
    public event EventHandler<AllFilesLoadedEventArgs> AllMusicFilesLoaded;
  }
}