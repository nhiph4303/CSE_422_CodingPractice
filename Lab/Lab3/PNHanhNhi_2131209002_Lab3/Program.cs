using System;
using System.Collections.Generic;
using PNHNhi_2131209002_Lab2.Models;

namespace PNHNhi_2131209002_Lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunExercise1();
            RunExercise2();
            RunExercise3();
            RunExercise4();
            RunExercise5();
            RunExercise6();
            RunExercise7();
            RunExercise8();
            RunExercise9();
            RunExercise10();
        }

        private static void RunExercise1()
        {
            Console.WriteLine("---- Exercise 1: Encapsulation ----");
            Book defaultBook = new Book();
            Book cleanCodeBook = new Book("978-0132350884", "Clean Code", "Robert C. Martin", 2008, 5);

            defaultBook.DisplayInfo();
            cleanCodeBook.DisplayInfo();
        }

        private static void RunExercise2()
        {
            Console.WriteLine("\n---- Exercise 2: Inheritance ----");
            Member member = new Member("M001", "Alice", "alice@example.com");
            PremiumMember premiumMember = new PremiumMember("P001", "Bob", "bob@example.com", DateTime.Now.AddMonths(6));

            member.DisplayInfo();
            premiumMember.DisplayInfo();
        }

        private static void RunExercise3()
        {
            Console.WriteLine("\n---- Exercise 3: Abstraction ----");
            Book csharpBook = new Book("111", "C# in Depth", "Jon Skeet", 2019, 1);
            Member alice = new Member("M002", "Alice", "alice@example.com");
            PremiumMember bob = new PremiumMember("P002", "Bob", "bob@example.com", DateTime.Now.AddMonths(3));

            Transaction borrowAlice = new BorrowTransaction(alice, csharpBook);
            Transaction borrowBob = new BorrowTransaction(bob, csharpBook);
            Transaction returnAlice = new ReturnTransaction(alice, csharpBook);

            borrowAlice.Execute(); // Success
            borrowBob.Execute();   // Out of stock
            returnAlice.Execute(); // Success
        }

        private static void RunExercise4()
        {
            Console.WriteLine("\n---- Exercise 4: Polymorphism ----");
            Book pragmaticBook = new Book("222", "The Pragmatic Programmer", "Andrew Hunt", 1999, 2);
            PremiumMember premiumMember = new PremiumMember("P003", "Carol", "carol@example.com", DateTime.Now.AddMonths(12));
            Member regularMember = new Member("M003", "David", "david@example.com");

            List<Transaction> transactions = new List<Transaction>
            {
                new BorrowTransaction(regularMember, pragmaticBook),
                new BorrowTransaction(premiumMember, pragmaticBook),
                new BorrowTransaction(premiumMember, pragmaticBook), // Out of stock
                new ReturnTransaction(regularMember, pragmaticBook),
                new BorrowTransaction(premiumMember, pragmaticBook)   // Borrow again
            };

            foreach (Transaction transaction in transactions)
            {
                transaction.Execute();
            }

            Console.WriteLine("\n---- Final Book Status ----");
            pragmaticBook.DisplayInfo();
            Console.WriteLine("\n---- Member Borrowed Books ----");
            Console.WriteLine($"Regular: {regularMember.BorrowedBooks.Count}");
            Console.WriteLine($"Premium: {premiumMember.BorrowedBooks.Count}");
        }

        private static void RunExercise5()
        {
            Console.WriteLine("\n---- Exercise 5: Interfaces ----");
            Book refactoringBook = new Book("333", "Refactoring", "Martin Fowler", 2018, 2);
            Book designPatternsBook = new Book("444", "Design Patterns", "GoF", 1994, 1);

            Member eva = new Member("M004", "Eva", "eva@example.com");
            PremiumMember frank = new PremiumMember("P004", "Frank", "frank@example.com", DateTime.Now.AddMonths(2));

            eva.BorrowBook(refactoringBook);
            eva.BorrowBook(designPatternsBook);
            frank.BorrowBook(refactoringBook);
            frank.BorrowBook(designPatternsBook); // Out of stock

            eva.ReturnBook(refactoringBook);
            frank.BorrowBook(refactoringBook); // Borrow again

            eva.PrintDetails();
            frank.PrintDetails();
        }

        private static void RunExercise6()
        {
            Console.WriteLine("\n---- Exercise 6: Constructors ----");

            List<Book> initialBooks = new List<Book>
            {
                new Book("111", "C# in Depth", "Jon Skeet", 2019, 3),
                new Book("222", "Clean Code", "Robert C. Martin", 2008, 2)
            };

            // Default Constructor
            Library defaultLib = new Library();
            defaultLib.Books.AddRange(initialBooks);
            defaultLib.Members.Add(new Member("M101", "Alice", "alice@mail.com"));
            defaultLib.TransactionHistory.Add(new BorrowTransaction(defaultLib.Members[0], defaultLib.Books[0]));

            Console.WriteLine("\n-- Library 1 (Default Constructor) --");
            defaultLib.DisplayLibraryInfo();

            // Parameterized Constructor
            Library paramLib = new Library("EIU Library", initialBooks);
            paramLib.Members.Add(new PremiumMember("P202", "Bob", "bob@mail.com", DateTime.Now.AddMonths(12)));

            Console.WriteLine("\n-- Library 2 (Parameterized Constructor) --");
            paramLib.DisplayLibraryInfo();

            // Copy Constructor
            Library copiedLib = new Library(paramLib);
            Console.WriteLine("\n-- Library 3 (Copied from Library 2) --");
            copiedLib.DisplayLibraryInfo();
        }

        private static void RunExercise7()
        {
            Console.WriteLine("\n---- Exercise 7: Overloading & Overriding ----");

            NotificationService notifyService = new NotificationService();
            AdvancedNotificationService advancedNotifyService = new AdvancedNotificationService();

            // Overloading
            notifyService.SendNotification("System maintenance at midnight.");
            notifyService.SendNotification("Book 'Clean Code' is due tomorrow!", "Alice");
            notifyService.SendNotification("Library closed on weekends.", new List<string> { "Alice", "Bob", "Carol" });

            // Overriding
            advancedNotifyService.SendNotification("Membership expires soon!");
        }

        private static void RunExercise8()
        {
            Console.WriteLine("\n---- Exercise 8: LibraryCard ----");

            Member memberHanhNhi = new Member("M501", "Hanh Nhi", "hanhnhi@eiu.edu.vn");
            PremiumMember memberNgocMai = new PremiumMember("P502", "Ngoc Mai", "ngocmai@eiu.edu.vn", DateTime.Now.AddMonths(6));

            LibraryCard card1 = new LibraryCard("CARD001", memberHanhNhi);
            LibraryCard card2 = new LibraryCard("CARD002", memberNgocMai);

            card1.DisplayCardInfo();
            card2.DisplayCardInfo();

            Console.WriteLine("\n-- Deactivating and renewing cards --");
            card1.DeactivateCard();
            card1.DisplayCardInfo();

            card1.RenewCard();
            card1.DisplayCardInfo();
        }

        private static void RunExercise9()
        {
            Console.WriteLine("\n---- Exercise 9: Class vs Record ----");

            BookClass bookClass1 = new BookClass("111", "Clean Code", "Robert C. Martin");
            BookClass bookClass2 = new BookClass("111", "Clean Code", "Robert C. Martin");

            BookRecord bookRecord1 = new BookRecord("111", "Clean Code", "Robert C. Martin");
            BookRecord bookRecord2 = new BookRecord("111", "Clean Code", "Robert C. Martin");

            // Comparison
            Console.WriteLine($"BookClass equality: {bookClass1 == bookClass2}");
            Console.WriteLine($"BookRecord equality: {bookRecord1 == bookRecord2}");

            // ToString
            Console.WriteLine($"Class ToString(): {bookClass1}");
            Console.WriteLine($"Record ToString(): {bookRecord1}");

            // HashCode
            Console.WriteLine($"Class HashCode: {bookClass1.GetHashCode()}");
            Console.WriteLine($"Record HashCode: {bookRecord1.GetHashCode()}");

            // with expression
            BookRecord modifiedRecord = bookRecord1 with { Title = "Clean Coder" };
            Console.WriteLine($"Modified Record: {modifiedRecord}");

            // Mutability
            bookClass1.Title = "Refactoring Clean Code";

            Console.WriteLine("\n--- Final Comparison ---");
            Console.WriteLine($"BookClass (mutable): {bookClass1}");
            Console.WriteLine($"BookRecord (immutable): {bookRecord1}");
        }

        private static void RunExercise10()
        {
            Console.WriteLine("\n---- Exercise 10: Delegates and Events ----");

            Library eventLibrary = new Library();
            Book eventBook = new Book("999", "The Event Driven Architecture", "Event Master", 2024, 5);
            Member eventMember = new Member("M999", "Eve", "eve@event.com");
            NotificationService notificationService = new NotificationService();

            // Subscribe to the event
            eventLibrary.OnBookBorrowed += notificationService.OnBookBorrowed;

            // Multicast: Subscribe another action (e.g., logging)
            eventLibrary.OnBookBorrowed += (b, m) =>
            {
                Console.WriteLine($"[Log system] Recorded transaction: {b.ISBN} borrowed by {m.MemberID}");
            };

            Console.WriteLine("-- Triggering Borrow Event --");
            eventLibrary.ProcessBorrowing(eventBook, eventMember);
        }
    }
}
