using System;

namespace ImageService.Exceptions
{
  public class CodeNotAchivedException : Exception
  {
    public CodeNotAchivedException(string message) : base(message)
    {
    }

    public CodeNotAchivedException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}