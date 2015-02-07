using System;
using Core.Services.Native;

namespace WinPhone.Services
{
  public class MediaPlayerService : IMediaPlayerService
  {
    public void Play(string fileName, string filePath)
    {
    }

    public void Pause()
    {
    }

    public void Resume()
    {
    }

    public void Stop()
    {
    }

    public void Release()
    {
    }

    public bool IsPlaying
    {
      get { return false; }
    }

    public event EventHandler Completion;
  }
}
