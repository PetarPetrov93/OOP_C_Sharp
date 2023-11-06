using _03.Raiding.Core;
using _03.Raiding.Core.Interfaces;
using _03.Raiding.Factories;
using _03.Raiding.IO;

namespace _03.Raiding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new CreateAHero());
            engine.Run();
        }
    }
}