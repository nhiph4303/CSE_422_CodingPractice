namespace LibraryManagementSystem.Models
{
    public interface IDocument
    {
        string Title { get; }
        void DisplayInfo();
    }
}
