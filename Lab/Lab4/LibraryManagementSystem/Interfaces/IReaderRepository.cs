using System.Collections.Generic;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces
{
    public interface IReaderRepository
    {
        void AddReader(Reader reader);
        Reader GetReaderById(string id);
        IEnumerable<Reader> GetAllReaders();
        void UpdateReader(Reader reader);
    }
}
