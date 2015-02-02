using System;

namespace Core.Services.Native
{
  public interface IMediaPlayerService
  {
    void Play(string fileName, string filePath);
    void Pause();
    void Resume();
    void Stop();
    void Release();
    bool IsPlaying { get; }
    event EventHandler Completion;
  }
}
