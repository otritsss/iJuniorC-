using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 1. Каким способом мне сделать создание радномного кол-ва каждого класса в отряде (не могут же всех быть столько же)
///     1) Для рандомного создания каждого создать свой метод.
///     2) В одном методе
///     
/// Что придумал:
///     В каждом классе с помощью рандома задавать количество бойцов того или иного класса во взводе.
///     
///     14.02.2023
///     
///     27.02.2023: 
///     1. Вроде бы сделал создание бойцов, но нужно отрегулировать рандом, потому что у каждого бойца одного класса одинаковые значения
///     
/// 
/// Вопросы:
///     1. Как сделать так, чтобы метод в наследуемых класаах принимал разные аргументы. В одном одного бойца, а в другом уже массив бойцов? Я сделал через перегрузку.
///     2. Как сделать создание разных типов(классов) бойцов? Я сделал 
/// </summary>

namespace OOP.War
{
    class Program
    {
        static void Main(string[] args)
        {
            Battle battle = new Battle();
            battle.Work();
        }
    }

    class Battle
    {
        private Platoon _platoon = new Platoon();

        private Country _country = new Country();
        public void Work()
        {
            _country.CreatePlatoon();
        }
    }

    class Country
    {
        private Platoon _platoon = new Platoon();

        public void CreatePlatoon()
        {
            _platoon.CreateCombatants();
        }
    }

    class Platoon
    {
        static private Random _random = new Random();
        private List<Combatant> _combatants = new List<Combatant> { new Sniper(), new MachineGunner(), new GrenadeLauncher() };
        private int _combatantsCount;

        public void CreateCombatants()
        {
            int minCountCombatants = 50;
            int maxCountCombatants = 101;

            _combatantsCount = _random.Next(minCountCombatants, maxCountCombatants);

            for (int i = 0; i < _combatantsCount; i++)
            {
                Combatant[] tempCombatants = { new Sniper(), new MachineGunner(), new GrenadeLauncher() };
                int numberAddCombatant = _random.Next(tempCombatants.Length);

                _combatants.Add(tempCombatants[numberAddCombatant]);
                _combatants[i].ShowInfo();
            }
        }

        //public void ShowInfo(Combatant combatant)
        //{
        //    Console.WriteLine($"{combatant.Grade} | Здоровье - {combatant.Health} . Урон - {combatant.Health}");
        //}
    }

    abstract class Combatant
    {
        static protected Random Random = new Random();

        public int Count { get; protected set; }
        public int Health { get; protected set; } = 100;
        public int Damage { get; protected set; } = 20;
        public int Armor { get; protected set; }
        public bool IsLive { get; protected set; } = true;
        public string Grade { get; protected set; }


        public Combatant()
        {
            int maxArmor = 20;
            Armor = Random.Next(maxArmor);

            int minCountCombatant = 5;
            int maxCountCombatant = 25;
            Count = Random.Next(minCountCombatant, maxCountCombatant);
        }

        public virtual void Attack(Combatant[] combatants) { }

        public virtual void Attack(Combatant combatant)
        {
            combatant.TakeDamage(Damage);
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Grade} | Здоровье - {Health} . Урон - {Health}");
        }
    }

    class Sniper : Combatant
    {
        public Sniper()
        {
            Grade = "Снайпер";
            Damage = 120;
        }

        public override void Attack(Combatant combatant)
        {
            combatant.TakeDamage(Damage);
        }
    }

    class MachineGunner : Combatant
    {
        public MachineGunner() : base()
        {
            Grade = "Пулеметчик";
            double attackSpeed = 1.5;
            Damage *= (int)attackSpeed;
        }

        public override void Attack(Combatant combatant)
        {
            //double attackSpeed = 1.5;
            //Damage *= (int)attackSpeed;
            combatant.TakeDamage(Damage);
        }
    }


    // принимает массив бойцов нахолящиеся рядом и сносит им урон
    class GrenadeLauncher : Combatant
    {
        public GrenadeLauncher() : base()
        {
            Grade = "Гранатометчик";
        }

        public override void Attack(Combatant[] combatants)
        {

        }
    }
}
