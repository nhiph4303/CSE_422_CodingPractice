using System;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;

        public BorrowService(IBookRepository bookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
        }

        public bool LendBook(string readerId, string bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var reader = _readerRepository.GetReaderById(readerId);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }
            if (reader == null)
            {
                Console.WriteLine("Reader not found.");
                return false;
            }

            if (book.Quantity <= 0)
            {
                Console.WriteLine("Book is out of stock.");
                return false;
            }

            if (reader.BorrowedBookIds.Count >= 3)
            {
                Console.WriteLine("Reader has reached the borrowing limit (3 books).");
                return false;
            }

            book.Quantity--;
            reader.BorrowedBookIds.Add(bookId);

            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);
            
            Console.WriteLine($"Book '{book.Title}' lent to {reader.Name}.");
            return true;
        }

        public bool ReturnBook(string readerId, string bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var reader = _readerRepository.GetReaderById(readerId);

            if (book == null || reader == null)
            {
                Console.WriteLine("Book or Reader not found.");
                return false;
            }

            if (!reader.BorrowedBookIds.Contains(bookId))
            {
                Console.WriteLine("Reader does not have this book.");
                return false;
            }

            book.Quantity++;
            reader.BorrowedBookIds.Remove(bookId);

            _bookRepository.UpdateBook(book);
            _readerRepository.UpdateReader(reader);

            Console.WriteLine($"Book '{book.Title}' returned by {reader.Name}.");
            return true;
        }
    }
}
