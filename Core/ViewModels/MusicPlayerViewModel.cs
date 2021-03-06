﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Models;
using Core.Services.Native;
using Core.ViewModels.Base;
using Xamarin.Forms;

namespace Core.ViewModels
{
  public class MusicPlayerViewModel : BaseViewModel
  {
    private readonly IMediaPlayerService _mediaPlayerService;

    public MusicPlayerViewModel(IMediaPlayerService mediaPlayerService)
    {
      _mediaPlayerService = mediaPlayerService;
      _mediaPlayerService.Completion += MediaPlayerServiceOnCompletion;
    }

    private void MediaPlayerServiceOnCompletion(object sender, EventArgs eventArgs)
    {
      ExecutePlayNext();
    }

    private MusicFile _selectedMusicFile;

    public MusicFile SelectedMusicFile
    {
      get { return _selectedMusicFile; }
      set
      {
        SetProperty(ref _selectedMusicFile, value);
        ExecutePlay(SelectedMusicFile);
      }
    }

    public void Init(MusicFile selectedMusicFile, List<MusicFile> musicFiles)
    {
      SelectedMusicFile = selectedMusicFile;
      _musicFiles = musicFiles;
      IsPlayBtnVisibile = false;
      IsPauseBtnVisible = true;
    }

    private Command _togglePlayPauseCommand;
    private List<MusicFile> _musicFiles;
    private Command _playPreviousSongCommand;
    private Command _playNextSongCommand;


    private bool _isPlayBtnVisibile;

    public bool IsPlayBtnVisibile
    {
      get { return _isPlayBtnVisibile; }
      set { SetProperty(ref _isPlayBtnVisibile, value); }
    }

    private bool _isPauseBtnVisible;
    private bool _isAlbumArtVisible;

    public bool IsPauseBtnVisible
    {
      get { return _isPauseBtnVisible; }
      set { SetProperty(ref _isPauseBtnVisible, value); }
    }

    public bool IsAlbumArtVisible
    {
      get { return _isAlbumArtVisible; }
      set { SetProperty(ref _isAlbumArtVisible, value); }
    }

    public Command TogglePlayPauseCommand
    {
      get
      {
        return _togglePlayPauseCommand ?? (_togglePlayPauseCommand = new Command(async () =>
        {
          if (_mediaPlayerService.IsPlaying)
          {
            ExecutePause();
            IsPlayBtnVisibile = true;
            IsPauseBtnVisible = false;
          }
          else
          {
            ExecuteResume();
            IsPlayBtnVisibile = false;
            IsPauseBtnVisible = true;
          }
        }));
      }
    }

    public Command PlayPreviousSongCommand
    {
      get
      {
        return _playPreviousSongCommand ?? (_playPreviousSongCommand = new Command(async () =>
        {
         ExecutePlayPrevious();
        }));
      }
    }

    public Command PlayNextSongCommand
    {
      get
      {
        return _playNextSongCommand ?? (_playNextSongCommand = new Command(async () =>
        {
          ExecutePlayNext();
        }));
      }
    }

    public Assembly SvgAssembly
    {
      get { return typeof (App).GetTypeInfo().Assembly; }
    }

    public string PlayButtonPath
    {
      get { return "Core.Images.PlayButton.svg"; }
    }

    private void ExecutePlay(MusicFile musicFileToPlay)
    {
      IsAlbumArtVisible = musicFileToPlay.AlbumArt != null;
      _mediaPlayerService.Play(musicFileToPlay.FileName, musicFileToPlay.FilePath);
    }

    private void ExecutePlayNext()
    {
      var currentMusicFileIndex = _musicFiles.IndexOf(SelectedMusicFile);
      var nextSongIndex = currentMusicFileIndex + 1;

      if (nextSongIndex > _musicFiles.Count - 1)
      {
        nextSongIndex = 0;
      }

      SelectedMusicFile = _musicFiles[nextSongIndex];
    }

    private void ExecutePlayPrevious()
    {
      var currentMusicFileIndex = _musicFiles.IndexOf(SelectedMusicFile);
      var previousSongIndex = currentMusicFileIndex - 1;

      if (previousSongIndex < 0)
      {
        previousSongIndex = _musicFiles.Count - 1;
      }

      SelectedMusicFile = _musicFiles[previousSongIndex];
    }

    private void ExecuteResume()
    {
      _mediaPlayerService.Resume();
    }

    private void ExecutePause()
    {
      _mediaPlayerService.Pause();
    }
  }
}