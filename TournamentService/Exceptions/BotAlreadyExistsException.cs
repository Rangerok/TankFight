using System;

namespace TournamentService.Exceptions
{
  public class BotAlreadyExistsException: Exception
  {
    public BotAlreadyExistsException(string message) : base(message)
    {
    }

    public BotAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}