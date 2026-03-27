using NotificationSystem.Interfaces;

namespace NotificationSystem.Decorators
{
    public abstract class NotifierDecorator : INotifier
    {
        protected readonly INotifier _notifier;

        protected NotifierDecorator(INotifier notifier)
        {
            _notifier = notifier;
        }

        public virtual void Send(string message)
        {
            _notifier.Send(message);
        }
    }
}
