using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fight
{
    class Program
    {
        static void Main()
        {
            Fight fight = new Fight();
            fight.Battle();
        }
    }

    class Fight
    {
        Fighter[] fighters = { new Tanjiro(100, 20), new Zenitsu(100, 20) };
        public void Battle()
        {
            for (int i = 0; i < fighters.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                fighters[i].ShowInfo();
            }

            Console.Write("Введите номер бойца левого ринга: ");
            Fighter leftFighter = fighters[Convert.ToInt32(Console.ReadLine()) - 1];
            Console.Write("Введите номер бойца правого ринга: ");
            Fighter rightFighter = fighters[Convert.ToInt32(Console.ReadLine()) - 1];

            int round = 1;

            while (leftFighter.Health > 0 && rightFighter.Health > 0)
            {
                Console.WriteLine($"{round++}-е столкновение");

                leftFighter.TakeDamage(rightFighter.Damage, rightFighter);
                rightFighter.TakeDamage(leftFighter.Damage, leftFighter);

                leftFighter.ShowInfo();
                rightFighter.ShowInfo();
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    class Fighter
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        protected int TrueHitChance = 7;
        protected bool IsHit;
        protected string Name;
        public Fighter(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void TakeDamage(int damage, Fighter fighter)
        {
            Random random = new Random();
            int maxHitChance = 10;
            int hitChance = random.Next(maxHitChance);

            if (hitChance > TrueHitChance)
            {
                Console.WriteLine($"{fighter.Name} промахнулся");
                IsHit = false;
            }
            else
            {
                IsHit = true;
                Health -= damage;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} | Здоровье - {Health} | Урон - {Damage}");
        }
    }

    class Tanjiro : Fighter
    {

        private int _trueDanceOfFireChance = 10;
        public Tanjiro(int health, int damage) : base("Танзиро", health, damage)
        {
            Random random = new Random();
            int maxChance = 10;
            int danceOfFireChance = random.Next(maxChance);

            if (danceOfFireChance < _trueDanceOfFireChance)
            {
                DanceOfFire(ref damage);
            }
        }

        private void DanceOfFire(ref int damage)
        {
            int danceOfFire = 5;
            damage += danceOfFire;
        }
    }

    class Zenitsu : Fighter
    {
        private int _trueSleep = 3;
        public Zenitsu(int health, int damage) : base("Зетицу", health, damage)
        {
            int anger = 0;

            if (IsHit)
            {
                anger++;
            }

            if (anger == _trueSleep)
            {
                Sleep(damage);
                anger = 0;
            }
        }

        private void Sleep(int damage)
        {
            double sleep = 2;
            damage *= (int)sleep;
        }
    }

    class Kanao
    {

    }

    class Nezuko
    {

    }

    class Inosuke
    {

    }
}