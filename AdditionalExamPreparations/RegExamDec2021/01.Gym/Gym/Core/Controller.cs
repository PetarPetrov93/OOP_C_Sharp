using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IEquipment> equipment;
        private readonly ICollection<IGym> gyms;
        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != nameof(WeightliftingGym) && gymType != nameof(BoxingGym))
            {
                throw new InvalidOperationException("Invalid gym type.");
            }

            IGym gym = null;

            if (gymType == nameof(WeightliftingGym))
            {
                gym = new WeightliftingGym(gymName);
            }
            else if (gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
            }

            gyms.Add(gym);

            return $"Successfully added {gymType}.";
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != nameof(Kettlebell) && equipmentType != nameof(BoxingGloves))
            {
                throw new InvalidOperationException("Invalid equipment type.");
            }

            IEquipment equipment = null;

            if (equipmentType == nameof(Kettlebell))
            {
                equipment = new Kettlebell();
            }
            else if (equipmentType == nameof(BoxingGloves))
            {
                equipment = new BoxingGloves();
            }

            this.equipment.Add(equipment);

            return $"Successfully added {equipmentType}.";
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipmentToAdd = this.equipment.FindByType(equipmentType);

            if (equipmentToAdd == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }

            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);

            gym.AddEquipment(equipmentToAdd);
            this.equipment.Remove(equipmentToAdd);

            return $"Successfully added {equipmentType} to {gymName}.";
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if (athleteType != nameof(Boxer) && athleteType != nameof(Weightlifter))
            {
                throw new InvalidOperationException("Invalid athlete type.");
            }

            IAthlete athlete = null;
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);

            if (athleteType == nameof(Boxer))
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == nameof(Weightlifter))
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }

            if (athlete is Boxer && gym.GetType().Name != nameof(BoxingGym))
            {
                throw new InvalidOperationException("The gym is not appropriate.");
            }
            if (athlete is Weightlifter && gym.GetType().Name != nameof(WeightliftingGym))
            {
                throw new InvalidOperationException("The gym is not appropriate.");
            }

            gym.AddAthlete(athlete);

            return $"Successfully added {athleteType} to {gymName}.";
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);

            gym.Exercise();

            return $"Exercise athletes: {gym.Athletes.Count}.";
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);

            return $"The total weight of the equipment in the gym {gymName} is {gym.EquipmentWeight:f2} grams.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IGym gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
