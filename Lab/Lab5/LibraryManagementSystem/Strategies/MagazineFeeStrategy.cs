namespace LibraryManagementSystem.Strategies
{
    public class MagazineFeeStrategy : IFeeStrategy
    {
        public decimal CalculateFee(int days) => days * 1500;
    }
}
