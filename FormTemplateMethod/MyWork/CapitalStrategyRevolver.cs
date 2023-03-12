namespace FormTemplateMethod.MyWork
{
    public class CapitalStrategyRevolver : CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return base.Capital(loan) + (loan.UnusedRiskAmount() * Duration(loan) * UnusedRiskFactorFor(loan));
        }

        protected override double RiskAmountFor(Loan loan)
        {
            return loan.OutstandingRiskAmount();
        }

        private double UnusedRiskFactorFor(Loan loan)
        {
            return UnusedRiskFactors.GetFactors().ForRating(loan.GetRiskRating());
        }
    }
}