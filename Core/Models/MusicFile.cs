using Xamarin.Forms;

namespace Core.Models
{
  public class MusicFile
  {
    public string ArtistName { get; set; }
    public string AlbumName { get; set; }
    public string Genre { get; set; }
    public string SongTitle { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public ImageSource AlbumArt { get; set; }

    public string MusicFileName
    {
      get
      {
        if (string.IsNullOrEmpty(SongTitle))
          return FileName;

        return SongTitle;
      }
    }

  }
}