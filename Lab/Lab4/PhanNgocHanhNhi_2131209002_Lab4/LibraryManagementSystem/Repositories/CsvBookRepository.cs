using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class CsvBookRepository : IBookRepository
    {
        private readonly string _filePath;

        public CsvBookRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
        }

        public void AddBook(Book book)
        {
            var books = GetAllBooks().ToList();
            books.Add(book);
            WriteBooks(books);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            if (!File.Exists(_filePath)) return new List<Book>();

            var lines = File.ReadAllLines(_filePath);
            var books = new List<Book>();

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length >= 6)
                {
                    var id = parts[0];
                    var type = parts[1];
                    var title = parts[2];
                    var author = parts[3];
                    var category = parts[4];
                    if (int.TryParse(parts[5], out int quantity))
                    {
                         books.Add(BookFactory.CreateBook(type, id, title, author, category, quantity));
                    }
                }
            }
            return books;
        }

        public Book GetBookById(string id)
        {
            return GetAllBooks().FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> SearchBooks(string query)
        {
            return GetAllBooks().Where(b => 
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                b.Category.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateBook(Book book)
        {
            var books = GetAllBooks().ToList();
            var index = books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                books[index] = book;
                WriteBooks(books);
            }
        }

        private void WriteBooks(IEnumerable<Book> books)
        {
            var lines = new List<string> { "Id,Type,Title,Author,Category,Quantity" };
            foreach (var book in books)
            {
                lines.Add($"{book.Id},{book.GetBookType()},{book.Title},{book.Author},{book.Category},{book.Quantity}");
            }
            File.WriteAllLines(_filePath, lines);
        }
    }
}
