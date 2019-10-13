using System;

namespace ImageService.Exceptions
{
  public class BuildImageException : Exception
  {
    public BuildImageException(string message) : base(message)
    {
    }

    public BuildImageException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}