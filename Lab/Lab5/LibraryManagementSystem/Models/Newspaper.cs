using System;

namespace LibraryManagementSystem.Models
{
    public class Newspaper : IDocument
    {
        public string Title { get; private set; }
        public Newspaper(string title) => Title = title;
        public void DisplayInfo() => Console.WriteLine($"[Document] Newspaper: {Title}");
    }
}
