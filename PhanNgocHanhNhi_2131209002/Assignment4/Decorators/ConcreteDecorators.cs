using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Decorators
{
    public class LoggingDecorator : NotifierDecorator
    {
        public LoggingDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            Console.WriteLine($"[LOG] LOGGING START: Preparing to send message at {DateTime.Now}");
            base.Send(message);
            Console.WriteLine("[LOG] LOGGING END: Message sent successfully");
        }
    }

    public class RetryDecorator : NotifierDecorator
    {
        private readonly int _maxRetries;

        public RetryDecorator(INotifier notifier, int maxRetries = 3) : base(notifier)
        {
            _maxRetries = maxRetries;
        }

        public override void Send(string message)
        {
            int attempts = 0;
            bool success = false;

            while (attempts < _maxRetries && !success)
            {
                try
                {
                    attempts++;
                    base.Send(message);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[RETRY] Attempt {attempts} failed: {ex.Message}");
                    if (attempts >= _maxRetries) throw;
                }
            }
        }
    }
}
