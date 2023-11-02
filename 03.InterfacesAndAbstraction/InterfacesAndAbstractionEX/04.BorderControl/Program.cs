using _04.BorderControl.Core;
using _04.BorderControl.Core.Interfaces;
using _04.BorderControl.Models;

namespace _04.BorderControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());
            engine.Run();
        }
    }
}