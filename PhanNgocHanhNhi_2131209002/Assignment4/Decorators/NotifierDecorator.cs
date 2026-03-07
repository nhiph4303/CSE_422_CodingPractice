using NotificationSystem.Interfaces;

namespace NotificationSystem.Decorators
{
    public abstract class NotifierDecorator : INotifier
    {
        protected readonly INotifier _wrappedNotifier;

        protected NotifierDecorator(INotifier notifier)
        {
            _wrappedNotifier = notifier;
        }

        public virtual void Send(string message)
        {
            _wrappedNotifier.Send(message);
        }
    }
}
