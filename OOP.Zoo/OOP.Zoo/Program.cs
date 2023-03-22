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
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private const string CatsAviaryCommand = "1";
        private const string DogsAviaryCommand = "2";
        private const string CowsAviaryCommand = "3";
        private const string PigsAviaryCommand = "4";
        private const string ExitCommand = "5";

        private List<Aviary> _aviarys = new List<Aviary>();

        public void Work()
        {
            bool isWorkZoo = true;

            while (isWorkZoo)
            {
                CreateAviarys();

                Console.WriteLine(
                    $"Выберите к какому вольеру подойти:\n{CatsAviaryCommand} - Кошки\n{DogsAviaryCommand} - Собаки\n" +
                    $"{CowsAviaryCommand} - Коровы\n{PigsAviaryCommand} - Свиньи\n{ExitCommand} - Выход");

                string inputCommand = Console.ReadLine();
                int numberAviary = GetConvertNumber(inputCommand);

                if (inputCommand == ExitCommand)
                    isWorkZoo = false;
                else if (numberAviary <= _aviarys.Count)
                    _aviarys[numberAviary - 1].ShowInfo();
                else
                    Console.WriteLine("Вы ввели некорректное значение..");

                Console.ReadKey();
                Console.Clear();
            }
        }

        private int GetConvertNumber(string inputCommand)
        {
            if (int.TryParse(inputCommand, out int numberAviary))
                return numberAviary;
            else
                Console.WriteLine("Такого вольера нет");

            return 0;
        }

        private void CreateAviarys()
        {
            AviaryCreator aviaryCreator = new AviaryCreator();
            Animal[] tempAnimals = {new Cat(), new Dog(), new Cow(), new Pig()};

            for (int i = 0; i < tempAnimals.Length; i++)
                _aviarys.Add(aviaryCreator.Create(tempAnimals[i]));
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Нахоидтся {_animals.Count} животных");

            for (int i = 0; i < _animals.Count; i++)
            {
                Console.Write($"{i + 1} ");
                _animals[i].ShowInfo();
            }
        }
    }

    class AviaryCreator
    {
        private static Random _random = new Random();

        public Aviary Create(Animal animal)
        {
            int maxCountAnimals = 10;
            int countAnimals = _random.Next(maxCountAnimals);

            AnimalCreator animalCreator = new AnimalCreator();
            Aviary aviary = new Aviary();

            for (int i = 0; i < countAnimals; i++)
                aviary.AddAnimal(animalCreator.Create(animal));

            return aviary;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }

    class AnimalCreator
    {
        public Animal Create(Animal animal)
        {
            switch (animal)
            {
                case Cat cat:
                    return new Cat();

                case Dog dog:
                    return new Dog();

                case Cow cow:
                    return new Cow();

                case Pig pig:
                    return new Pig();

                default:
                    return animal;
            }
        }
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

        public void ShowInfo()
        {
            Console.Write($"{Name}. Пол - {Gender} | ");
            MakeSound();
        }

        protected abstract void MakeSound();
    }

    class Cat : Animal
    {
        public Cat() : base()
        {
            Name = "Cat";
        }

        protected override void MakeSound()
        {
            Console.Write("Издает звук - Мяу\n");
        }
    }

    class Dog : Animal
    {
        public Dog() : base()
        {
            Name = "Dogs";
        }

        protected override void MakeSound()
        {
            Console.Write("Издает звук - Гав\n");
        }
    }

    class Cow : Animal
    {
        public Cow() : base()
        {
            Name = "Cows";
        }

        protected override void MakeSound()
        {
            Console.Write("Издает звук - Му\n");
        }
    }

    class Pig : Animal
    {
        public Pig() : base()
        {
            Name = "Pigs";
        }

        protected override void MakeSound()
        {
            Console.Write("Издает звук - Хрю\n");
        }
    }
}