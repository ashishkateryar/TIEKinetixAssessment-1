using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.Repositories
{
  public class OrderLineRepository : IOrderLineRepository
  {
    public OrderLine GetOrderLine(string line)
    {
      //Process the line according to the file format
      var orderLine = new OrderLine();
      orderLine.EanArticle = long.Parse(line.Substring(0, 13));
      orderLine.Description = line.Substring(13, 65).Trim();
      orderLine.Quantity = int.Parse(line.Substring(78, 10));
      orderLine.UnitPrice = decimal.Parse(line.Substring(88, 10));

      return orderLine;
    }
  }
}
