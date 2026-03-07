using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Factories;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Strategies;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Library Management System (Design Patterns) ===\n");

            // 1. Singleton: Database Connection
            var db = DatabaseConnection.GetInstance();
            db.ExecuteQuery("SELECT * FROM Documents");

            // 2. Factory Method: Document Creation
            IDocument book = DocumentFactory.CreateDocument("book", "Design Patterns: Elements of Reusable Object-Oriented Software");
            IDocument magazine = DocumentFactory.CreateDocument("magazine", "IEEE Software");
            IDocument newspaper = DocumentFactory.CreateDocument("newspaper", "The New York Times");

            book.DisplayInfo();
            magazine.DisplayInfo();
            newspaper.DisplayInfo();

            // 3. Observer: Notification System
            var notifier = new LibraryNotifier();
            var alice = new UserSubscriber("Alice");
            var bob = new UserSubscriber("Bob");

            notifier.Subscribe(alice);
            notifier.Subscribe(bob);

            Console.WriteLine("\n[Action] New book arrived...");
            notifier.NotifySubscribers($"New document added: {book.Title}");

            Console.WriteLine("\n[Action] Alice borrows a magazine...");
            notifier.NotifySubscribers($"Magazine '{magazine.Title}' has been borrowed by Alice.");

            Console.WriteLine("\n[Action] Bob returns a newspaper...");
            notifier.NotifySubscribers($"Newspaper '{newspaper.Title}' has been returned by Bob.");

            // 4. Strategy: Loan Fee Calculation
            Console.WriteLine("\n--- Loan Fee Calculation (5 days) ---");
            var loan = new Loan(new BookFeeStrategy());
            Console.WriteLine($"Book: '{book.Title}' | Fee: {loan.GetFee(5):N0} VNĐ");

            loan.SetStrategy(new MagazineFeeStrategy());
            Console.WriteLine($"Magazine: '{magazine.Title}' | Fee: {loan.GetFee(5):N0} VNĐ");

            loan.SetStrategy(new NewspaperFeeStrategy());
            Console.WriteLine($"Newspaper: '{newspaper.Title}' | Fee: {loan.GetFee(5):N0} VNĐ");

            Console.WriteLine("\n=== End of Program ===");
        }
    }
}
