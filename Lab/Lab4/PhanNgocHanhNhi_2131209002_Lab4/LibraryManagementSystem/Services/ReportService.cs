using System;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IBookRepository _bookRepository;

        public ReportService(IReaderRepository readerRepository, IBookRepository bookRepository)
        {
            _readerRepository = readerRepository;
            _bookRepository = bookRepository;
        }

        public void GenerateReport()
        {
            var readers = _readerRepository.GetAllReaders();
            Console.WriteLine("=== Library Report ===");
            foreach (var reader in readers)
            {
                Console.WriteLine($"Reader: {reader.Name} (ID: {reader.Id})");
                if (reader.BorrowedBookIds.Count == 0)
                {
                    Console.WriteLine("  No books borrowed.");
                }
                else
                {
                    foreach (var bookId in reader.BorrowedBookIds)
                    {
                        var book = _bookRepository.GetBookById(bookId);
                        if (book != null)
                        {
                            Console.WriteLine($"  - {book.Title} (Type: {book.GetBookType()})");
                        }
                        else
                        {
                            Console.WriteLine($"  - Book ID {bookId} (Unknown/Deleted)");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
