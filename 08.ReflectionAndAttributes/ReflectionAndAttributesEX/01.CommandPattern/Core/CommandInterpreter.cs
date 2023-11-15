using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] arguments = args.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string commandInput = arguments[0];
            string[] data = arguments.Skip(1).ToArray();

            Type commandType = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(t => t.Name == $"{commandInput}Command");

            if (commandType is null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            ICommand command = Activator.CreateInstance(commandType) as ICommand;

            string result = command.Execute(data);

            return result;
        }
    }
}
