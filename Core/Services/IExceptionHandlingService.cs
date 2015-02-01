using System;

namespace Core.Services
{
  public interface IExceptionHandlingService
  {
    void Handle(Exception exception);
  }
}