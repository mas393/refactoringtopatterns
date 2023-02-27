using System;
using System.Collections.Generic;

namespace ReplaceConditionalLogicWithStrategy.MyWork
{
    public class Loan
    {
        double _commitment = 1.0;
        private DateTime? _expiry;
        private DateTime? _maturity;
        private double _outstanding;
        IList<Payment> _payments = new List<Payment>();
        private DateTime? _today = DateTime.Now;
        private DateTime _start;
        private double _riskRating;
        private double _unusedPercentage;

        private CapitalStrategy _capitalStrategy;

        public DateTime? expiry()
        {
            return _expiry;
        }

        public DateTime? maturity()
        {
            return _maturity;
        }

        public double commitment()
        {
            return _commitment;
        }

        public double riskRating()
        {
            return _riskRating;
        }

        public DateTime? today()
        {
            return _today;
        }

        public DateTime start()
        {
            return _start;
        }

        public Loan(double commitment, double notSureWhatThisIs, DateTime start, DateTime? expiry, DateTime? maturity, int riskRating, CapitalStrategy capitalStrategy)
        {
            this._expiry = expiry;
            this._commitment = commitment;
            this._today = null;
            this._start = start;
            this._maturity = maturity;
            this._riskRating = riskRating;
            this._unusedPercentage = 1.0;
            this._capitalStrategy = capitalStrategy;
        }

        public static Loan NewTermLoan(double commitment, DateTime start, DateTime maturity, int riskRating)
        {
            return new Loan(commitment, commitment, start, null, 
                            maturity, riskRating, new CaptialStrategyTermLoan());
        }

        public static Loan NewRevolver(double commitment, DateTime start, DateTime expiry, int riskRating) 
        {
            return new Loan(commitment, 0, start, expiry,
                            null, riskRating, new CapitalStrategyRevolver());
        }

        public static Loan NewAdvisedLine(double commitment, DateTime start, DateTime expiry, int riskRating)
        {
            if (riskRating > 3) return null;
            Loan advisedLine = new Loan(commitment, 0, start, expiry,
                            null, riskRating, new CapitalStrategyAdvisedLine());
            advisedLine.SetUnusedPercentage(0.1);
            return advisedLine;
        }

        public void Payment(double amount, DateTime paymentDate)
        {
            _payments.Add(new Payment(amount, paymentDate));
        }

        public double Capital() {
            return _capitalStrategy.Capital(this);
        }

        public double Duration() {
            return _capitalStrategy.Duration(this);
        }

        internal double GetUnusedPercentage()
        {
            return _unusedPercentage;
        }

        public void SetUnusedPercentage(double unusedPercentage) 
        {
            _unusedPercentage = unusedPercentage;
        }

        internal double UnusedRiskAmount()
        {
            return (_commitment - _outstanding);
        }

        internal double OutstandingRiskAmount()
        {
            return _outstanding;
        }

        internal IList<Payment> payments()
        {
            return _payments;
        }
    }

    public abstract class CapitalStrategy
    {
        private long MILLIS_PER_DAY = 86400000;
        private long DAYS_PER_YEAR = 365;

        public abstract double Capital(Loan loan);

        //why would we want to pass the whole loan object into this instead of just the used parameter?
        protected double UnusedRiskFactor(double riskRating)
        {
            return UnusedRiskFactors.GetFactors().ForRating(riskRating);
        }

        protected double RiskFactor(double riskRating)
        {
            return InitialCode.RiskFactor.GetFactors().ForRating(riskRating);
        }
    
        public virtual double Duration(Loan loan)
        {
            return YearsTo(loan.expiry(), loan);
        }

        protected double YearsTo(DateTime? endDate, Loan loan)
        {
            DateTime? beginDate = (loan.today() == null ? loan.start() : loan.today());
            return (double)((endDate?.Ticks - beginDate?.Ticks) / MILLIS_PER_DAY / DAYS_PER_YEAR);
        }
    }

    public class CaptialStrategyTermLoan: CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return loan.commitment() * Duration(loan) * RiskFactor(loan.riskRating());
        }

        public override double Duration(Loan loan)
        {
            return WeightedAverageDuration(loan);
        }

        private double WeightedAverageDuration(Loan loan)
        { 
            double duration = 0.0;
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;

            foreach (var payment in loan.payments())
            {
                sumOfPayments += payment.Amount;
                weightedAverage += YearsTo(payment.Date, loan) * payment.Amount;
            }

            if (loan.commitment() != 0.0)
            {
                duration = weightedAverage / sumOfPayments;
            }

            return duration;
        }
    }

    public class CapitalStrategyAdvisedLine: CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return loan.commitment() * loan.GetUnusedPercentage() * Duration(loan) * RiskFactor(loan.riskRating());
        }
    }

    public class CapitalStrategyRevolver: CapitalStrategy
    {
        public override double Capital(Loan loan)
        {
            return (loan.OutstandingRiskAmount() * Duration(loan) * RiskFactor(loan.riskRating()))
                    + (loan.UnusedRiskAmount() * Duration(loan) * UnusedRiskFactor(loan.riskRating()));
        }
    }
}