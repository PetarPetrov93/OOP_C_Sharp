using _03.Telephony.Core;
using _03.Telephony.Core.Interfaces;
using _03.Telephony.IO;
using _03.Telephony.Models;
using _03.Telephony.Models.Interfaces;

namespace _03.Telephony
{
    public class Program
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());

            engine.Run();
        }
    }
}