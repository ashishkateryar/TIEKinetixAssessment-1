namespace OrderProcessingFromFlatFile.Repositories
{
  public interface IReferencePriceListRepository
  {
    Task<decimal> GetReferencePrice(long eanBuyer, long eanArticle);
  }
}