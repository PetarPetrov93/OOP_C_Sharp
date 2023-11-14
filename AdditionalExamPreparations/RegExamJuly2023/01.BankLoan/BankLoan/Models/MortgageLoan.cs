using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int MORTGAGE_INTEREST_RATE = 3;
        private const double MORTGAGE_AMMOUNT = 50_000;
        public MortgageLoan() : base(MORTGAGE_INTEREST_RATE, MORTGAGE_AMMOUNT) { }
    }
}
