using System;

namespace TournamentService.Exceptions
{
  public class ImageNotCreatedException : Exception
  {
    public ImageNotCreatedException(string message) : base(message)
    {
    }

    public ImageNotCreatedException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}