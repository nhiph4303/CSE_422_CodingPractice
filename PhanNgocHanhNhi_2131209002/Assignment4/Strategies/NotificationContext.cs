using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Strategies
{
    public class NotificationContext
    {
        private INotifier _notifier;

        public NotificationContext(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void SetStrategy(INotifier notifier)
        {
            _notifier = notifier;
            Console.WriteLine($"[Strategy] Switched to {notifier.GetType().Name}");
        }

        public void SendNotification(string message)
        {
            if (_notifier == null)
            {
                throw new InvalidOperationException("Strategy not set");
            }
            _notifier.Send(message);
        }
    }
}
