using System;
using Android.Media;
using Core.Services.Native;

namespace SimpleMusicPlayer.Android.Services
{
  public class MediaPlayerService : IMediaPlayerService
  {
    public MediaPlayerService()
    {
      MediaPlayer.Completion += MediaPlayerOnCompletion;  
    }

    public event EventHandler Completion;

    private void MediaPlayerOnCompletion(object sender, EventArgs eventArgs)
    {
      if (Completion != null) 
          Completion(sender, eventArgs);
    }

    private static MediaPlayer _mediaPlayer;

    public static MediaPlayer MediaPlayer
    {
      get { return _mediaPlayer ?? (_mediaPlayer = new MediaPlayer()); }
    }

    public void Play(string fileName, string filePath)
    {
      MediaPlayer.Reset();
      MediaPlayer.SetDataSource(filePath);
      MediaPlayer.Prepare();
      MediaPlayer.Start();
    }

    public void Pause()
    {
      MediaPlayer.Pause();
    }

    public void Resume()
    {
     MediaPlayer.Start();
    }

    public void Stop()
    {
      MediaPlayer.Stop();
    }

    public void Release()
    {
      MediaPlayer.Release();
    }

    public bool IsPlaying
    {
      get { return MediaPlayer.IsPlaying; }
    }

  }
}