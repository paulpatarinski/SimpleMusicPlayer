using System;
using System.IO;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Native;
using TagLib;
using File = Core.Models.EventArgs.File;

namespace SimpleMusicPlayer.Android.Services
{
  public class Id3TagService : IId3TagService
  {
    public async Task<Id3Tag> GetId3TagAsync(File file)
    {
      var id3Tag = new Id3Tag();

      try
      {
        await Task.Run(() =>
        {
          using (var fileStream = new FileStream(file.Path, FileMode.OpenOrCreate))
          {
            var tagFile = TagLib.File.Create(new StreamFileAbstraction(file.Name, fileStream, fileStream));

            var tags = tagFile.GetTag(TagTypes.Id3v2);

            id3Tag.Artist = string.IsNullOrEmpty(tags.FirstPerformer) ? tags.FirstAlbumArtist : tags.FirstPerformer;
            id3Tag.Title = tags.Title;
            id3Tag.Album = tags.Album;
            id3Tag.Genre = tags.FirstGenre;
            id3Tag.Year = tags.Year.ToString();
            id3Tag.TrackNumber = tags.Track.ToString();
          }
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return await Task.FromResult(id3Tag);
    }
  }
}