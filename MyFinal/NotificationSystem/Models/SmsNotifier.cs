using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Models
{
    public class SmsNotifier : INotifier
    {
        public void Send(string message) =>
            Console.WriteLine($"[SMS] Sending: {message}");
    }
}
