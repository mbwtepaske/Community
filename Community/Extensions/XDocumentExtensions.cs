namespace System.Xml.Linq
{
  using Schema;

  public static class XDocumentExtensions
  {
    public static Boolean Validate(this XDocument document, XmlSchema schema, XmlSeverityType severity)
    {
      schema.ArgumentNull(nameof(schema));

      var isValid = true;

      ValidationEventHandler handler = (sender, arguments) =>
      {
        if (arguments.Severity.HasFlag(severity))
        {
          isValid = false;
        }
      };

      var schemaSet = new XmlSchemaSet();

      schemaSet.Add(schema);
      document.Validate(schemaSet, handler);

      return isValid;
    }

    public static Boolean ValidateForErrors(this XDocument document, XmlSchema schema) => Validate(document, schema, XmlSeverityType.Error);

    public static Boolean ValidateForErrorsAndWarnings(this XDocument document, XmlSchema schema) => Validate(document, schema, XmlSeverityType.Warning);
  }
}
