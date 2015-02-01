/*
 * Source from https://github.com/XForms/Xamarin-Forms-Labs 
 */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Core.Helpers.Codes;
using Core.Helpers.Controls;
using WinPhone.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Button = Xamarin.Forms.Button;
using Image = System.Windows.Controls.Image;
using TextAlignment = System.Windows.TextAlignment;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof (ImageButton), typeof (ImageButtonRenderer))]

namespace WinPhone.Renderers
{
  /// <summary>
  ///   Draws a button on the Windows Phone platform with the image shown in the right
  ///   position with the right size.
  /// </summary>
  public class ImageButtonRenderer : ButtonRenderer
  {
    private Image currentImage;
    private ImageButton sourceButton;

    /// <summary>
    ///   Handles the initial drawing of the button.
    /// </summary>
    /// <param name="e">Information on the <see cref="Xamarin.Forms.Labs.WP.Controls.ImageButton" />.</param>
    protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
    {
      base.OnElementChanged(e);

      sourceButton = Element as ImageButton;
      var targetButton = Control;
      if (sourceButton != null && targetButton != null && !string.IsNullOrEmpty(sourceButton.ImagePath))
      {
        var stackPanel = new StackPanel
        {
          Orientation = (sourceButton.Orientation == ImageOrientation.ImageOnTop ||
                         sourceButton.Orientation == ImageOrientation.ImageOnBottom)
            ? Orientation.Vertical
            : Orientation.Horizontal
        };

        currentImage = GetImage(sourceButton.ImagePath, sourceButton.ImageHeightRequest,
          sourceButton.ImageWidthRequest);
        SetImageMargin(currentImage, sourceButton.Orientation);

        var label = new TextBlock
        {
          TextAlignment = GetTextAlignment(sourceButton.Orientation),
          FontSize = 16,
          VerticalAlignment = VerticalAlignment.Center,
          Text = sourceButton.Text
        };

        if (sourceButton.Orientation == ImageOrientation.ImageToLeft)
        {
          targetButton.HorizontalContentAlignment = HorizontalAlignment.Left;
        }
        else if (sourceButton.Orientation == ImageOrientation.ImageToRight)
        {
          targetButton.HorizontalContentAlignment = HorizontalAlignment.Right;
        }

        if (sourceButton.Orientation == ImageOrientation.ImageOnTop ||
            sourceButton.Orientation == ImageOrientation.ImageToLeft)
        {
          currentImage.HorizontalAlignment = HorizontalAlignment.Left;
          stackPanel.Children.Add(currentImage);
          stackPanel.Children.Add(label);
        }
        else
        {
          stackPanel.Children.Add(label);
          stackPanel.Children.Add(currentImage);
        }

        targetButton.Content = stackPanel;
      }
    }

    /// <summary>
    ///   Called when the underlying model's properties are changed
    /// </summary>
    /// <param name="sender">Model sending the change event</param>
    /// <param name="e">Event arguments</param>
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);

      if (e.PropertyName == ImageButton.ImagePathProperty.PropertyName)
      {
        var sourceButton = Element as ImageButton;
        if (sourceButton != null && !string.IsNullOrEmpty(sourceButton.ImagePath))
        {
          currentImage = GetImage(sourceButton.ImagePath, sourceButton.ImageHeightRequest,
            sourceButton.ImageWidthRequest);
          SetImageMargin(currentImage, sourceButton.Orientation);
        }
      }
    }

    /// <summary>
    ///   Returns the alignment of the label on the button depending on the orientation.
    /// </summary>
    /// <param name="imageOrientation">The orientation to use.</param>
    /// <returns>The alignment to use for the text.</returns>
    private static TextAlignment GetTextAlignment(ImageOrientation imageOrientation)
    {
      TextAlignment returnValue;
      switch (imageOrientation)
      {
        case ImageOrientation.ImageToLeft:
          returnValue = TextAlignment.Left;
          break;
        case ImageOrientation.ImageToRight:
          returnValue = TextAlignment.Right;
          break;
        default:
          returnValue = TextAlignment.Center;
          break;
      }

      return returnValue;
    }

    /// <summary>
    ///   Returns a <see cref="Image" /> of type .png that is a resource in the Windows Phone project
    ///   and stored in the images folder.
    /// </summary>
    /// <param name="imageName">The name of the image to return.  Should be the resource name without the .png extension.</param>
    /// <param name="height">The height for the image (divides by 2 for the Windows Phone platform).</param>
    /// <param name="width">The width for the image (divides by 2 for the Windows Phone platform).</param>
    /// <returns>An image </returns>
    private static Image GetImage(string imageName, int height, int width)
    {
      var image = new Image();
      var uri = new Uri("images/" + imageName + ".png", UriKind.Relative);
      var bmp = new BitmapImage(uri);
      image.Source = bmp;
      image.Height = height/2;
      image.Width = width/2;
      return image;
    }

    /// <summary>
    ///   Sets a margin of 10 between the image and the text.
    /// </summary>
    /// <param name="image">The image to add a margin to.</param>
    /// <param name="orientation">The orientation of the image on the button.</param>
    private void SetImageMargin(Image image, ImageOrientation orientation)
    {
      const int defaultMargin = 10;
      var left = 0;
      var top = 0;
      var right = 0;
      var bottom = 0;

      //If the button has text add a margin
      if (!string.IsNullOrEmpty(sourceButton.Text))
      {
        switch (orientation)
        {
          case ImageOrientation.ImageToLeft:
            right = defaultMargin;
            break;
          case ImageOrientation.ImageOnTop:
            bottom = defaultMargin;
            break;
          case ImageOrientation.ImageToRight:
            left = defaultMargin;
            break;
          case ImageOrientation.ImageOnBottom:
            top = defaultMargin;
            break;
        }
      }

      image.Margin = new Thickness(left, top, right, bottom);
    }
  }
}