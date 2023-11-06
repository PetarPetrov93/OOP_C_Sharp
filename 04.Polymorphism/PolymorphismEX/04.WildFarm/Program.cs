using _04.WildFarm.Core;
using _04.WildFarm.Core.Interfaces;
using _04.WildFarm.Factories;
using _04.WildFarm.IO;

namespace _04.WildFarm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new AnimalFactory());
            engine.Run();
        }
    }
}