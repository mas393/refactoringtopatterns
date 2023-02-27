﻿using NUnit.Framework;
using ChainConstructors.MyWork;
using System;

namespace RefactoringToPatterns.ChainConstructors.MyWork
{
    [TestFixture]
	public class CapitalCalculationTests
	{
        [SetUp]
		public void Init()
		{
		}

        [Test]
        public void four_parameter_constructor_yields_loan_with_TermROC_strategy()
		{
            Loan loan = Loan.TermROCLoan(1.0f, 2.0f, 4, new DateTime());
            Assert.IsInstanceOf(typeof(TermROC), loan.CapitalStrategy);
		}

        [Test]
        public void five_parameter_constructor_yields_revolving_TermROC_strategy() 
        {
            Loan loan = Loan.RevolvingTermROCLoan(1.0f, 2.0f, 4, new DateTime(), new DateTime());
            Assert.IsInstanceOf(typeof(RevolvingTermROC), loan.CapitalStrategy);
        }   
	}
}