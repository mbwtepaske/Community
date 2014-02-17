namespace System.Xml.XPath
{
  public static class XPathNavigatorExtensions
  {
    /// <summary>
    /// Evaluates the specified XPath expression and returns the typed result.
    /// </summary>
    public static TResult Evaluate<TResult>(this XPathNavigator navigator, String xPath)
    {
      Assert.ThrowIfNull<NullReferenceException>(navigator, "navigator");
      Assert.ThrowIfNull<ArgumentNullException>(xPath, "xPath");
      
      return (TResult)navigator.Evaluate(xPath);
    }
  }
}
