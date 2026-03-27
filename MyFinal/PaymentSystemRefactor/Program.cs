using System;

// 1. Định nghĩa Interface
public interface IPaymentProcessor
{
    void Process(double amount);
}

// 2. Các lớp thanh toán cụ thể
public class CardPayment : IPaymentProcessor
{
    public void Process(double amount) => Console.WriteLine($"Pay with card: {amount}");
}

public class PaypalPayment : IPaymentProcessor
{
    public void Process(double amount) => Console.WriteLine($"Pay with paypal: {amount}");
}

// 3. Dịch vụ thông báo
public class NotificationService
{
    private const double VIP_THRESHOLD = 500.0;
    public void SendConfirmation(double amount)
    {
        if (amount > VIP_THRESHOLD)
            Console.WriteLine("Send VIP email");
        else
            Console.WriteLine("Send normal email");
    }
}

// 4. Lớp điều phối (Manager)
public class PaymentManager
{
    private readonly NotificationService _notificationService = new NotificationService();

    public void ExecutePayment(IPaymentProcessor paymentProcessor, double amount)
    {
        paymentProcessor.Process(amount);
        _notificationService.SendConfirmation(amount);
    }
}

// 5. CHƯƠNG TRÌNH CHÍNH ĐỂ CHẠY (Hàm Main)
class Program
{
    static void Main(string[] args)
    {
        PaymentManager manager = new PaymentManager();

        Console.WriteLine("--- Testing Card Payment ---");
        manager.ExecutePayment(new CardPayment(), 600.0); // Sẽ gửi email VIP

        Console.WriteLine("\n--- Testing PayPal Payment ---");
        manager.ExecutePayment(new PaypalPayment(), 100.0); // Sẽ gửi email thường

        // Đợi người dùng nhấn phím mới đóng cửa sổ
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}