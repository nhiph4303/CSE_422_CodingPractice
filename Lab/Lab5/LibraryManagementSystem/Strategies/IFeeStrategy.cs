namespace LibraryManagementSystem.Strategies
{
    public interface IFeeStrategy
    {
        decimal CalculateFee(int days);
    }
}
