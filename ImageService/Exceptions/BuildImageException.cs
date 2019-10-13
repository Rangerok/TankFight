using System;
using System.Runtime.Serialization;

namespace ImageService.Exceptions
{
  [Serializable]
  public class BuildImageException : Exception
  {
    public BuildImageException(string message) : base(message)
    {
    }

    public BuildImageException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BuildImageException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}