using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
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
        public int Endurance
        {
            get { return endurance; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Endurance should be between 0 and 100.");
                }
                endurance = value;
            }
        }
        public int Sprint
        {
            get { return sprint; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Sprint should be between 0 and 100.");
                }
                sprint = value;
            }
        }
        public int Dribble
        {
            get { return dribble; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Dribble should be between 0 and 100.");
                }
                dribble = value;
            }
        }
        public int Passing
        {
            get { return passing; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Passing should be between 0 and 100.");
                }
                passing = value;
            }
        }
        public int Shooting
        {
            get { return shooting; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Shooting should be between 0 and 100.");
                }
                shooting = value;
            }
        }

        // this is probably not required based on the problem description and will be done in the Team
        //public int OverallSkillLevel => CalculateOverallSkillLevel();

        //private int CalculateOverallSkillLevel() => (Endurance + Sprint + Dribble + Passing + Shooting) / 5; //that way it should onlu return the int value e.g. 83.2 = 83
        
    }
}
