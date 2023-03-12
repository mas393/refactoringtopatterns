﻿using System;
namespace FormTemplateMethod.MyWork
{
    public abstract class CapitalStrategy
    {

        private long MILLIS_PER_DAY = 86400000;
        private long DAYS_PER_YEAR = 365;

        public virtual double Capital(Loan loan)
        {
            return RiskAmountFor(loan) * Duration(loan) * RiskFactorFor(loan);
        }

        protected virtual double RiskAmountFor(Loan loan)
        {
            return loan.GetCommitment();
        }

        protected virtual double RiskFactorFor(Loan loan)
        {
            return RiskFactor.GetFactors().ForRating(loan.GetRiskRating());
        }

        private double UnusedRiskFactorFor(Loan loan)
        {
            return UnusedRiskFactors.GetFactors().ForRating(loan.GetRiskRating());
        }

        public virtual double Duration(Loan loan)
        {
            return YearsTo(loan.GetExpiry(), loan);
        }

        protected double YearsTo(DateTime? endDate, Loan loan)
        {
            DateTime? beginDate = (loan.GetToday() == null ? loan.GetStart() : loan.GetToday());
            return (double)((endDate?.Ticks - beginDate?.Ticks) / MILLIS_PER_DAY / DAYS_PER_YEAR);
        }
    }
}