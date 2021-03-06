﻿using Xamarin.Forms;

namespace Core.Models
{
  public class Id3Tag
  {
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Album { get; set; }
    public string Genre { get; set; }
    public string Year { get; set; }
    public string TrackNumber { get; set; }
    public ImageSource AlbumArt { get; set; }
  }
}