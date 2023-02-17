using OrderProcessingFromFlatFile.Models;

namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public interface IValidateOrder
  {
    Task<bool> ValidateAsync(Order order);
  }
}