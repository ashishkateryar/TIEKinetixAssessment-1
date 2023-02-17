using System.Data.SqlTypes;
using System.Text;
using System.Xml.Linq;

namespace OrderProcessingFromFlatFile.ExternalAPIs
{
  public class OrderManagementSystemProxy : IOrderManagementSystemProxy
  {
    public async Task SendOrderAsync(string xmlData)
    {
      //Calls the post API of the Oder Management System with order in XML form.
      XDocument doc = XDocument.Parse(xmlData);
      string formattedXmlString = doc.ToString(SaveOptions.None);
      Console.WriteLine(formattedXmlString);

      // Send the XML via an HTTP call to another endpoint
      using (var client = new HttpClient())
      {
        var content = new StringContent(xmlData, Encoding.UTF8, "application/xml");
        try
        {
          var response = await client.PostAsync("https://example.com/api/orders", content);
          if (response.IsSuccessStatusCode)
          {
            Console.WriteLine("Order successfully sent.");
          }
          else
          {
            Console.WriteLine($"Failed to send order. Status code: {response.StatusCode}");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Failed to send order. Exception: {ex.Message}");
        }
        
      }
    }
  }
}
