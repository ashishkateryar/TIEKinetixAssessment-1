using System.Xml.Serialization;
using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public class XmlConverter : IXmlConverter
  {
    public string Serialize(Order order)
    {
      var serializer = new XmlSerializer(typeof(Order));

      // Serialize the order to XML
      string xml;
      using (var writer = new StringWriter())
      {
        serializer.Serialize(writer, order);
        xml = writer.ToString();
      }
      return xml;
    }
  }
}
