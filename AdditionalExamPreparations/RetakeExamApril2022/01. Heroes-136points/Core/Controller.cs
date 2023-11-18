﻿using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHero> heroes;
        private readonly IRepository<IWeapon> weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            if (type != nameof(Knight) && type != nameof(Barbarian))
            {
                throw new InvalidOperationException($"Invalid hero type.");
            }

            IHero hero = null;

            if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return $"Successfully added Sir {name} to the collection.";
            }
            else
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return $"Successfully added Barbarian {name} to the collection.";
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            IWeapon weapon = null;

            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
            }
            else
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }

            weapons.Add(weapon);
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            if (hero is null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }

            if (weapon is null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well - armed.");
            }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);

            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string StartBattle()
        {
            Map map = new Map();
           return map.Fight(heroes.Models.Where(h => h.Weapon != null).ToList()); // should work

        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IHero hero in heroes.Models.OrderBy(h => h.GetType().Name).ThenByDescending(h => h.Health).ThenBy(h => h.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.AppendLine($"--Weapon: {(hero.Weapon != null ? hero.Weapon.Name : "Unarmed")}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
