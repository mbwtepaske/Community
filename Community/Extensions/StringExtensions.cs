using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
  public static class StringExtensions
  {
    /// <summary>
    /// Returns a String array that contains the substrings in this String that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.
    /// </summary>
    [DebuggerStepThrough]
    public static String[] Split(this String instance, String separator, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
    {
      Assert.ThrowIfNull<NullReferenceException>(instance);
      
      return instance.Split(new[] { separator }, options);
    }

    /// <summary>
    /// Indicates whether the specified string is null or an System.String.Empty string
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean IfNullOrEmpty(this String instance)
    {
      return String.IsNullOrEmpty(instance);
    }

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    [DebuggerStepThrough]
    public static Boolean IsNullOrWhiteSpace(this String instance)
    {
      return String.IsNullOrWhiteSpace(instance);
    }
  }
}
