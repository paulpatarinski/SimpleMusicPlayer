using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Core;
using Core.Services;
using Core.Services.Native;
using SimpleMusicPlayer.Android.Services;
using Xamarin.Forms;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;

namespace SimpleMusicPlayer.Android
{
  [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : XFormsApplicationDroid
  {
    /// <summary>
    ///   Called when [create].
    /// </summary>
    /// <param name="bundle">The bundle.</param>
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      if (!Resolver.IsSet)
      {
        SetIoc();
      }
      else
      {
        var app = Resolver.Resolve<IXFormsApp>() as IXFormsApp<XFormsApplicationDroid>;
        app.AppContext = this;
      }

      Forms.Init(this, bundle);

      App.Init();

      LoadApplication(new App());
    }

    /// <summary>
    ///   Sets the IoC.
    /// </summary>
    private void SetIoc()
    {
      var nativeApplication = new XFormsApp<XFormsApplicationDroid>();
      nativeApplication.Init(this);

      var containerBuilder = new ContainerBuilder();

      containerBuilder.Register(c => AndroidDevice.CurrentDevice).As<IDevice>();
      containerBuilder.Register(c => nativeApplication).As<IXFormsApp>();
      containerBuilder.Register(c => new FileService()).As<IFileService>();
      containerBuilder.Register(c => new Id3TagService()).As<IId3TagService>();

      Core.App.RegisterCoreComponents(containerBuilder);

      var autofacContainer = new AutofacContainer(containerBuilder.Build());
      autofacContainer.Register<IDependencyContainer>(autofacContainer);

      Resolver.SetResolver(autofacContainer.GetResolver());
    }
  }
}