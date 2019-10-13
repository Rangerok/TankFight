using System;
using System.Runtime.Serialization;

namespace TournamentService.Exceptions
{
  [Serializable]
  public class BotsCountExceededException : Exception
  {
    public BotsCountExceededException(string message) : base(message)
    {
    }

    public BotsCountExceededException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BotsCountExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}