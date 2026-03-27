using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Models
{
    public class PushNotifier : INotifier
    {
        public void Send(string message) =>
            Console.WriteLine($"[Push] Sending: {message}");
    }
}
