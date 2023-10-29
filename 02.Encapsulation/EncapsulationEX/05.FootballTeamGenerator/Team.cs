using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            players = new List<Player>();
            Name = name;
        }

        public string Name 
        {
            get { return name; }
            private set 
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("A name should not be empty.");
                }
                name = value; 
            }
        }

        public int Rating => TeamRating();

        public int NumberOfPlayers => players.Count;

        private int TeamRating()
        { 
            if (players.Count > 0)
            {
                double teamRating = 0;
                foreach (Player player in players)
                {
                    teamRating += AverageRating(player);
                }

                return (int)Math.Round(teamRating / NumberOfPlayers);
            }
            return 0;
        }

        public void AddPlayer(Player playerToAdd) => players.Add(playerToAdd);

        public void RemovePlayer(string name)
        {
            if (players.Any(x => x.Name == name))
            {
                players.RemoveAll(x => x.Name == name);
            }
            else
            {
                throw new Exception($"Player {name} is not in {Name} team.");
            }
        }
        private static double AverageRating(Player player)
        {
            double averagePlayerRating = (player.Endurance + player.Shooting + player.Sprint + player.Dribble + player.Passing)*1.0 / 5;


            return averagePlayerRating;
        }
        
    }
}
