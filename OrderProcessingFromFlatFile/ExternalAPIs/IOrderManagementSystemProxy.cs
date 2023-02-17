namespace OrderProcessingFromFlatFile.ExternalAPIs
{
  public interface IOrderManagementSystemProxy
  {
    Task SendOrderAsync(string xmlData);
  }
}