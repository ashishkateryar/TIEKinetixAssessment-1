namespace OrderProcessingFromFlatFile.BaseClassLibraries
{
  public class NotificationManager : INotificationManager
  {
    public void SendNotification(string message)
    {
      Console.WriteLine(message);
    }
  }
}
