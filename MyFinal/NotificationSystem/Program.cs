using System;
using NotificationSystem.Interfaces;
using NotificationSystem.Factories;
using NotificationSystem.Decorators;

namespace NotificationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết kế để Console hiển thị được tiếng Việt (nếu cần)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("========================================");
            Console.WriteLine("   HỆ THỐNG THÔNG BÁO - DESIGN PATTERNS");
            Console.WriteLine("========================================\n");

            string[] channels = { "email", "sms", "push" };

            foreach (var channel in channels)
            {
                try
                {
                    Console.WriteLine($"--- [{channel.ToUpper()}] ---");

                    // Factory Pattern: tạo đúng loại Notifier theo tên kênh
                    INotifier notifier = NotifierFactory.CreateNotifier(channel);

                    // Decorator Pattern: bọc thêm tính năng Logging
                    INotifier loggedNotifier = new LoggingDecorator(notifier);

                    // Strategy Pattern: gọi Send() — hành vi khác nhau tuỳ loại
                    loggedNotifier.Send($"Chào bạn! Đây là bài thi Design Patterns của Nhi.");

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }

            Console.WriteLine("========================================");
            Console.WriteLine("Nhấn phím bất kỳ để kết thúc...");
            Console.ReadKey();
        }
    }
}
