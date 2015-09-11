namespace System.Collections.Generic
{
  using Diagnostics;

  /// <summary>
  /// Provides a set of static methods for querying objects that implement <see cref="T:System.Collections.Generic.IDictionary" />.
  /// </summary>
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Gets the value associated with the key or if the key does not exists returns the default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      return dictionary.GetValueOrDefault(key, default(TValue));
    }

    /// <summary>
    /// Gets the value associated with the key or if the key does not exists it returns the specified default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
      TValue value;

      return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    }

    /// <summary>
    /// Gets the value associated with the key or if the key does not exists it invokes the specified function to return a default value.
    /// </summary>
    [DebuggerNonUserCode]
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueFactory)
    {
      if (defaultValueFactory == null)
      {
        throw new ArgumentNullException("defaultValueFactory");
      }

      TValue value;

      return dictionary.TryGetValue(key, out value) ? value : defaultValueFactory();
    }
  }
}
