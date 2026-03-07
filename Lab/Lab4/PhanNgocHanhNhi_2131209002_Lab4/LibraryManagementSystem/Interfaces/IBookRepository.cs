using System.Collections.Generic;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        Book GetBookById(string id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string query);
        void UpdateBook(Book book);
    }
}
