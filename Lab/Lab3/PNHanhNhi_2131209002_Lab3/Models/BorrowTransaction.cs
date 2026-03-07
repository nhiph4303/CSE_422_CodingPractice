using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class BorrowTransaction : Transaction
    {
        public Book BookBorrowed {  get; set; }

        public BorrowTransaction (Member member, Book book) : base(member)
        {
            BookBorrowed = book ?? throw new ArgumentNullException(nameof(book));
        }
        public override void Execute()
        {
            Console.WriteLine($"\n[Transaction {TransactionID}] Processing Borrow...");
            Member.BorrowBook(BookBorrowed);
        }
    }
}
