using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models
{
    public class Reader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> BorrowedBookIds { get; set; } = new List<string>();

        public Reader(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
