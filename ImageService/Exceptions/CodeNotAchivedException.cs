using System;
using System.Runtime.Serialization;

namespace ImageService.Exceptions
{
  [Serializable]
  public class CodeNotAchivedException : Exception
  {
    public CodeNotAchivedException(string message) : base(message)
    {
    }

    public CodeNotAchivedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CodeNotAchivedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}