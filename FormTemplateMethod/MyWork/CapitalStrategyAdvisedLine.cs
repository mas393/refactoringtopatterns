namespace FormTemplateMethod.MyWork
{
    public class CapitalStrategyAdvisedLine : CapitalStrategy
    {
        protected override double RiskFactorFor(Loan loan)
        {
            return base.RiskFactorFor(loan) * loan.GetUnusedPercentage();
        }
    }
}