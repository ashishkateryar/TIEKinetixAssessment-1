namespace OrderProcessingFromFlatFile.Repositories
{
  public class ReferencePriceListRepository : IReferencePriceListRepository
  {
    public async Task<decimal> GetReferencePrice(long eanBuyer, long eanArticle)
    {
      //Ideally we would have a DB call here to get the reference price for the given EanArticle for a given EanBuyer if exist
      //As we making a DB call, we need to simulate it with a delay
      await Task.Delay(1000);      
      return 56.00m;
    }
  }
}
