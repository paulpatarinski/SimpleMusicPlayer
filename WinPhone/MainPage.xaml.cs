using System.Threading;
using Microsoft.Phone.Controls;
using Xamarin.Forms;

namespace WinPhone
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      // Sample code to localize the ApplicationBar
      Forms.Init();

      Core.App.Init();
      Thread.Sleep(2000);
      Content = Core.App.GetMainPage().ConvertPageToUIElement(this);
      //BuildLocalizedApplicationBar();
    }
  }
}