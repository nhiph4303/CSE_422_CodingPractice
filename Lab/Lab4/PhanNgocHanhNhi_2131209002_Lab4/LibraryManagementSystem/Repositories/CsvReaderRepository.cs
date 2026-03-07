using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class CsvReaderRepository : IReaderRepository
    {
        private readonly string _filePath;

        public CsvReaderRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
        }

        public void AddReader(Reader reader)
        {
            var readers = GetAllReaders().ToList();
            readers.Add(reader);
            WriteReaders(readers);
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            if (!File.Exists(_filePath)) return new List<Reader>();

            var lines = File.ReadAllLines(_filePath);
            var readers = new List<Reader>();

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    var id = parts[0];
                    var name = parts[1];
                    var reader = new Reader(id, name);
                    
                    if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]))
                    {
                        var bookIds = parts[2].Split(';', StringSplitOptions.RemoveEmptyEntries);
                        reader.BorrowedBookIds.AddRange(bookIds);
                    }
                    readers.Add(reader);
                }
            }
            return readers;
        }

        public Reader GetReaderById(string id)
        {
            return GetAllReaders().FirstOrDefault(r => r.Id == id);
        }

        public void UpdateReader(Reader reader)
        {
            var readers = GetAllReaders().ToList();
            var index = readers.FindIndex(r => r.Id == reader.Id);
            if (index != -1)
            {
                readers[index] = reader;
                WriteReaders(readers);
            }
        }

        private void WriteReaders(IEnumerable<Reader> readers)
        {
            var lines = new List<string> { "Id,Name,BorrowedBookIds" };
            foreach (var reader in readers)
            {
                var borrowed = string.Join(";", reader.BorrowedBookIds);
                lines.Add($"{reader.Id},{reader.Name},{borrowed}");
            }
            File.WriteAllLines(_filePath, lines);
        }
    }
}
