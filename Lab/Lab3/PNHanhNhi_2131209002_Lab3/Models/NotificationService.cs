using System;
using System.Collections.Generic;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class NotificationService
    {
        public virtual void SendNotification(string message)
        {
            Console.WriteLine($"[Notification] {message}");
        }

        public void SendNotification(string message, string recipient)
        {
            Console.WriteLine($"[To {recipient}] {message}");
        }

        public void SendNotification(string message, List<string> recipients)
        {
            Console.WriteLine($"[Broadcast to {recipients.Count} recipients] {message}");
        }

        public void OnBookBorrowed(Book book, Member member)
        {
            SendNotification($"User '{member.Name}' successfully borrowed book '{book.Title}' at {DateTime.Now}");
        }
    }

    public class AdvancedNotificationService : NotificationService
    {
        public override void SendNotification(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[{timestamp}] [Notification - HIGH PRIORITY] {message}");
        }
    }
}
