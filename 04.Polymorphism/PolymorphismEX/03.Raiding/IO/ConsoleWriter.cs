using _03.Raiding.IO.Interfaces;
using System;


namespace _03.Raiding.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text) => Console.WriteLine(text);
    }
}
