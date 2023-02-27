using System;

namespace ChainConstructors.MyWork
{
    public class Loan
    {
        private readonly CapitalStrategy _strategy;
        private float _notional;
        private float _outstanding;
        private int _rating;
        private DateTime _expiry;
        private Nullable<DateTime> _maturity;

        private Loan(CapitalStrategy strategy, float notional, float outstanding, 
                    int rating, DateTime expiry, DateTime? maturity)
        {
            this._strategy = strategy;
            this._notional = notional;
            this._outstanding = outstanding;
            this._rating = rating;
            this._expiry = expiry;
            this._maturity = maturity;
        }

		public CapitalStrategy CapitalStrategy
		{
			get
			{
				return _strategy;
			}
		}

        static public Loan TermROCLoan(float notional, float outstanding, int rating, DateTime expiry)
        {
            return new Loan(new TermROC(), notional, outstanding, rating, expiry, null);
        }

        static public Loan RevolvingTermROCLoan(float notional, float outstanding, int rating, DateTime expiry, DateTime maturity)
        {
            return new Loan(new RevolvingTermROC(), notional, outstanding, rating, expiry, maturity);
        }
		
    }
}