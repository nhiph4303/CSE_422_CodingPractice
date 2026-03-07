using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNHNhi_2131209002_Lab2.Models
{
    public class ReturnTransaction : Transaction
    {

        public Book BookReturned { get; set; }

        public ReturnTransaction (Member member, Book book) : base(member)
        {
            BookReturned = book ?? throw new ArgumentNullException(nameof(book));
        }

        public override void Execute()
        {
            Console.WriteLine($"\n[Transaction {TransactionID}] Processing Return...");

            Member.ReturnBook(BookReturned);
        }
    }
}
