using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Application.Services;
using LibraryManagement.Infrastructure.Repositories;

// ── Dependency Injection (manual composition root) ──────────────────
IBookRepository   bookRepo   = new InMemoryBookRepository();
IMemberRepository memberRepo = new InMemoryMemberRepository();
ILoanRepository   loanRepo   = new InMemoryLoanRepository();

var bookService   = new BookService(bookRepo);
var memberService = new MemberService(memberRepo);
var loanService   = new LoanService(bookRepo, loanRepo);

// ── Seed data ────────────────────────────────────────────────────────
bookService.AddBook(new Book { Id = 1, Title = "Clean Code",          Author = "Robert C. Martin" });
bookService.AddBook(new Book { Id = 2, Title = "Design Patterns",     Author = "Gang of Four" });
bookService.AddBook(new Book { Id = 3, Title = "The Pragmatic Programmer", Author = "Hunt & Thomas" });

memberService.Register(new Member { Id = 1, Name = "Alice", Email = "alice@example.com" });
memberService.Register(new Member { Id = 2, Name = "Bob",   Email = "bob@example.com" });

// ── Demo ─────────────────────────────────────────────────────────────
Console.WriteLine("══════════════════════════════════════════════════════");
Console.WriteLine(" Exercise 6 – Library Management System (4 Projects)");
Console.WriteLine("══════════════════════════════════════════════════════\n");

Console.WriteLine("── Available Books ──");
foreach (var b in bookService.GetAvailableBooks())
    Console.WriteLine($"  [{b.Id}] {b.Title} by {b.Author}");

Console.WriteLine();
Console.WriteLine("── Borrow: Alice takes 'Clean Code' (Id=1) ──");
var loan = loanService.BorrowBook(bookId: 1, memberId: 1);
Console.WriteLine($"  Loan created: Id={loan.Id}, BookId={loan.BookId}, MemberId={loan.MemberId}");

Console.WriteLine();
Console.WriteLine("── Available Books after borrow ──");
foreach (var b in bookService.GetAvailableBooks())
    Console.WriteLine($"  [{b.Id}] {b.Title}");

Console.WriteLine();
Console.WriteLine("── Return: Alice returns loan Id=1 ──");
loanService.ReturnBook(loanId: 1);
Console.WriteLine("  Book returned.");

Console.WriteLine();
Console.WriteLine("── Available Books after return ──");
foreach (var b in bookService.GetAvailableBooks())
    Console.WriteLine($"  [{b.Id}] {b.Title}");

Console.WriteLine();
Console.WriteLine("── Alice's loan history ──");
foreach (var l in loanService.GetMemberLoans(memberId: 1))
    Console.WriteLine($"  LoanId={l.Id}, BookId={l.BookId}, Returned={l.IsReturned}");

Console.WriteLine();
Console.WriteLine("══════════════════════════════════════════════════════");
