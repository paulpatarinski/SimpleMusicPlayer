using System;
using System.Threading.Tasks;
using Android.Media;
using Core.Models;
using Core.Services.Native;

namespace SimpleMusicPlayer.Android.Services
{
  public class Id3TagService : IId3TagService
  {
    public async Task<Id3Tag> GetId3TagAsync(string musicFilePath)
    {
      var id3Tag = new Id3Tag();

      try
      {
        var reader = new MediaMetadataRetriever();

        reader.SetDataSource(musicFilePath);

        id3Tag.Artist = reader.ExtractMetadata(MetadataKey.Artist);
        id3Tag.Title = reader.ExtractMetadata(MetadataKey.Title);
        id3Tag.Album = reader.ExtractMetadata(MetadataKey.Album);
        id3Tag.Genre = reader.ExtractMetadata(MetadataKey.Genre);
        id3Tag.Year = reader.ExtractMetadata(MetadataKey.Year);
        id3Tag.Track = reader.ExtractMetadata(MetadataKey.NumTracks);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return await Task.FromResult(id3Tag);
    }
  }
}