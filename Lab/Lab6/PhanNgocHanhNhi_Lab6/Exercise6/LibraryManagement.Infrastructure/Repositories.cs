using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _store = new();

    public IEnumerable<Book> GetAll() => _store.AsReadOnly();

    public Book? GetById(int id) => _store.FirstOrDefault(b => b.Id == id);

    public void Add(Book book) => _store.Add(book);

    public void Update(Book book)
    {
        var index = _store.FindIndex(b => b.Id == book.Id);
        if (index >= 0) _store[index] = book;
    }
}

public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _store = new();

    public IEnumerable<Member> GetAll() => _store.AsReadOnly();

    public Member? GetById(int id) => _store.FirstOrDefault(m => m.Id == id);

    public void Add(Member member) => _store.Add(member);
}

public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _store = new();

    public IEnumerable<Loan> GetAll() => _store.AsReadOnly();

    public Loan? GetById(int id) => _store.FirstOrDefault(l => l.Id == id);

    public void Add(Loan loan) => _store.Add(loan);

    public void Update(Loan loan)
    {
        var index = _store.FindIndex(l => l.Id == loan.Id);
        if (index >= 0) _store[index] = loan;
    }

    public IEnumerable<Loan> GetByMember(int memberId)
        => _store.Where(l => l.MemberId == memberId);
}
