using System;
using NotificationSystem.Factories;
using NotificationSystem.Models;
using NotificationSystem.Decorators;
using NotificationSystem.Strategies;
using NotificationSystem.Interfaces;

namespace NotificationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Notification System Demo ===");
            Console.WriteLine();

            // using factory
            INotifier emailNotifier = NotifierFactory.CreateNotifier(NotificationType.Email);
            
            // applying decorator
            INotifier decoratedEmail = new LoggingDecorator(emailNotifier);

            // strategy context
            NotificationContext notificationContext = new NotificationContext(decoratedEmail);
            
            Console.WriteLine("--- Strategy 1: Email with Logging ---");
            notificationContext.SendNotification("Hello via Email!");
            Console.WriteLine();

            // switch strategy
            Console.WriteLine("--- Strategy 2: SMS with Logging & Retry ---");
            INotifier smsNotifier = NotifierFactory.CreateNotifier(NotificationType.Sms);
            
            // multiple decorators
            INotifier decoratedSms = new LoggingDecorator(new RetryDecorator(smsNotifier, 2));
            
            notificationContext.SetStrategy(decoratedSms);
            notificationContext.SendNotification("Hello via SMS!");
            Console.WriteLine();

            // push noti
            Console.WriteLine("--- Strategy 3: Push Notification (Clean) ---");
            INotifier pushNotifier = NotifierFactory.CreateNotifier(NotificationType.Push);
            notificationContext.SetStrategy(pushNotifier);
            notificationContext.SendNotification("Hello via Push!");
            Console.WriteLine();

            // task noti
            Console.WriteLine("--- Strategy 4: Task Notification with Logging ---");
            INotifier taskNotifier = NotifierFactory.CreateNotifier(NotificationType.Task);
            notificationContext.SetStrategy(new LoggingDecorator(taskNotifier));
            notificationContext.SendNotification("Assigning new review task");
            
            Console.WriteLine();
            Console.WriteLine("=== Demo Completed ===");
        }
    }
}
