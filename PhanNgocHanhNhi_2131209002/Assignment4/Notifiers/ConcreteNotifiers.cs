using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Notifiers
{
    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Email] Sending: {message}");
        }
    }

    public class SmsNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"[SMS] Sending: {message}");
        }
    }

    public class PushNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Push] Sending: {message}");
        }
    }

    public class TaskNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Task] Creating system task: {message}");
        }
    }
}
