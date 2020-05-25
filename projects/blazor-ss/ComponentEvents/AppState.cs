using System;

namespace ComponentEvents
{
  public class AppState
  {
    public event Action<string> OnNotification;

    public void Notify(string notification)
    {
      OnNotification?.Invoke(notification);
    }
  }
}