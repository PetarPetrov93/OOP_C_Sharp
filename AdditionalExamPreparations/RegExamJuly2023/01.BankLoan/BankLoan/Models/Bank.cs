using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private readonly ICollection<ILoan> loans;
        private readonly ICollection<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name 
        { 
            get => name;
            private set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Bank name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans.ToList().AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.ToList().AsReadOnly();


        public void AddClient(IClient Client)
        {
            if (clients.Count >= Capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan) => loans.Add(loan);

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}, Type: { GetType().Name}");

            sb.Append("Clients: ");

            if (clients.Any())
            {
                List<string> clientsNames = clients.Select(c => c.Name).ToList();
                sb.AppendLine(string.Join(", ", clientsNames));
            }
            else
            {
                sb.AppendLine("none");
            }
            
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {this.SumRates()}"); //SumRates should work, if not should just change it as it is in the method

            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client) => clients.Remove(Client);

        public double SumRates() => loans.Sum(l => l.InterestRate);
    }
}
