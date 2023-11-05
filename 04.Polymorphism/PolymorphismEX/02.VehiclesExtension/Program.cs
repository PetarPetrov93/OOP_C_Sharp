using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.IO;

namespace Vehicles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Iengine engine = new Engine(new ConsoleReader(), new  ConsoleWriter());
            engine.Run();
        }
    }
}