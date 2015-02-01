using System;

namespace Core.Services
{
  public class ExceptionHandlingService : IExceptionHandlingService
  {
    public void Handle(Exception exception)
    {
      System.Diagnostics.Debug.WriteLine("Unexpected Exception occured {0}", exception);
    }
  }
}
