using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.EventArgs;
using Core.Services.Native;
using Microsoft.Xna.Framework.Media;

namespace WinPhone.Services
{
  public class Id3TagService : IId3TagService
  {
    public Task<Id3Tag> GetId3TagAsync(File file)
    {
      var mediaLibrary = new MediaLibrary();

      var song = mediaLibrary.Songs.FirstOrDefault(x => x.Name.Equals(file.Name));

      var id3Tag = new Id3Tag();

      if (song != null)
      {
        id3Tag.Artist = song.Artist.Name;
        id3Tag.Title = song.Name;
        id3Tag.Album = song.Album.Name;
        id3Tag.Genre = song.Genre.Name;
        id3Tag.TrackNumber = song.TrackNumber.ToString();
      }

      return Task.FromResult(id3Tag);
    }
  }
}
