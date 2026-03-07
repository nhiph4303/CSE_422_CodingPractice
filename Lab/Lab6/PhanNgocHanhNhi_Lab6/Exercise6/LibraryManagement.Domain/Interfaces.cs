using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    void Add(Book book);
    void Update(Book book);
}

public interface IMemberRepository
{
    IEnumerable<Member> GetAll();
    Member? GetById(int id);
    void Add(Member member);
}

public interface ILoanRepository
{
    IEnumerable<Loan> GetAll();
    Loan? GetById(int id);
    void Add(Loan loan);
    void Update(Loan loan);
    IEnumerable<Loan> GetByMember(int memberId);
}
