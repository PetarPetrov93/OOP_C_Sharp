using _03.Telephony.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string number)
        {
            if (!ValidatePhoneNumber(number))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Calling... {number}";
        }
        public string Browse(string url)
        {
            if (!ValidateURL(url))
            {
                throw new ArgumentException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }
        private bool ValidatePhoneNumber(string number) => number.All(ch => char.IsDigit(ch));

        private bool ValidateURL(string url) => url.All(ch => !char.IsDigit(ch));
    }
}
