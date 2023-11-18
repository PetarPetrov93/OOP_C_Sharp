namespace Heroes.IO
{
    using System;
    using Heroes.IO.Contracts;
    public class Writer : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor= ConsoleColor.White;
        }
    }
}
