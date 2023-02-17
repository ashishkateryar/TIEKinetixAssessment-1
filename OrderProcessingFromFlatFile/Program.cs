using OrderProcessingFromFlatFile.BaseClassLibraries;
using OrderProcessingFromFlatFile.Repositories;
using OrderProcessingFromFlatFile.Models;
using OrderProcessingFromFlatFile.ExternalAPIs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
  static async Task Main(string[] args)
  {
    var builder = Host.CreateDefaultBuilder()
                      .ConfigureServices((context, services) =>
                        {
                          services.AddTransient<IOrderLineRepository, OrderLineRepository>();
                          services.AddTransient<IOrderRepository, OrderRepository>();
                          services.AddSingleton<IOrderManager, OrderManager>();
                          services.AddTransient<IReferencePriceListRepository, ReferencePriceListRepository>();
                          services.AddSingleton<INotificationManager, NotificationManager>();
                          services.AddSingleton<IErpProxy, ErpProxy>();
                          services.AddTransient<IValidateOrder, ValidateOrder>();
                          services.AddSingleton<IXmlConverter, XmlConverter>();
                          services.AddSingleton<IOrderManagementSystemProxy, OrderManagementSystemProxy>();                        
                        }).Build();

    var orderManager = builder.Services.GetService<IOrderManager>();
    var validateOrder = builder.Services.GetService<IValidateOrder>();
    var xmlConverter = builder.Services.GetService<IXmlConverter>();
    var orderManagementSystemProxy = builder.Services.GetService<IOrderManagementSystemProxy>();
    var erpProxy = builder.Services.GetService<IErpProxy>();

    //Initiate order receipt and processing, ideally should be triggered by some event.
    Order order = orderManager.ReceiveOrder();
    
    if (order == null) 
    {
      Console.WriteLine("Order processing failed, Aborting application!");
      return; 
    }
    
    bool validOrder = await validateOrder.ValidateAsync(order);

    if (validOrder)
    {
      foreach (OrderLine orderLine in order.OrderLines)
      {
        erpProxy.UpdateArticleQuantity(orderLine.EanArticle, orderLine.Quantity);
      }

      var xmlDoc = xmlConverter.Serialize(order);
      await orderManagementSystemProxy.SendOrderAsync(xmlDoc);
    }
  }
}