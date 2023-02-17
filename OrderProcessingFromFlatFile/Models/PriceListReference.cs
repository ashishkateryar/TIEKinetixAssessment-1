namespace OrderProcessingFromFlatFile.Models
{
  public class PriceListReference
  {
    public long EanArticle { get; set; }
    public decimal DefaultPrice { get; set; }
    public Dictionary<long, decimal> SpecialPrices { get; set; } = new Dictionary<long, decimal>();
  }
}
