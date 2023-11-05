using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Core
{
    public class Engine : Iengine
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
            string[] carInput = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            IDrivable car = new Car(double.Parse(carInput[1]), double.Parse(carInput[2]));

            string[] truckInput = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            IDrivable truck = new Truck(double.Parse(truckInput[1]), double.Parse((truckInput[2])));

            int n = int.Parse(reader.ReadLine());

            List<IDrivable> vehicles = new List<IDrivable>();
            vehicles.Add(car);
            vehicles.Add(truck);

            IDrivable vehicleToDrive = null;
            for (int i = 0; i < n; i++)
            {
                string[] inputData = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = inputData[0];
                string vehicleType = inputData[1];
                
                if (command == "Drive")
                {
                    double kmToDrive = double.Parse(inputData[2]);
                    vehicleToDrive = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
                    writer.Writeline(vehicleToDrive.Drive(kmToDrive));
                }
                else if (command == "Refuel")
                {
                    double fuel = double.Parse(inputData[2]);
                    vehicleToDrive = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
                    vehicleToDrive.Refuel(fuel);
                }
            }
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

        }
    }
}
