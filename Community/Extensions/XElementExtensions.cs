using System;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace System.Xml.Linq
{
  /// <summary>
  /// Provides a set of extension-methods for <see cref="T:System.Xml.Linq.XElement" />.
  /// </summary>
  public static class XElementExtensions
  {
    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the default value.
    /// </summary>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static String AttributeValue(this XElement element, String attributeName, String defaultValue = null)
    {
      return AttributeValue<String>(element, attributeName, defaultValue);
    }

    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue AttributeValue<TValue>(this XElement element, String attributeName)
    {
      return AttributeValue(element, attributeName, default(TValue));
    }

    /// <summary>
    /// Returns the value of an xml-attribute if it exists or else returns the specified default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue AttributeValue<TValue>(this XElement element, String attributeName, TValue defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");

      var attribute = element.Attribute(attributeName);

      if (attribute != null)
      {
        return TypeConverterService.ConvertFromString<TValue>(attribute.Value);
      }

      return defaultValue;
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the specified default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="elementName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static String ElementValue(this XElement element, String elementName, String defaultValue = null)
    {
      return ElementValue<String>(element, elementName, defaultValue);
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue ElementValue<TValue>(this XElement element, String elementPath)
    {
      return ElementValue(element, elementPath, default(TValue));
    }

    /// <summary>
    /// Returns the value of an xml-element if it exists or else returns the specified default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to return.</typeparam>
    /// <param name="elementPath">The name of the attribute.</param>
    /// <param name="defaultValue">The fallback value if the attribute doesn't exists.</param>
    public static TValue ElementValue<TValue>(this XElement element, String elementPath, TValue defaultValue)
    {
      Assert.ThrowIfNull<NullReferenceException>(element, "element");

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
