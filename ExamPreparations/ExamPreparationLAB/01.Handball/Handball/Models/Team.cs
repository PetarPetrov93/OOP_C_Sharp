using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            players = new List<IPlayer>();
        }

        public string Name 
        { 
            get => name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            } 
        }

        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (players.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(players.Average(p => p.Rating), 2);
                }
            }
        }
        

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();

        public void Draw()
        {
            PointsEarned++;
            players.FirstOrDefault(p => p.GetType().Name == "Goalkeeper").IncreaseRating();
        }

        public void Lose()
        {
            foreach (var player in players)
            {
                player.DecreaseRating();
            }
        }

        public void Win()
        {
            PointsEarned += 3;
            foreach (var player in players)
            {
                player.IncreaseRating();
            }
        }
        public void SignContract(IPlayer player) => players.Add(player);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            if (!players.Any())
            {
                sb.AppendLine("--Players: none");
            }
            else
            {
                ICollection<string> names = new List<string>();
                foreach (var player in players)
                {
                    names.Add(player.Name);
                }
                sb.AppendLine($"--Players: {string.Join(", ", names)}");
            }
            
            return sb.ToString().TrimEnd();
        }
    }
}
