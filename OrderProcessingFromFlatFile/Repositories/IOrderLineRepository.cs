using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.Repositories
{
  public interface IOrderLineRepository
  {
    OrderLine GetOrderLine(string line);
  }
}