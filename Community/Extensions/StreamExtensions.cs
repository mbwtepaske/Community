using Community;

namespace System.IO
{
  using Diagnostics;

  /// <summary>
  /// Provides a set of extension-methods for <see cref="T:System.IO.Stream" />.
  /// </summary>
  public static class StreamExtensions
  {
    /// <summary>
    /// Reads and returns a specified amount of bytes from the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Byte[] Read(this Stream stream, Int32 length)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      if (length < 0)
      {
        throw new ArgumentOutOfRangeException(String.Format(Exceptions.ARGUMENT_LESS, "length", "zero"));
      }

      var buffer = new Byte[length];

      stream.Read(buffer, 0, length);

      return buffer;
    }

    /// <summary>
    /// </summary>
    [DebuggerStepThrough]
    public static Byte[] ReadToEnd(this Stream stream)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      var buffer = new Byte[stream.Length - stream.Position];

      stream.Read(buffer, 0, buffer.Length);

      return buffer;
    }


    /// <summary>
    /// Resets the position to the beginning and returns the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static TStream Reset<TStream>(this TStream stream)
      where TStream : Stream
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      stream.SeekBegin();

      return stream;
    }

    /// <summary>
    /// Sets the position to the beginning of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      return SeekBegin(stream, 0);
    }

    /// <summary>
    /// Sets the position to the beginning of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream, Int64 offset)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      if (offset < 0)
      {
        throw new ArgumentOutOfRangeException("offset");
      }

      return stream.Seek(offset, SeekOrigin.Begin);
    }

    /// <summary>
    /// Sets the position to the end of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      return SeekEnd(stream, 0);
    }

    /// <summary>
    /// Sets the position to the end of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream, Int64 offset)
    {
      if (stream == null)
      {
        throw new NullReferenceException("stream");
      }

      if (offset > 0)
      {
        throw new ArgumentOutOfRangeException("offset");
      }

      return stream.Seek(-offset, SeekOrigin.End);
    }
  }
}
