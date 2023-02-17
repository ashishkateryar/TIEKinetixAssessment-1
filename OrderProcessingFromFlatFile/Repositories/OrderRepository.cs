using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly IOrderLineRepository _orderLineRepo;

    public OrderRepository(IOrderLineRepository orderLineRepo)
    {
      _orderLineRepo = orderLineRepo;
    }


    public Order GetOrder()
    {
      using (StreamReader reader = new StreamReader(@"OrderData\order.sdf"))
      {
        string line = reader.ReadLine();

        // Parse the order header
        var orderData = new Order();
        orderData.FileType = line.Substring(0, 3);
        orderData.OrderNumber = int.Parse(line.Substring(3, 20));
        orderData.OrderDate = DateTime.ParseExact(line.Substring(23, 13), "yyyyMMddTHHmm", null);
        orderData.EanBuyer = long.Parse(line.Substring(36, 13));
        orderData.EanSupplier = long.Parse(line.Substring(49, 13));
        orderData.Comment = line.Substring(62, 100).Trim();
        // Process the rest of the lines in the file
        while ((line = reader.ReadLine()) != null)
        {
          orderData.OrderLines.Add(_orderLineRepo.GetOrderLine(line));
        }
        //Console.WriteLine(orderData.OrderLines[0]);
        return orderData;
      }
    }
  }
}
