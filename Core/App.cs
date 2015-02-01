using System.Diagnostics;
using System.Reflection;
using Autofac;
using Core.Pages;
using Core.Services;
using Core.ViewModels;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;

namespace Core
{
  public class App : Application
  {
    public App()
    {
      Init();
      MainPage = GetMainPage();
    }

    public static Page GetMainPage()
    {
      ViewFactory.Register<MainPage, MainPageViewModel>();
      ViewFactory.Register<MusicPlayerPage, MusicPlayerViewModel>();

      var landingPage = (Page) ViewFactory.CreatePage<MainPageViewModel, MainPage>((model, page) =>
      {
        model.LoadSongs();
      });

      var mainNavigationPage = new NavigationPage(landingPage);

      return mainNavigationPage;
    }

    public static void RegisterCoreComponents(ContainerBuilder containerBuilder)
    {
      var assemblies = new[]
      {
        typeof (App).GetTypeInfo().Assembly
      };

      containerBuilder.RegisterAssemblyTypes(assemblies)
        .Where(clss => clss.Name.EndsWith("Service")).AsImplementedInterfaces()
        .Except<ExceptionHandlingService>(ehs => ehs.As<IExceptionHandlingService>().SingleInstance());
    }

    /// <summary>
    ///   Initializes the application.
    /// </summary>
    public static void Init()
    {
      var app = Resolver.Resolve<IXFormsApp>();

      if (app == null)
      {
        return;
      }

      app.Closing += (o, e) => Debug.WriteLine("Application Closing");
      app.Error += (o, e) => Debug.WriteLine("Application Error");
      app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
      app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
      app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
      app.Startup += (o, e) => Debug.WriteLine("Application Startup");
      app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
    }
  }
}