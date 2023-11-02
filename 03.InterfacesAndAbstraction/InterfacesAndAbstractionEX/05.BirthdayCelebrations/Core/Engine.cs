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
            List<IHasABirthday> creaturesWithBirthdays = new List<IHasABirthday>();
            while ((cmd = reader.ReadLine()) != "End")
            {
                string[] info = cmd.Split(" ");
                IHasABirthday hasABirthday = null;
                if (info[0] == "Citizen")
                {
                    hasABirthday = new Person(info[1], int.Parse(info[2]), info[3], info[4]);
                    creaturesWithBirthdays.Add(hasABirthday);
                }
                else if (info[0] == "Pet")
                {
                    hasABirthday = new Pet(info[1], info[2]);
                    creaturesWithBirthdays.Add(hasABirthday);
                }

                //Don't need the robot class in this task
            }
            
            int year = int.Parse(reader.ReadLine());


            foreach (var creature in creaturesWithBirthdays)
            {
                int yearBorn = int.Parse(creature.Birthday.Substring(creature.Birthday.LastIndexOf('/')+1));
                if (yearBorn == year)
                {
                    writer.WriteLine(creature.Birthday);
                }
            }
        }
    }
}
