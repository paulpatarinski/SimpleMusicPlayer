/*
 * Source from https://github.com/XForms/Xamarin-Forms-Labs 
 */

using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Views;
using Core.Helpers.Codes;
using Core.Helpers.Controls;
using SimpleMusicPlayer.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof (ImageButton), typeof (ImageButtonRenderer))]

namespace SimpleMusicPlayer.Android.Renderers
{
  /// <summary>
  ///   Draws a button on the Android platform with the image shown in the right
  ///   position with the right size.
  /// </summary>
  public class ImageButtonRenderer : ButtonRenderer
  {
    private ImageButton ImageButton
    {
      get { return (ImageButton) Element; }
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
    {
      base.OnElementChanged(e);

      if (e.OldElement == null)
      {
        var targetButton = Control;

        if (Element != null && !string.IsNullOrEmpty(ImageButton.ImagePath))
        {
          SetImageSource(targetButton, ImageButton);
        }
      }
    }

    /// <summary>
    ///   Sets the image source.
    /// </summary>
    /// <param name="targetButton">The target button.</param>
    /// <param name="model">The model.</param>
    private void SetImageSource(global::Android.Widget.Button targetButton, ImageButton model)
    {
      var packageName = Context.PackageName;
      const int padding = 10;
      const string resourceType = "drawable";

      var resId = Resources.GetIdentifier(model.ImagePath, resourceType, packageName);
      if (resId > 0)
      {
        var scaledDrawable = GetScaleDrawableFromResourceId(resId, GetWidth(model.ImageWidthRequest),
          GetHeight(model.ImageHeightRequest));

        Drawable left = null;
        Drawable right = null;
        Drawable top = null;
        Drawable bottom = null;
        targetButton.CompoundDrawablePadding = padding;
        switch (model.Orientation)
        {
          case (ImageOrientation.ImageToLeft):
            targetButton.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
            left = scaledDrawable;
            break;
          case (ImageOrientation.ImageToRight):
            targetButton.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            right = scaledDrawable;
            break;
          case (ImageOrientation.ImageOnTop):
            top = scaledDrawable;
            break;
          case (ImageOrientation.ImageOnBottom):
            bottom = scaledDrawable;
            break;
        }

        targetButton.SetCompoundDrawables(left, top, right, bottom);
      }
    }

    /// <summary>
    ///   Called when the underlying model's properties are changed
    /// </summary>
    /// <param name="sender">Model</param>
    /// <param name="e">Event arguments</param>
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);

      if (e.PropertyName == ImageButton.ImagePathProperty.PropertyName)
      {
        var targetButton = Control;
        SetImageSource(targetButton, ImageButton);
      }
    }

    /// <summary>
    ///   Returns a <see cref="Drawable" /> with the correct dimensions from an
    ///   Android resource id.
    /// </summary>
    /// <param name="resId">The android resource id to load the drawable from.</param>
    /// <param name="width">The width to scale to.</param>
    /// <param name="height">The height to scale to.</param>
    /// <returns>A scaled <see cref="Drawable" />.</returns>
    private Drawable GetScaleDrawableFromResourceId(int resId, int width, int height)
    {
      var drawable = Resources.GetDrawable(resId);

      var returnValue = new ScaleDrawable(drawable, 0, width, height).Drawable;
      returnValue.SetBounds(0, 0, width, height);
      return returnValue;
    }

    /// <summary>
    ///   Gets the width based on the requested width, if request less than 0, returns 50.
    /// </summary>
    /// <param name="requestedWidth">The requested width.</param>
    /// <returns>The width to use.</returns>
    private int GetWidth(int requestedWidth)
    {
      const int defaultWidth = 50;
      return requestedWidth <= 0 ? defaultWidth : requestedWidth;
    }

    /// <summary>
    ///   Gets the height based on the requested height, if request less than 0, returns 50.
    /// </summary>
    /// <param name="requestedHeight">The requested height.</param>
    /// <returns>The height to use.</returns>
    private int GetHeight(int requestedHeight)
    {
      const int defaultHeight = 50;
      return requestedHeight <= 0 ? defaultHeight : requestedHeight;
    }
  }
}