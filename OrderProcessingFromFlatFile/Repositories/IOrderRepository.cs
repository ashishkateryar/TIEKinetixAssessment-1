using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.Repositories
{
  public interface IOrderRepository
  {
    Order GetOrder();
  }
}