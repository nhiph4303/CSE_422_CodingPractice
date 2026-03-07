using System;
using System.Linq;
using System.Reflection;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public static class BookFactory
    {
        public static Book CreateBook(string type, string id, string title, string author, string category, int quantity)
        {
            var bookType = Assembly.GetAssembly(typeof(Book))
                                   .GetTypes()
                                   .FirstOrDefault(t => t.Name.Equals(type, StringComparison.OrdinalIgnoreCase) && typeof(Book).IsAssignableFrom(t));

            if (bookType == null)
            {
                throw new ArgumentException($"Unknown book type: {type}. Ensure a class with this name exists and inherits from Book.");
            }

            try
            {
                return (Book)Activator.CreateInstance(bookType, id, title, author, category, quantity);
            }
            catch (MissingMethodException)
            {
                throw new InvalidOperationException($"The book type '{type}' does not have a matching constructor accepting (id, title, author, category, quantity).");
            }
        }
    }
}
