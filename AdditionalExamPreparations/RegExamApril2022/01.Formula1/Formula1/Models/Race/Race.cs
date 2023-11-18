using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models.Race
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private readonly ICollection<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            pilots = new List<IPilot>();
        }

        public string RaceName 
        { 
            get => raceName;
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid race name: {value}.");
                }
                raceName = value;
            } 
        }

        public int NumberOfLaps 
        { 
            get => numberOfLaps;
            private set 
            {
                if (value < 1)
                {
                    throw new ArgumentException($"Invalid lap numbers: {value}.");
                }
                numberOfLaps = value;
            }
        }

        public bool TookPlace { get; set; }

        public ICollection<IPilot> Pilots => pilots;

        public void AddPilot(IPilot pilot) => pilots.Add(pilot);

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {RaceName} race has:");
            sb.AppendLine($"Participants: {pilots.Count}");
            sb.AppendLine($"Number of laps: {NumberOfLaps}");
            sb.AppendLine($"Took place: {(TookPlace ? "Yes" : "No")}");

            return sb.ToString().TrimEnd();
        }
    }
}
