using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
///     1. Создание бойцов правильно сделано? Или нужно думать его создание в конструкторе
///     2. Где сделать 'IsLive = false'?
///     
///     1) Сделать в конструкторе Platoon - YES
///     2) IsLive можно убрать. Сделать метод 'Удаление мертвых' и вызывать его в конце каждой итерации
///     3) Удалить класс 'Country' - YES
///     4) Сделать метод вывода характеристики в методе Platoon - YES
///     5) Нужно ли мне свойство CountComb.. Могу сделать проверку выживших в классе Platoon
///     5) Нужно как-то передать список врагов (Возможно через метод). Ментор написал про IReadOnlyCollection
/// 
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
        private Platoon _platoonRussia = new Platoon("Россия");
        private Platoon _platoonUsa = new Platoon("Америка");

        public void Work()
        {
            while (_platoonRussia.CombatantsCount > 0 || _platoonUsa.CombatantsCount > 0)
            {
                Console.WriteLine($"{new string(' ', 25)} 1-й день битвы");
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
    }

    class Platoon
    {
        const string GrenadeLauncherName = "Гранатометчик";

        static private Random _random = new Random();
        private List<Combatant> _combatants = new List<Combatant>();


        public Platoon(string nameCountry)
        {
            NameCountry = nameCountry;

            int minCountCombatants = 5;
            int maxCountCombatants = 25;

            CombatantsCount = _random.Next(minCountCombatants, maxCountCombatants);

            for (int i = 0; i < CombatantsCount; i++)
            {
                Combatant[] tempCombatants = { new Sniper(), new MachineGunner(), new GrenadeLauncher() };
                int numberAddCombatant = _random.Next(tempCombatants.Length);

                _combatants.Add(tempCombatants[numberAddCombatant]);
            }
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

        public List<Combatant> ReturnRandomFighters(Combatant combatant)
        {
            List<Combatant> returnFighters = new List<Combatant>();
            int enemyNumber = _random.Next(_combatants.Count);

            if (combatant.GradeName != GrenadeLauncherName)
            {
                returnFighters.Add(_combatants[enemyNumber]);
                return returnFighters;
            }
            else
            {
                for (int i = 0; i < combatant.CountEnemyTrapped; i++)
                {
                    returnFighters.Add(_combatants[enemyNumber++]);
                }

                return returnFighters;
            }
        }

        public void Fight(Platoon enemy)
        {
            for (int i = 0; i < _combatants.Count; i++)
            {
                _combatants[i].Attack(enemy.ReturnRandomFighters(_combatants[i]));
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{NameCountry}: В живых - {_combatants.Count} бойцов " +
                    $"| Снайперов - {CountingCombatantOneGrade("Снайпер")}, " +
                    $"Пулеметчиков - {CountingCombatantOneGrade("Пулеметчик")}, Гранатомётчиков - {CountingCombatantOneGrade("Гранатометчик")}");
        }

        public void RemoveDead()
        {
            for (int i = 0; i < _combatants.Count; i++)
                if (_combatants[i].Health <= 0)
                    _combatants.RemoveAt(i);
        }
    }

    abstract class Combatant
    {
        static protected Random Random = new Random();

        public Combatant()
        {
            int maxArmor = 20;
            Armor = Random.Next(maxArmor);
        }

        public int CountEnemyTrapped { get; protected set; }
        public int Count { get; protected set; }
        public int Health { get; protected set; } = 100;
        public int Damage { get; protected set; } = 20;
        public int Armor { get; protected set; }
        public string GradeName { get; protected set; }

        public virtual void Attack(List<Combatant> enemys)
        {
            for (int i = 0; i < enemys.Count; i++)
                enemys[i].TakeDamage(Damage);
        }

        public virtual void TakeDamage(int damage)
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

    class GrenadeLauncher : Combatant
    {
        public GrenadeLauncher() : base()
        {
            GradeName = "Гранатометчик";
            Damage = 100;
            CountEnemyTrapped = 3;
        }

        public override void Attack(List<Combatant> enemys)
        {
            int enemyNumber = Random.Next(enemys.Count);

            for (int i = 0; i < CountEnemyTrapped; i++)
                enemys[enemyNumber++].TakeDamage(Damage);
        }
    }
}
