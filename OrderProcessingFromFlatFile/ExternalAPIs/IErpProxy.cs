namespace OrderProcessingFromFlatFile.ExternalAPIs
{
  public interface IErpProxy
  {
    Task<int> GetArticleQuantityAsync(long articleEAN);
    void UpdateArticleQuantity(long articleEan, int quantityUsed);
  }
}