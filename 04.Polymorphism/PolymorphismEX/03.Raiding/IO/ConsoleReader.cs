using _03.Raiding.IO.Interfaces;
using System;

namespace _03.Raiding.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
