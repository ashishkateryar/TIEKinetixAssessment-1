using OrderProcessingFromFlatFile.ExternalAPIs;
using OrderProcessingFromFlatFile.Models;
using OrderProcessingFromFlatFile.Repositories;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public class ValidateOrder : IValidateOrder
  {
    private IReferencePriceListRepository _referencePriceList;
    private INotificationManager _notification;
    private IErpProxy _erpProxy;

    public ValidateOrder(IReferencePriceListRepository referencePriceList, INotificationManager notification, IErpProxy erpProxy)
    {
      _referencePriceList = referencePriceList;
      _notification = notification;
      _erpProxy = erpProxy;

    }
    public async Task<bool> ValidateAsync(Order order)
    {
      foreach (OrderLine line in order.OrderLines)
      {
        var refPrice = await _referencePriceList.GetReferencePrice(order.EanBuyer, line.EanArticle);

        if (line.UnitPrice != refPrice)
        {
          //Send notification to manager
          string message = String.Format($"Reference Price: {refPrice} is not equal to unit price: {line.UnitPrice}!");
          _notification.SendNotification(message);
          //Set UnitPrice to reference price fetched from DB
          line.UnitPrice = refPrice;

        }
        var availableQty = await _erpProxy.GetArticleQuantityAsync(line.EanArticle);

        if (availableQty < line.Quantity)
        {
          string message = String.Format($"Ordered quantity: {line.Quantity} not available, only {availableQty} left!!");

          _notification.SendNotification(message);

          return false;
        }
      }

      return true;
    }
  }
}
