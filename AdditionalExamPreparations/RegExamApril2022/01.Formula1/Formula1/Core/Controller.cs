using Formula1.Core.Contracts;
using Formula1.Models.Contracts;
using Formula1.Models.FromulaCars;
using Formula1.Models.Pilot;
using Formula1.Models.Race;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IPilot> pilotRepository;
        private readonly IRepository<IRace> raceRepository;
        private readonly IRepository<IFormulaOneCar> formulaOneCarRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
        }
        public string CreatePilot(string fullName)
        {
            IPilot pilot = pilotRepository.FindByName(fullName);

            if (pilot != null)
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }

            pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);

            return $"Pilot {fullName} is created.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car = formulaOneCarRepository.FindByName(model);

            if (car != null)
            {
                throw new InvalidOperationException($"Formula one car {model} is already created.");
            }

            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == nameof(Williams))
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }

            if (car != null)
            {
                formulaOneCarRepository.Add(car);
                return $"Car {type}, model {model} is created.";
            }
            else
            {
                throw new InvalidOperationException($"Formula one car type {type} is not valid.");
            }
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race != null)
            {
                throw new InvalidOperationException($"Race { raceName} is already created.");
            }

            race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);

            return $"Race {raceName} is created.";
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);

            if (pilot is null || pilot.Car != null)
            {
                throw new InvalidOperationException($"Pilot {pilotName} does not exist or has a car.");
            }

            IFormulaOneCar car = formulaOneCarRepository.FindByName(carModel);

            if (car is null)
            {
                throw new NullReferenceException($"Car {carModel} does not exist.");
            }

            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);

            return $"Pilot {pilotName } will drive a {car.GetType().Name} {carModel} car.";
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = raceRepository.FindByName(raceName);
            IPilot pilot = pilotRepository.FindByName(pilotFullName);

            if (race is null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }

            if (pilot is null || !pilot.CanRace || race.Pilots.Contains(pilot)) 
            {
                throw new InvalidOperationException($"Can not add pilot {pilotFullName} to the race.");
            }

            race.AddPilot(pilot);

            return $"Pilot {pilotFullName} is added to the {raceName} race.";
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race is null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }

            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than three participants.");
            }

            if (race.TookPlace)
            {
                throw new InvalidOperationException($"Can not execute race {raceName}.");
            }

            IPilot[] sortedPilots = race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps)).ToArray();

            IPilot winner = sortedPilots[0];
            IPilot runnerUp = sortedPilots[1];
            IPilot thirdPlace = sortedPilots[2];

            race.TookPlace = true;
            winner.WinRace();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {winner.FullName} wins the {race.RaceName} race.");
            sb.AppendLine($"Pilot {runnerUp.FullName} is second in the {race.RaceName} race.");
            sb.AppendLine($"Pilot {thirdPlace.FullName} is third in the {race.RaceName} race.");

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IRace race in raceRepository.Models.Where(r => r.TookPlace))
            {
               sb.AppendLine(race.RaceInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IPilot pilot in pilotRepository.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
