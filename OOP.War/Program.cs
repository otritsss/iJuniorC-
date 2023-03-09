using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP.War
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena battle = new Arena();
            battle.Open();
        }
    }

    class Arena
    {
        private Platoon _platoonRussia = new Platoon("Россия");
        private Platoon _platoonUsa = new Platoon("Америка");

        public void Open()
        {
            StartFight();
            PrintWinner();
        }

        private void StartFight()
        {
            int dayBattle = 1;

            while (_platoonRussia.CombatantsCount > 0 && _platoonUsa.CombatantsCount > 0)
            {
                Console.WriteLine($"{new string(' ', 25)} {dayBattle++}-й день битвы");
                _platoonRussia.ShowInfo();
                _platoonUsa.ShowInfo();

                _platoonRussia.Fight(_platoonUsa);
                _platoonUsa.Fight(_platoonRussia);

                _platoonRussia.RemoveDead();
                _platoonUsa.RemoveDead();

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void PrintWinner()
        {
            if (_platoonRussia.CombatantsCount == 0 && _platoonUsa.CombatantsCount == 0)
            {
                Console.WriteLine("Ничья");
            }
            else if (_platoonRussia.CombatantsCount == 0)
            {
                Console.WriteLine("Победила Америка!");
            }
            else
            {
                Console.WriteLine("Победила Россия!");
            }
        }
    }

    class Platoon
    {
        private static Random _random = new Random();
        private List<Combatant> _combatants = new List<Combatant>();

        public Platoon(string nameCountry)
        {
            NameCountry = nameCountry;
            CreateCombatants();
        }

        public string NameCountry { get; private set; }
        public int CombatantsCount { get; private set; }

        public int CountingCombatantOneGrade(string gradeName)
        {
            int countCombatantOneGrade = 0;

            foreach (var combatant in _combatants)
                if (combatant.GradeName == gradeName)
                    countCombatantOneGrade++;

            return countCombatantOneGrade;
        }

        public Combatant GetRandomFighters()
        {
            int randomEnemyNumber = _random.Next(_combatants.Count);
            return _combatants[randomEnemyNumber];
        }

        public void Fight(Platoon enemy)
        {
            for (int i = 0; i < _combatants.Count; i++)
            {
                Combatant enemyRandomFighter = enemy.GetRandomFighters();
                _combatants[i].Attack(enemyRandomFighter);
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{NameCountry}: В живых - {_combatants.Count} бойцов " +
                    $"| Снайперов - {CountingCombatantOneGrade("Снайпер")}, " +
                    $"Пулеметчиков - {CountingCombatantOneGrade("Пулеметчик")}, Мечников - {CountingCombatantOneGrade("Мечник")}");
        }

        public void RemoveDead()
        {
            for (int i = 0; i < _combatants.Count; i++)
                if (_combatants[i].Health <= 0)
                    _combatants.RemoveAt(i);

            CombatantsCount = _combatants.Count;
        }

        private void CreateCombatants()
        {
            int minCountCombatants = 5;
            int maxCountCombatants = 25;
            CombatantsCount = _random.Next(minCountCombatants, maxCountCombatants);

            for (int i = 0; i < CombatantsCount; i++)
            {
                Combatant[] tempCombatants = { new Sniper(), new MachineGunner(), new Swordsman() };
                int numberAddCombatant = _random.Next(tempCombatants.Length);

                _combatants.Add(tempCombatants[numberAddCombatant]);
            }
        }
    }

    abstract class Combatant
    {
        static protected Random Random = new Random();

        public Combatant()
        {
            int maxArmor = 20;
            Armor = Random.Next(maxArmor + 1);
            Health += Armor;
        }

        public int Health { get; protected set; } = 100;
        public int Damage { get; protected set; } = 20;
        public int Armor { get; protected set; }
        public string GradeName { get; protected set; }

        public void Attack(Combatant enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{GradeName} | Здоровье - {Health} . Урон - {Damage} . Броня - {Armor}");
        }
    }

    class Sniper : Combatant
    {
        public Sniper()
        {
            GradeName = "Снайпер";
            Damage = 120;
        }
    }

    class MachineGunner : Combatant
    {
        public MachineGunner() : base()
        {
            GradeName = "Пулеметчик";
            int attackSpeed = 20;
            Damage += attackSpeed;
        }
    }

    class Swordsman : Combatant
    {
        public Swordsman() : base()
        {
            GradeName = "Мечник";
            Damage = 100;
        }
    }
}

