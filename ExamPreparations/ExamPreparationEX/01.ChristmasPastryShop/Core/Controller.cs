using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths = new BoothRepository();

        public string AddBooth(int capacity)
        {
            IBooth booth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(booth);
            return string.Format(OutputMessages.NewBoothAdded, booth.BoothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != "Gingerbread" && delicacyTypeName != "Stolen")
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.Models.Any(d => d.Name == delicacyName)) 
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy = null;
            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.AddModel(delicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != "Hibernation" && cocktailTypeName != "MulledWine")
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            if (booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }
            ICocktail cocktail = null;
            if (cocktailTypeName == "Hibernation")
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == "MulledWine")
            {
                cocktail = new  MulledWine(cocktailName, size);
            }
            booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.AddModel(cocktail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            IBooth booth = booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople).OrderBy(b => b.Capacity).ThenByDescending(b => b.BoothId).FirstOrDefault();
            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] orderSpecs = order.Split("/", StringSplitOptions.RemoveEmptyEntries);
            string itemTypeName = orderSpecs[0];
            string itemName = orderSpecs[1];
            int orderedPiecesCount = int.Parse(orderSpecs[2]);
            string size = string.Empty;
            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine") // Should be the types of cocktails, however it could just be Cocktail, KEEP IN MIND THAT THIS COULD BE A SUBJECT TO CHANGE!
            {
                size = orderSpecs[3];
            }

            if (itemTypeName != "Hibernation" && itemTypeName != "MulledWine" && itemTypeName != "Gingerbread" && itemTypeName != "Stolen")
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            // MIGHT NEED TO CHECK THIS STATEMENT!
            if (!(booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.Models.Any(d => d.Name == itemName)) && !(booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.Models.Any(c => c.Name == itemName)))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine")
            {
                if (!booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.Models.Any(c => c.GetType().Name == itemTypeName && c.Name == itemName && c.Size == size))
                {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
                booths.Models.FirstOrDefault(b => b.BoothId == boothId).UpdateCurrentBill(orderedPiecesCount * booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName).Price);
            }

            if (itemTypeName == "Gingerbread" || itemTypeName == "Stolen")
            {
                if (!booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.Models.Any(d => d.GetType().Name == itemTypeName && d.Name == itemName))
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
                booths.Models.FirstOrDefault(b => b.BoothId == boothId).UpdateCurrentBill(orderedPiecesCount * booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.Models.FirstOrDefault(c => c.Name == itemName).Price);
            }

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, orderedPiecesCount, itemName);
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double bill = booth.CurrentBill;
            booth.Charge();
            booth.ChangeStatus();

            return $"Bill {bill:f2} lv{Environment.NewLine}Booth {boothId} is now available!";
            
        }

        public string BoothReport(int boothId) => booths.Models.FirstOrDefault(b => b.BoothId == boothId).ToString();



    }
}
