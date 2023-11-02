using _04.BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Models
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string value) => Console.WriteLine(value);
    }
}
