﻿namespace OrderProcessingFromFlatFile.Models
{
  public class OrderLine
  {
    public long EanArticle { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
  }
}
