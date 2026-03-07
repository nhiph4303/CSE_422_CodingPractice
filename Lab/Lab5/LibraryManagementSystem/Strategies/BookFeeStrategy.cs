namespace LibraryManagementSystem.Strategies
{
    public class BookFeeStrategy : IFeeStrategy
    {
        public decimal CalculateFee(int days) => days * 2000;
    }
}
