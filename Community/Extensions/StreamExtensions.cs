﻿namespace System.IO
{
  using Diagnostics;

  /// <summary>
  /// Provides a set of extension-methods for <see cref="T:System.IO.Stream" />.
  /// </summary>
  public static class StreamExtensions
  {
    /// <summary>
    /// Resets the position to the beginning and returns the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static TStream Reset<TStream>(this TStream stream)
      where TStream : Stream
    {
      Assert.ThrowIfNull<NullReferenceException>(stream, "stream");

      stream.SeekBegin();

      return stream;
    }

    /// <summary>
    /// Sets the position to the beginning of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream)
    {
      Assert.ThrowIfNull<NullReferenceException>(stream, "stream");

      return SeekBegin(stream, 0);
    }

    /// <summary>
    /// Sets the position to the beginning of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekBegin(this Stream stream, Int64 offset)
    {
      Assert.ThrowIfNull<NullReferenceException>(stream, "stream");

      return stream.Seek(offset, SeekOrigin.Begin);
    }

    /// <summary>
    /// Sets the position to the end of the stream.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream)
    {
      Assert.ThrowIfNull<NullReferenceException>(stream, "stream");

      return SeekEnd(stream, 0);
    }

    /// <summary>
    /// Sets the position to the end of the stream, with a specific offset.
    /// </summary>
    [DebuggerStepThrough]
    public static Int64 SeekEnd(this Stream stream, Int64 offset)
    {
      Assert.ThrowIfNull<NullReferenceException>(stream, "stream");

      return stream.Seek(offset, SeekOrigin.End);
    }
  }
}
