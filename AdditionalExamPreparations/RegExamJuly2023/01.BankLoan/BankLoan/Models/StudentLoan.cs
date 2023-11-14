using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int STUDENT_INTEREST_RATE = 1;
        private const double STUDENT_AMMOUNT = 10_000;
        public StudentLoan() : base(STUDENT_INTEREST_RATE, STUDENT_AMMOUNT) { }
    }
}
