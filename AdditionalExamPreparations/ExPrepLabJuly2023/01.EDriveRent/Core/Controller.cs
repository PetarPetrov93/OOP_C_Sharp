using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.FindById(drivingLicenseNumber) != null)
            {
                return $"{drivingLicenseNumber} is already registered in our platform.";
            }
            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return $"{firstName} {lastName} is registered successfully with DLN-{drivingLicenseNumber}";
        }
        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != "CargoVan" && vehicleType != "PassengerCar")
            {
                return $"{vehicleType} is not accessible in our platform.";
            }

            if (vehicles.FindById(licensePlateNumber) != null)
            {
                return $"{licensePlateNumber} belongs to another vehicle.";
            }

            IVehicle vehicle = null;

            if (vehicleType == "CargoVan")
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else if (vehicleType == "PassengerCar")
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);
            return $"{brand} {model} is uploaded successfully with LPN-{licensePlateNumber}";
        }


        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            foreach (IRoute route in routes.GetAll())
            {
                if (route.StartPoint == startPoint && route.EndPoint == endPoint && route.Length == length)
                {
                    return $"{startPoint}/{endPoint} - {length} km is already added in our platform.";
                }
                else if (route.StartPoint == startPoint && route.EndPoint == endPoint && route.Length < length)
                {
                    return $"{startPoint}/{endPoint} shorter route is already added in our platform.";
                }
            }
            IRoute newRoute = new Route(startPoint, endPoint, length, routes.GetAll().Count + 1);

            foreach (IRoute route in routes.GetAll())
            {
                if (route.StartPoint == newRoute.StartPoint && route.EndPoint == newRoute.EndPoint && route.Length > newRoute.Length)
                {
                    route.LockRoute();
                }
            }
            routes.AddModel(newRoute);

            return $"{startPoint}/{endPoint} - {length} km is unlocked in our platform.";
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser driver = users.GetAll().FirstOrDefault(u => u.DrivingLicenseNumber == drivingLicenseNumber);
            IVehicle vehicle = vehicles.GetAll().FirstOrDefault(v => v.LicensePlateNumber == licensePlateNumber);
            IRoute route = routes.GetAll().FirstOrDefault(r => r.RouteId == int.Parse(routeId));

            if (driver.IsBlocked)
            {
                return $"User {drivingLicenseNumber} is blocked in the platform! Trip is not allowed.";
            }
            if (vehicle.IsDamaged)
            {
                return $"Vehicle {licensePlateNumber} is damaged! Trip is not allowed.";
            }
            if (route.IsLocked)
            {
                return $"Route {routeId} is locked! Trip is not allowed.";
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                driver.DecreaseRating();
            }
            else
            {
                driver.IncreaseRating();
            }
            return vehicle.ToString();
        }

        public string RepairVehicles(int count)
        {
            int repairedVehicles = 0;
            foreach (IVehicle vehicle in vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v => v.Brand).ThenBy(v => v.Model).Take(count))
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
                repairedVehicles++;
            }
            return $"{repairedVehicles} vehicles are successfully repaired!";
        }

        public string UsersReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (IUser user in users.GetAll().OrderByDescending(u => u.Rating).ThenBy(u => u.LastName).ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
