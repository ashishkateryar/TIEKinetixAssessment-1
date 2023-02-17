namespace OrderProcessingFromFlatFile.ExternalAPIs
{
  public class ErpProxy : IErpProxy
  {
    public async Task<int> GetArticleQuantityAsync(long articleEAN)
    {
      //As we are calling a different sub-system (ERP), we need to simulate it with a delay
      await Task.Delay(1000);
      return 700;
    }


    public void UpdateArticleQuantity(long articleEan, int quantityUsed)
    {
      Console.WriteLine("Quantity of Article : {0}, updated!", articleEan);
    }
  }
}
