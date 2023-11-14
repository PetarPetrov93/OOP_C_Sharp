using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private readonly IRepository<ILoan> loans;
        private readonly IRepository<IBank> banks;
        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != "BranchBank" && bankTypeName != "CentralBank")
            {
                throw new ArgumentException("Invalid bank type.");
            }

            IBank bank = null;

            if (bankTypeName == "BranchBank")
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == "CentralBank")
            {
                bank = new CentralBank(name);
            }
            banks.AddModel(bank);

            return $"{bankTypeName} is successfully added.";
        }
        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != "StudentLoan" && loanTypeName != "MortgageLoan")
            {
                throw new ArgumentException("Invalid loan type.");
            }

            ILoan loan = null;

            if (loanTypeName == "StudentLoan")
            {
                loan = new StudentLoan();
            }
            else if (loanTypeName == "MortgageLoan")
            {
                loan = new MortgageLoan();
            }
            loans.AddModel(loan);

            return $"{loanTypeName} is successfully added.";
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            if (!loans.Models.Any(l => l.GetType().Name == loanTypeName))
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }
            ILoan loan = loans.Models.FirstOrDefault(l => l.GetType().Name == loanTypeName);
            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);

            bank.AddLoan(loan);
            loans.RemoveModel(loan);

            return $"{loanTypeName} successfully added to {bankName}.";
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            if (clientTypeName != "Student" && clientTypeName != "Adult")
            {
                throw new ArgumentException("Invalid client type.");
            }

            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);

            if (clientTypeName == "Adult" && bank.GetType().Name != nameof(CentralBank))
            {
                return "Unsuitable bank.";
            }
            if (clientTypeName == "Student" && bank.GetType().Name != nameof(BranchBank))
            {
                return "Unsuitable bank.";
            }

            IClient client = null;

            if (clientTypeName == "Student")
            {
                client = new Student(clientName, id, income);
            }
            else if (clientTypeName == "Adult")
            {
                client = new Adult(clientName, id, income);
            }

            bank.AddClient(client);

            return $"{clientTypeName} successfully added to {bankName}.";
        }


        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.Models.FirstOrDefault(b => b.Name == bankName);

            double funds = bank.Loans.Sum(l => l.Amount);
            funds += bank.Clients.Sum(c => c.Income);

            return $"The funds of bank {bankName} are {funds :f2}.";
        }


        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IBank bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
