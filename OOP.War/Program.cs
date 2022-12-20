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
        Platoon platoon = new Platoon();
        public void Work()
        {
            platoon.CreateCombatants();
        }
    }

    class Country
    {

    }

    class Platoon
    {
        static private Random _random;

        private Combatant[] _combatants = { new Sniper(), new MachineGunner(), new GrenadeLauncher(), new Doctor() };

        public void CreateCombatants()
        {
            for (int i = 0; i < _combatants[0].Count; i++)
            {

            }
        }
    }

    class Combatant
    {
        static protected Random Random;

        public int Count { get; protected set; }
        public int Health { get; protected set; } = 100;
        public int Damage { get; protected set; } = 20;
        public int Armor { get; protected set; }
        public bool IsLive { get; protected set; } = true;


        public Combatant()
        {
            int maxArmor = 20;
            //Armor = Random.Next(maxArmor);

            int minCountCombatant = 5;
            int maxCountCombatant = 25;
            Count = Random.Next(minCountCombatant, maxCountCombatant);

        }

        public virtual void Attack(Combatant combatant)
        {
            combatant.TakeDamage(Damage);
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    class Sniper : Combatant
    {
        public Sniper()
        {

        }

        public override void Attack(Combatant combatant)
        {
            Damage = 120;
            combatant.TakeDamage(Damage);
        }
    }

    class MachineGunner : Combatant
    {
        public override void Attack(Combatant combatant)
        {
            double attackSpeed = 1.5;
            Damage *= (int)attackSpeed;
            combatant.TakeDamage(Damage);
        }
    }

    class GrenadeLauncher : Combatant
    {
        public override void Attack(Combatant combatant)
        {

        }
    }

    class Doctor : Combatant
    {

    }
}
