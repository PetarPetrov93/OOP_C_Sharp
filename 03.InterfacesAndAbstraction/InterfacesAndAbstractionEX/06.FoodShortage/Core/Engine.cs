using _04.BorderControl.Core.Interfaces;
using _04.BorderControl.Models;
using _04.BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl.Core
{
    public class Engine : IEngine
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
            //Don't need the robot nor the pet class in this task

            int n = int.Parse(reader.ReadLine());
            List<IBuyer> buyers = new List<IBuyer>();

            for (int i = 0; i < n; i++)
            {
                IBuyer buyer = null;

                string[] info = reader.ReadLine().Split(" ");

                if (info.Length == 4)
                {
                    buyer = new Person(info[0], int.Parse(info[1]), info[2], info[3]);

                }
                else if (info.Length == 3)
                {
                    buyer = new Rebel(info[0], int.Parse(info[1]), info[2]);
                    
                }
                buyers.Add(buyer);
            }
            string name;

            while ((name = reader.ReadLine()) != "End")
            {
                buyers.FirstOrDefault(b => b.Name == name)?.BuyFood();
            }

            writer.WriteLine(buyers.Sum(b => b.Food).ToString());
        }
    }
}
