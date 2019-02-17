using System;

namespace TournamentService.Exceptions
{
  public class BattleNotStartedException : Exception
  {
    public BattleNotStartedException(string message) : base(message)
    {
    }

    public BattleNotStartedException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}