using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public interface IXmlConverter
  {
    string Serialize(Order order);
  }
}