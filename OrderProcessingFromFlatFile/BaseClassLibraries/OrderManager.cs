using OrderProcessingFromFlatFile.Models;
using OrderProcessingFromFlatFile.Repositories;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public class OrderManager : IOrderManager
  {
    private readonly IOrderRepository _orderRepository;

    public OrderManager(IOrderRepository orderRepository)
    {
      _orderRepository = orderRepository;
    }
    public Order ReceiveOrder()
    {
      // Contains the logic to get the oder file from the publishing system and places it at a location
      // where the repository can read it.
      Console.WriteLine("Order file received and stored in OrderData folder!");
      return _orderRepository.GetOrder();

    }
  }
}
