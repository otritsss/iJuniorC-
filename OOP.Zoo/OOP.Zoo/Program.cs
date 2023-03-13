using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Administrator
    {
        public void Work()
        {
            Console.WriteLine($"Выберите к какому вольеру подойти:");
        }
    }

    class Zoo
    {
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public void ShowInfo()
        {
        }
    }

    abstract class Animal
    {
        public enum Gender
        {
            Male,
            Female
        }

        public abstract void MakeSound();
    }

    class AviaryCreator
    {
        private static Random _random = new Random();

        public void CreateAviary(Animal animal, List<Animal> animals)
        {
            Aviary aviary = new Aviary();

            int maxCountAnimals = 10;
            int countAnimals = _random.Next(maxCountAnimals);

            for (int i = 0; i < countAnimals; i++)
            {
                
            }
        }
    }

    class Cat : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Мяу");
        }
    }

    class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Гав");
        }
    }

    class Cow : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Му");
        }
    }

    class Pig : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Хрю");
        }
    }
}