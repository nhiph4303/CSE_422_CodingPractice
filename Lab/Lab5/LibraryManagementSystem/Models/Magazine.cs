using System;

namespace LibraryManagementSystem.Models
{
    public class Magazine : IDocument
    {
        public string Title { get; private set; }
        public Magazine(string title) => Title = title;
        public void DisplayInfo() => Console.WriteLine($"[Document] Magazine: {Title}");
    }
}
