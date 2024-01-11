using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private readonly ICollection<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            Name = name;
            Stamina = stamina;
            conqueredPeaks = new HashSet<string>();
        }

        public string Name 
        { 
            get => name;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Climber's name cannot be null or whitespace.");
                }
                name = value;
            }
        }

        public int Stamina // this should be the correct implementation
        { 
            get => stamina;
            protected set 
            {
                if (value < 0)
                {
                    stamina = 0;
                }
                else if (value > 10)
                {
                    stamina = 10;
                }
                else
                {
                    stamina = value;
                }
            } 
        }

        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks.ToList().AsReadOnly();

        public void Climb(IPeak peak)
        {
            conqueredPeaks.Add(peak.Name);
            if (peak.DifficultyLevel == "Extreme")
            {
                Stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                Stamina -= 4;
            }
            else if (peak.DifficultyLevel == "Moderate")
            {
                Stamina -= 2;
            }
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name} - Name: {Name}, Stamina: {Stamina}");

            if (conqueredPeaks.Count == 0)
            {
                sb.AppendLine("Peaks conquered: no peaks conquered");
            }
            else
            {
                sb.AppendLine($"Peaks conquered: {conqueredPeaks.Count}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
