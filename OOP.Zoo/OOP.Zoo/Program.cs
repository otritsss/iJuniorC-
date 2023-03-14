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
        private static Random _random = new Random();
        private List<Animal> _animals = new List<Animal>();

        public Aviary()
        {
            int maxCountAnimals = 10;
            CountAnimals = _random.Next(maxCountAnimals);
        }

        public int CountAnimals { get; private set; }

        public void AddAnimals(Animal animal)
        {
            _animals.Add(animal);
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                Console.WriteLine("");
            }
        }
    }


    class AviaryCreator
    {
        private static Random _random = new Random();

        public Aviary CreateAviary(Animal animal, List<Animal> animals)
        {
            Aviary aviary = new Aviary();

            for (int i = 0; i < aviary.CountAnimals; i++)
                aviary.AddAnimals(animals[i]);

            return aviary;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }

    abstract class Animal
    {
        private static Random _random = new Random();

        public Animal()
        {
            int genderCount = 2;
            Gender = (Gender) _random.Next(genderCount);
        }

        public string Name { get; protected set; }
        public Gender Gender { get; protected set; }

        public abstract void MakeSound();

        public void ShowInfo()
        {
            Console.WriteLine($"Пол - {Gender}");
        }
    }

    class Cat : Animal
    {
        public Cat() : base()
        {
            Name = "Cats";
        }

        public override void MakeSound()
        {
            Console.WriteLine("Мяу");
        }
    }

    class Dog : Animal
    {
        public Dog() : base()
        {
            Name = "Dogs";
        }

        public override void MakeSound()
        {
            Console.WriteLine("Гав");
        }
    }

    class Cow : Animal
    {
        public Cow() : base()
        {
            Name = "Cows";
        }

        public override void MakeSound()
        {
            Console.WriteLine("Му");
        }
    }

    class Pig : Animal
    {
        public Pig() : base()
        {
            Name = "Pigs";
        }

        public override void MakeSound()
        {
            Console.WriteLine("Хрю");
        }
    }
}