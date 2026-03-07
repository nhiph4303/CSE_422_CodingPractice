using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Models
{
    public class UserSubscriber : ISubscriber
    {
        public string Name { get; private set; }
        public UserSubscriber(string name) => Name = name;
        public void Update(string message) => Console.WriteLine($"[Notification] To {Name}: {message}");
    }
}
