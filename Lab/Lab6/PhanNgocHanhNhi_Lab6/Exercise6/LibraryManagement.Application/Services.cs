using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class BookService
{
    private readonly IBookRepository _books;

    public BookService(IBookRepository books) => _books = books;

    public IEnumerable<Book> GetAvailableBooks()
        => _books.GetAll().Where(b => b.IsAvailable);

    public void AddBook(Book book) => _books.Add(book);
}

public class LoanService
{
    private readonly IBookRepository _books;
    private readonly ILoanRepository _loans;
    private int _nextId = 1;

    public LoanService(IBookRepository books, ILoanRepository loans)
    {
        _books = books;
        _loans = loans;
    }

    public Loan BorrowBook(int bookId, int memberId)
    {
        var book = _books.GetById(bookId)
            ?? throw new Exception($"Book {bookId} not found.");

        if (!book.IsAvailable)
            throw new Exception($"Book '{book.Title}' is not available.");

        book.IsAvailable = false;
        _books.Update(book);

        var loan = new Loan
        {
            Id = _nextId++,
            BookId = bookId,
            MemberId = memberId,
            BorrowedAt = DateTime.Now
        };
        _loans.Add(loan);
        return loan;
    }

    public void ReturnBook(int loanId)
    {
        var loan = _loans.GetById(loanId)
            ?? throw new Exception($"Loan {loanId} not found.");

        loan.ReturnedAt = DateTime.Now;
        _loans.Update(loan);

        var book = _books.GetById(loan.BookId)!;
        book.IsAvailable = true;
        _books.Update(book);
    }

    public IEnumerable<Loan> GetMemberLoans(int memberId)
        => _loans.GetByMember(memberId);
}

public class MemberService
{
    private readonly IMemberRepository _members;

    public MemberService(IMemberRepository members) => _members = members;

    public void Register(Member member) => _members.Add(member);

    public IEnumerable<Member> GetAll() => _members.GetAll();
}
