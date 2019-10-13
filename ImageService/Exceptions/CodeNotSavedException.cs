using System;
using System.Runtime.Serialization;

namespace ImageService.Exceptions
{
  [Serializable]
  public class CodeNotSavedException : Exception
  {
    public CodeNotSavedException(string message) : base(message)
    {
    }

    public CodeNotSavedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CodeNotSavedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}