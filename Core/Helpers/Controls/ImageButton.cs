﻿using Core.Helpers.Codes;
using Xamarin.Forms;

namespace Core.Helpers.Controls
{
  public class ImageButton : Button
  {
    /// <summary>
    ///   The name of the image without path or file type information.
    ///   Android: There should be a drable resource with the same name
    ///   iOS: There should be an image in the Resources folder with a build action of BundleResource.
    ///   Windows Phone: There should be an image in the Images folder with a type of .png and build action set to resource.
    /// </summary>
    public static readonly BindableProperty ImagePathProperty =
      BindableProperty.Create<ImageButton, string>(
        p => p.ImagePath, default(string));

    /// <summary>
    ///   The orientation of the image relative to the text.
    /// </summary>
    public static readonly BindableProperty OrientationProperty =
      BindableProperty.Create<ImageButton, ImageOrientation>(
        p => p.Orientation, ImageOrientation.ImageToLeft);

    /// <summary>
    ///   The requested height of the image.  If less than or equal to zero than a
    ///   height of 50 will be used.
    /// </summary>
    public static readonly BindableProperty ImageHeightRequestProperty =
      BindableProperty.Create<ImageButton, int>(
        p => p.ImageHeightRequest, default(int));

    /// <summary>
    ///   The requested width of the image.  If less than or equal to zero than a
    ///   width of 50 will be used.
    /// </summary>
    public static readonly BindableProperty ImageWidthRequestProperty =
      BindableProperty.Create<ImageButton, int>(
        p => p.ImageWidthRequest, default(int));

    public string ImagePath
    {
      get { return (string) GetValue(ImagePathProperty); }
      set { SetValue(ImagePathProperty, value); }
    }

    public ImageOrientation Orientation
    {
      get { return (ImageOrientation) GetValue(OrientationProperty); }
      set { SetValue(OrientationProperty, value); }
    }

    public int ImageHeightRequest
    {
      get { return (int) GetValue(ImageHeightRequestProperty); }
      set { SetValue(ImageHeightRequestProperty, value); }
    }

    public int ImageWidthRequest
    {
      get { return (int) GetValue(ImageWidthRequestProperty); }
      set { SetValue(ImageWidthRequestProperty, value); }
    }
  }
}