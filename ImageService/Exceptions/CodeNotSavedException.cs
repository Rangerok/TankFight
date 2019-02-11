using System;

namespace ImageService.Exceptions
{
  public class CodeNotSavedException : Exception
  {
    public CodeNotSavedException(string message) : base(message)
    {
    }

    public CodeNotSavedException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}