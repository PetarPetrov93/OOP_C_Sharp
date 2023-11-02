using _03.Telephony.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public string Call(string number)
        {
            if (!ValidatePhoneNumber(number))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Dialing... {number}";
        }
        private bool ValidatePhoneNumber(string number) => number.All(ch => char.IsDigit(ch));
    }
}
