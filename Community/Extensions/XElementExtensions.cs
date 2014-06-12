namespace System.Xml.Linq
{
  using ComponentModel;

  /// <summary>
  /// Provides a set of extension-methods for <see cref="T:System.Xml.Linq.XElement" />.
  /// </summary>
  public static class XElementExtensions
  {
    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the default value.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static String AttributeValue(this XElement element, String attributeName, String defaultValue = null)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(attributeName, "attributeName");

      return AttributeValue<String>(element, attributeName, defaultValue);
    }

    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="element"></param>
    /// <param name="attributeName">The name of the attribute.</param>
    public static TValue AttributeValue<TValue>(this XElement element, String attributeName)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(attributeName, "attributeName");

      return AttributeValue(element, attributeName, default(TValue));
    }

    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the specified default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="element"></param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue AttributeValue<TValue>(this XElement element, String attributeName, TValue defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(attributeName, "attributeName");

      var attribute = element.Attribute(attributeName);

      return attribute != null
        ? TypeConverterService.ConvertFromString<TValue>(attribute.Value)
        : defaultValue;
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the specified default value.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="elementName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static String ElementValue(this XElement element, String elementName, String defaultValue = null)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(elementName, "elementName");

      return ElementValue<String>(element, elementName, defaultValue);
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="element"></param>
    /// <param name="elementPath"></param>
    public static TValue ElementValue<TValue>(this XElement element, String elementPath)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(elementPath, "elementPath");

      return ElementValue(element, elementPath, default(TValue));
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the specified default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="element"></param>
    /// <param name="elementPath">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue ElementValue<TValue>(this XElement element, String elementPath, TValue defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");
      Assert.ThrowIfNull(elementPath, "elementPath");

      var childNames = elementPath.Split("/");
      var childElement = element;

      for (var index = 0; index < childNames.Length && childElement != null; index++)
      {
        childElement = childElement.Element(childNames[index]);
      }

      if (childElement != null)
      {
        return TypeConverterService.ConvertFromString<TValue>(childElement.Value);
      }

      return defaultValue;
    }
  }
}
