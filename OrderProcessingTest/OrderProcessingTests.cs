using OrderProcessingFromFlatFile.BaseClassLibraries;
using OrderProcessingFromFlatFile.Models;
using Moq;
using OrderProcessingFromFlatFile.ExternalAPIs;
using OrderProcessingFromFlatFile.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OrderProcessingTest
{
  public class OderProcessingTests
  {
    private readonly IValidateOrder _validateOrder;
    private readonly IXmlConverter _xmlConverter;
    private Order sampleOder1;
    private Order sampleOder2;


    public OderProcessingTests()
    {
      // Create an instance of the DI container from the base project
      var builder = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                          services.AddSingleton<IOrderLineRepository, OrderLineRepository>();
                          services.AddSingleton<IOrderRepository, OrderRepository>();
                          services.AddSingleton<IOrderManager, OrderManager>();
                          services.AddSingleton<IReferencePriceListRepository, ReferencePriceListRepository>();
                          services.AddSingleton<INotificationManager, NotificationManager>();
                          services.AddSingleton<IErpProxy, ErpProxy>();
                          services.AddSingleton<IValidateOrder, ValidateOrder>();
                          services.AddSingleton<IXmlConverter, XmlConverter>();
                          services.AddSingleton<IOrderManagementSystemProxy, OrderManagementSystemProxy>();
                        }).Build();

      _validateOrder = builder.Services.GetService<IValidateOrder>();
      _xmlConverter = builder.Services.GetService<IXmlConverter>();

      sampleOder1 = new Order()
      {
        FileType = "ORD",
        OrderDate = DateTime.Now,
        OrderNumber = 12346,
        EanBuyer = 1234567890123,
        EanSupplier = 1234567890123,
        Comment = "Test",
        OrderLines = new List<OrderLine>()
        {
          new OrderLine()
          {
            EanArticle = 8712345678906,
            Description = "Test-Article-1",
            Quantity = 10,
            UnitPrice = 100.00m
          },
          new OrderLine()
          {
            EanArticle = 8712345678907,
            Description = "Test-Article-2",
            Quantity = 20,
            UnitPrice = 200.00m
          }
        }
      };
      
      sampleOder2 = new Order()
      {
        FileType = "ORD",
        OrderDate = DateTime.Now,
        OrderNumber = 12346,
        EanBuyer = 1234567890123,
        EanSupplier = 1234567890123,
        Comment = "Test",
        OrderLines = new List<OrderLine>()
        {
          new OrderLine()
          {
            EanArticle = 8712345678906,
            Description = "Test-Article-1",
            Quantity = 1000,
            UnitPrice = 100.00m
          },
          new OrderLine()
          {
            EanArticle = 8712345678907,
            Description = "Test-Article-2",
            Quantity = 20,
            UnitPrice = 200.00m
          }
        }
      };
    }

    [Fact]
    public async Task TestValidateAsync_ValidOrder_ReturnsTrue()
    {
      // Act
      var result = await _validateOrder.ValidateAsync(sampleOder1);

      // Assert
      Assert.True(result);
    }

    [Fact]
    public async Task TestValidateAsync_InvalidOrder_ReturnsFalse()
    {
      // Act
      var result = await _validateOrder.ValidateAsync(sampleOder2);

      // Assert
      Assert.False(result);
    }

    
    [Fact]
    public void TestXmlConverter_ValidOrder()
    {
      string xmlData = _xmlConverter.Serialize(sampleOder1);
      Assert.NotNull(xmlData);
    }
  }
  
}