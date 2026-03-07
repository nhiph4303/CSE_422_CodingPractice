using System;
using System.IO;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDir = Directory.GetCurrentDirectory();
            string dataDir = Path.Combine(baseDir, "Data");
            
            if (!Directory.Exists(dataDir))
            {
                var current = new DirectoryInfo(baseDir);
                for (int i = 0; i < 4; i++)
                {
                    if (current == null) break;
                    var check = Path.Combine(current.FullName, "Data");
                    if (Directory.Exists(check))
                    {
                        dataDir = check;
                        break;
                    }
                    current = current.Parent;
                }
            }

            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            string booksPath = Path.Combine(dataDir, "books.csv");
            string readersPath = Path.Combine(dataDir, "readers.csv");

            // Initialize Components (Dependency Injection manually)
            IBookRepository bookRepo = new CsvBookRepository(booksPath);
            IReaderRepository readerRepo = new CsvReaderRepository(readersPath);
            
            IBorrowService borrowService = new BorrowService(bookRepo, readerRepo);
            IReportService reportService = new ReportService(readerRepo, bookRepo);

            // Pre-seed sample data check (if files are empty but created by repo logic)
            // (Already handled by my create file logic if I ran it, but good to be safe)

            // UI Loop
            while (true)
            {
                Console.WriteLine("\n=== Library Management System ===");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. Search Books");
                Console.WriteLine("3. Lend Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. Generate Report");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddBookUI(bookRepo);
                        break;
                    case "2":
                        SearchBooksUI(bookRepo);
                        break;
                    case "3":
                        LendBookUI(borrowService);
                        break;
                    case "4":
                        ReturnBookUI(borrowService);
                        break;
                    case "5":
                        reportService.GenerateReport();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void AddBookUI(IBookRepository bookRepo)
        {
            Console.WriteLine("\n-- Add New Book --");
            Console.Write("Enter ID: ");
            string id = Console.ReadLine();
            
            if (bookRepo.GetBookById(id) != null)
            {
                Console.WriteLine("Book with this ID already exists.");
                return;
            }

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter Category: ");
            string category = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            // Defaulting to RegularBook for now, as per simple requirement
            // Extensibility point: Ask for type or default
            var book = new RegularBook(id, title, author, category, quantity);
            bookRepo.AddBook(book);
            Console.WriteLine("Book added successfully.");
        }

        static void SearchBooksUI(IBookRepository bookRepo)
        {
            Console.WriteLine("\n-- Search Books --");
            Console.Write("Enter Title or Category to search: ");
            string query = Console.ReadLine();
            
            var results = bookRepo.SearchBooks(query);
            foreach (var b in results)
            {
                Console.WriteLine($"[{b.Id}] {b.Title} by {b.Author} ({b.Category}) - Qty: {b.Quantity}");
            }
        }

        static void LendBookUI(IBorrowService borrowService)
        {
            Console.WriteLine("\n-- Lend Book --");
            Console.Write("Enter Reader ID: ");
            string rId = Console.ReadLine();
            Console.Write("Enter Book ID: ");
            string bId = Console.ReadLine();

            borrowService.LendBook(rId, bId);
        }

        static void ReturnBookUI(IBorrowService borrowService)
        {
            Console.WriteLine("\n-- Return Book --");
            Console.Write("Enter Reader ID: ");
            string rId = Console.ReadLine();
            Console.Write("Enter Book ID: ");
            string bId = Console.ReadLine();

            borrowService.ReturnBook(rId, bId);
        }
    }
}
