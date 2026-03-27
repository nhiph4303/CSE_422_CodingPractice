using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Models
{
    public class EmailNotifier : INotifier
    {
        public void Send(string message) =>
            Console.WriteLine($"[Email] Sending: {message}");
    }
}
