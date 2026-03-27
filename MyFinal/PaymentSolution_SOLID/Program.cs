using System;

namespace PaymentSolution
{
    // --- 1. ABSTRACTION (DIP) ---
    // Interface đóng vai trò là khuôn mẫu chung
    public interface IPaymentMethod
    {
        void ProcessPayment(double amount);
    }

    // --- 2. CONCRETE IMPLEMENTATIONS (OCP) ---
    // Mỗi loại thanh toán là một class riêng biệt
    public class CardPayment : IPaymentMethod
    {
        public void ProcessPayment(double amount) =>
            Console.WriteLine($"[Card] Đang thanh toán qua Thẻ: {amount:C}");
    }

    public class PaypalPayment : IPaymentMethod
    {
        public void ProcessPayment(double amount) =>
            Console.WriteLine($"[Paypal] Đang thanh toán qua Paypal: {amount:C}");
    }

    // Dễ dàng thêm Momo mà không sửa code cũ
    public class MomoPayment : IPaymentMethod
    {
        public void ProcessPayment(double amount) =>
            Console.WriteLine($"[Momo] Đang quét mã QR Momo: {amount:C}");
    }

    // --- 3. HIGH-LEVEL MODULE (DIP & OCP) ---
    public class PaymentService
    {
        private readonly IPaymentMethod _paymentMethod;

        // Dependency Injection: Truyền "phương thức" vào thông qua Constructor
        public PaymentService(IPaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public void Execute(double amount)
        {
            _paymentMethod.ProcessPayment(amount);
        }
    }

    // --- 4. CHƯƠNG TRÌNH CHÍNH ---
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Test với Thẻ
            var cardProcessor = new PaymentService(new CardPayment());
            cardProcessor.Execute(150000);

            // Test với Momo (Thêm mới cực dễ dàng)
            var momoProcessor = new PaymentService(new MomoPayment());
            momoProcessor.Execute(50000);

            Console.WriteLine("\nNhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
    }
}