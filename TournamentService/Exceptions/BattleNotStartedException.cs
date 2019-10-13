using System;
using System.Runtime.Serialization;

namespace TournamentService.Exceptions
{
  [Serializable]
  public class BattleNotStartedException : Exception
  {
    public BattleNotStartedException(string message) : base(message)
    {
    }

    public BattleNotStartedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BattleNotStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}