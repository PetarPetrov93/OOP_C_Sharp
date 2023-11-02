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
            string cmd;
            List<IIdentifiable> citizens = new List<IIdentifiable>();
            while ((cmd = reader.ReadLine()) != "End")
            {
                string[] info = cmd.Split(" ");
                IIdentifiable identifiable = null;
                if (info.Length == 3)
                {
                    identifiable = new Person(info[0], int.Parse(info[1]), info[2]);
                }
                else if (info.Length == 2)
                {
                    identifiable = new Robot(info[0], info[1]);
                }
                citizens.Add(identifiable);
            }
            
            string fakeID = reader.ReadLine();

            foreach (var citizen in citizens)
            {
                string lastTwoDigitsOfID = citizen.ID.Substring(citizen.ID.Length-fakeID.Length);
                if (fakeID == lastTwoDigitsOfID)
                {
                    writer.WriteLine(citizen.ID);
                }
            }
        }
    }
}
