namespace System.Xml.XPath
{
  public static class XPathNavigatorExtensions
  {
    /// <summary>
    /// Evaluates the specified XPath expression and returns the typed result.
    /// </summary>
    public static TResult Evaluate<TResult>(this XPathNavigator navigator, String xPath)
    {
      if (xPath == null)
      {
        throw new ArgumentNullException("xPath");
      }
      
      return (TResult)navigator.Evaluate(xPath);
    }
  }
}
