using System;
using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Factories
{
    public static class NotifierFactory
    {
        public static INotifier CreateNotifier(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailNotifier(),
                "sms"   => new SmsNotifier(),
                "push"  => new PushNotifier(),
                _       => throw new ArgumentException("Loại thông báo không hợp lệ!")
            };
        }
    }
}
