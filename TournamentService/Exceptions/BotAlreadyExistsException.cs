﻿using System;
using System.Runtime.Serialization;

namespace TournamentService.Exceptions
{
  [Serializable]
  public class BotAlreadyExistsException: Exception
  {
    public BotAlreadyExistsException(string message) : base(message)
    {
    }

    public BotAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BotAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}