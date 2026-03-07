namespace LibraryManagementSystem.Interfaces
{
    public interface IBorrowService
    {
        bool LendBook(string readerId, string bookId);
        bool ReturnBook(string readerId, string bookId);
    }
}
