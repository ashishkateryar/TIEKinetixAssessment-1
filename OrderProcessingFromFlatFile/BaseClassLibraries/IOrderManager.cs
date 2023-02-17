using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public interface IOrderManager
  {
    Order ReceiveOrder();
  }
}