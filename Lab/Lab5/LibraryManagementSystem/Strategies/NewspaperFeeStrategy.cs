namespace LibraryManagementSystem.Strategies
{
    public class NewspaperFeeStrategy : IFeeStrategy
    {
        public decimal CalculateFee(int days) => days * 1000;
    }
}
