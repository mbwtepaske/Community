namespace System.ComponentModel
{
  using Collections.Concurrent;
  using Globalization;

  /// <summary>
  /// Represents a service class for getting <see cref="System.ComponentModel.TypeConverter"/> and caching them for quick access.
  /// </summary>
  public class TypeConverterService : IServiceProvider
  {
    /// <summary>
    /// Gets the default instance of the type converter services.
    /// </summary>
    public static TypeConverterService Default
    {
      get;
      private set;
    }

    static TypeConverterService()
    {
      Default = new TypeConverterService();
    }

    private ConcurrentDictionary<Type, TypeConverter> Cache
    {
      get;
      set;
    }

    protected TypeConverterService()
    {
      Cache = new ConcurrentDictionary<Type, TypeConverter>();
    }

    /// <summary>
    /// Returns a <see cref="System.ComponentModel.TypeConverter"/> for the specified <see cref="System.Type"/>.
    /// </summary>
    public virtual TypeConverter this[Type type]
    {
      get
      {
        if (type == null)
        {
          throw new ArgumentNullException("type");
        }

        return Cache.GetOrAdd(type, delegate
        {
          return TypeDescriptor.GetConverter(type);
        });
      }
    }

    /// <summary>
    /// Converts the given text to an object, using the specified context and culture information.
    /// </summary>
    public static Object ConvertFrom(Type type, Object value, CultureInfo culture = null)
    {
      if (type == null)
      {
        throw new ArgumentNullException("type");
      }

      if (value == null)
      {
        throw new ArgumentNullException("value");
      }

      return Default[type].ConvertFrom(null, culture, value);
    }

    /// <summary>
    /// Converts the given text to an object, using the specified context and culture information.
    /// </summary>
    public static TResult ConvertFrom<TResult>(Object value, CultureInfo culture = null)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value");
      }

      return (TResult)ConvertFrom(typeof(TResult), value, culture);
    }

    /// <summary>
    /// Converts the given text to an object, using the specified context and culture information.
    /// </summary>
    public static Object ConvertFromString(Type type, String value, CultureInfo culture = null)
    {
      return Default[type].ConvertFromString(null, culture, value);
    }

    /// <summary>
    /// Converts the given text to an object, using the specified context and culture information.
    /// </summary>
    public static TResult ConvertFromString<TResult>(String value, CultureInfo culture = null)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value");
      }

      return (TResult)ConvertFromString(typeof(TResult), value, culture);
    }

    /// <summary>
    /// Converts the given value to a string representation, using the specified context and culture information.
    /// </summary>
    public static String ConvertToString(Object value, CultureInfo culture = null)
    {
      if (value == null)
      {
        throw new ArgumentNullException("value");
      }

      return Default[value.GetType()].ConvertToString(null, culture, value);
    }

    /// <summary>
    /// Returns the default instance of the current <see cref="System.AppDomain"/>.
    /// </summary>
    public Object GetService(Type serviceType)
    {
      if (serviceType == null)
      {
        throw new ArgumentNullException("serviceType");
      }

      return Default[serviceType];
    }
  }
}
