using System;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using NotificationSystem.Notifiers;

namespace NotificationSystem.Factories
{
    public class NotifierFactory
    {
        public static INotifier CreateNotifier(NotificationType type)
        {
            return type switch
            {
                NotificationType.Email => new EmailNotifier(),
                NotificationType.Sms => new SmsNotifier(),
                NotificationType.Push => new PushNotifier(),
                NotificationType.Task => new TaskNotifier(),
                _ => throw new ArgumentException("Invalid notification type")
            };
        }
    }
}
