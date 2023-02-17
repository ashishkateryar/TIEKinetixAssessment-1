namespace OrderProcessingFromFlatFile.Models
{
  public class Order
    {
      public string FileType { get; set; } = string.Empty;
      public int OrderNumber { get; set; }
      public DateTime OrderDate { get; set; }
      public long EanBuyer { get; set; }
      public long EanSupplier { get; set; }
      public string Comment { get; set; } = string.Empty;
      public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    }

}
