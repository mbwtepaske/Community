using System;
using System.Diagnostics;
using System.Linq;

namespace System.Collections.Generic
{
  /// <summary>
  /// Provides a set of static methods for querying objects that implement <see cref="T:System.Collections.Generic.IDictionary<TKey, TValue>" />.
  /// </summary>
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Gets the value associated with the key or if the key does not exists returns the default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      Assert.ThrowIfNull<NullReferenceException>(dictionary, "dictionary");
      
      return dictionary.GetValueOrDefault(key, default(TValue));
    }

    /// <summary>
    /// Gets the value associated with the key or if the key does not exists it returns the specified default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(dictionary, "dictionary");

      var value = default(TValue);

      return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    }

    /// <summary>
    /// Gets the value associated with the key or if the key does not exists it invokes the specified function to return a default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
    {
      Assert.ThrowIfNull<NullReferenceException>(dictionary, "dictionary");
      Assert.ThrowIfNull<ArgumentNullException>(dictionary, "defaultValueFactory");

      var value = default(TValue);

      return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
    }
  }
}
