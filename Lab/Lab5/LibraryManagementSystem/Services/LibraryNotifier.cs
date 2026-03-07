using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Services
{
    public class LibraryNotifier
    {
        private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();

        public void Subscribe(ISubscriber subscriber) => _subscribers.Add(subscriber);

        public void Unsubscribe(ISubscriber subscriber) => _subscribers.Remove(subscriber);

        public void NotifySubscribers(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(message);
            }
        }
    }
}
