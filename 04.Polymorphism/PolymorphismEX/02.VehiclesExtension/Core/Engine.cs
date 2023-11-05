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
            
            IDrivable car = new Car(double.Parse(carInput[1]), double.Parse(carInput[2]), double.Parse(carInput[3]));

            string[] truckInput = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            IDrivable truck = new Truck(double.Parse(truckInput[1]), double.Parse(truckInput[2]), double.Parse(truckInput[3]));

            string[] busInput = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            IDrivable bus = new Bus(double.Parse(busInput[1]), double.Parse(busInput[2]), double.Parse(busInput[3]));

            int n = int.Parse(reader.ReadLine());

            List<IDrivable> vehicles = new List<IDrivable>();
            vehicles.Add(car);
            vehicles.Add(truck);
            vehicles.Add(bus);

            IDrivable vehicleToDrive = null;
            for (int i = 0; i < n; i++)
            {
                string[] inputData = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = inputData[0];
                string vehicleType = inputData[1];

                try
                {
                    if (command == "Drive")
                    {
                        double kmToDrive = double.Parse(inputData[2]);
                        vehicleToDrive = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
                        writer.Writeline(vehicleToDrive.Drive(kmToDrive));
                    }
                    else if (command == "DriveEmpty")
                    {
                        double kmToDrive = double.Parse(inputData[2]);
                        vehicleToDrive = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
                        writer.Writeline(vehicleToDrive.Drive(kmToDrive, false));
                    }
                    else if (command == "Refuel")
                    {
                        double fuel = double.Parse(inputData[2]);

                        vehicleToDrive = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);
                        vehicleToDrive.Refuel(fuel);

                    }
                }
                catch (Exception ex)
                {

                    writer.Writeline(ex.Message);
                }
                
            }
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

        }
    }
}
