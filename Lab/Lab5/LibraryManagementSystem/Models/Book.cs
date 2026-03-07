using System;

namespace LibraryManagementSystem.Models
{
    public class Book : IDocument
    {
        public string Title { get; private set; }
        public Book(string title) => Title = title;
        public void DisplayInfo() => Console.WriteLine($"[Document] Book: {Title}");
    }
}
