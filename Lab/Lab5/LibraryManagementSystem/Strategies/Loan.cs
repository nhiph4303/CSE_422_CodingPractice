namespace LibraryManagementSystem.Strategies
{
    public class Loan
    {
        private IFeeStrategy _strategy;

        public Loan(IFeeStrategy strategy) => _strategy = strategy;

        public void SetStrategy(IFeeStrategy strategy) => _strategy = strategy;

        public decimal GetFee(int days) => _strategy.CalculateFee(days);
    }
}
