using System;
using System.Runtime.Serialization;

namespace TournamentService.Exceptions
{
  [Serializable]
  public class ImageNotCreatedException : Exception
  {
    public ImageNotCreatedException(string message) : base(message)
    {
    }

    public ImageNotCreatedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ImageNotCreatedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}