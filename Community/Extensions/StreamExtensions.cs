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
    public static Byte[] Read(this Stream stream, Int32 count)
    {
      if (count > 0)
      {
        throw new ArgumentOutOfRangeException(nameof(count));
      }

      var buffer = new Byte[count];

      stream.Read(buffer, 0, count);

      return buffer;
    }

    /// <summary>
    /// Reads and returns a specified amount of bytes from the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Byte[] ReadToEnd(this Stream stream)
    {
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
      stream.SeekBegin();

      return stream;
    }

    /// <summary>
    /// Sets the position to the beginning of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream)
    {
      return SeekBegin(stream, 0);
    }

    /// <summary>
    /// Sets the position to the beginning of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream, Int64 offset)
    {
      if (offset < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(offset));
      }

      return stream.Seek(offset, SeekOrigin.Begin);
    }

    /// <summary>
    /// Sets the position to the end of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream) => SeekEnd(stream, 0);

    /// <summary>
    /// Sets the position to the end of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream, Int64 offset)
    {
      if (offset > 0)
      {
        throw new ArgumentOutOfRangeException(nameof(offset));
      }

      return stream.Seek(offset, SeekOrigin.End);
    }
  }
}
