using _03.Telephony.Core.Interfaces;
using _03.Telephony.Models.Interfaces;
using _03.Telephony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03.Telephony.IO.Interfaces;

namespace _03.Telephony.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            string[] numbers = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] websites = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var number in numbers)
            {
                ICallable callable;
                if (number.Length == 10)
                {
                    callable = new Smartphone();
                }
                else
                {
                    callable = new StationaryPhone();
                }
                try
                {
                    writer.WriteLine(callable.Call(number));
                }
                catch (Exception ex)
                {

                    writer.WriteLine(ex.Message);
                }

            }

            foreach (var url in websites)
            {
                IBrowsable browable = new Smartphone();
                try
                {
                    writer.WriteLine(browable.Browse(url));
                }
                catch (Exception ex)
                {

                    writer.WriteLine(ex.Message);
                }

            }
        }
    }
}
