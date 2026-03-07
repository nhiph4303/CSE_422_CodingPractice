namespace LibraryManagementSystem.Models
{
    public class RegularBook : Book
    {
        public RegularBook(string id, string title, string author, string category, int quantity) 
            : base(id, title, author, category, quantity)
        {
        }

        public override string GetBookType()
        {
            return "RegularBook";
        }
    }
}
