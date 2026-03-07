using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Factories
{
    public static class DocumentFactory
    {
        public static IDocument CreateDocument(string type, string title)
        {
            return type.ToLower() switch
            {
                "book" => new Book(title),
                "magazine" => new Magazine(title),
                "newspaper" => new Newspaper(title),
                _ => throw new ArgumentException("Invalid document type!")
            };
        }
    }
}
