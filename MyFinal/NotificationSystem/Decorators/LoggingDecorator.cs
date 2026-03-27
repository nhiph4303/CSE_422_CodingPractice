using System;
using NotificationSystem.Interfaces;

namespace NotificationSystem.Decorators
{
    public class LoggingDecorator : NotifierDecorator
    {
        public LoggingDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            // Thêm logic Log trước khi gửi
            Console.WriteLine($"[LOG] {DateTime.Now}: Chuẩn bị gửi tin nhắn...");

            base.Send(message); // Goi hàm gửi của đối tượng gốc

            // Thêm logic Log sau khi gửi
            Console.WriteLine("[LOG]: Đã gửi thành công.");
        }
    }
}
