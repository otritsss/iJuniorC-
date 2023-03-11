using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace OOP.Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Administrator administrator = new Administrator();
            administrator.Work();
        }
    }

    class Aquarium
    {
        private List<Fish> _allFishes = new List<Fish>();
        private int _maxCountFish = 10;
        private int _maxAgeFish = 10;

        public bool IsLife
        {
            get
            {
                if (_allFishes.Count > 0)
                    return true;
                else
                    return false;
            }
            private set { }
        }

        public void CreateDefaultFish()
        {
            FishCreator creator = new FishCreator();
            _allFishes = creator.DefaultCreateFishes();
        }

        public void AddYearFishes()
        {
            foreach (var fish in _allFishes)
                fish.AddYear();
        }

        public void AddFish(Fish addFish)
        {
            if (_allFishes.Count < _maxCountFish)
                _allFishes.Add(addFish);
            else
                Console.WriteLine("Аквариум переполнен");
        }

        public void RemoveFish(int indexRemoveFish)
        {
            _allFishes.RemoveAt(indexRemoveFish);
        }

        public void RemoveDiedFishes()
        {
            for (int i = 0; i < _allFishes.Count; i++)
                if (_allFishes[i].Age >= _maxAgeFish)
                    RemoveFish(i--);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"В аквариуме сейчас находится {_allFishes.Count} рыбок.\nПодробнее:\n");

            for (int i = 0; i < _allFishes.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _allFishes[i].ShowInfo();
            }
        }
    }

    class Administrator
    {
        private const string ShowInfoCommand = "1";
        private const string AddFishCommad = "2";
        private const string RemoveFishCommad = "3";

        private Aquarium _aquarium = new Aquarium();

        public void Work()
        {
            _aquarium.CreateDefaultFish();

            while (_aquarium.IsLife)
            {
                Console.WriteLine($"Введите номер команды:\n{ShowInfoCommand} - Посмотреть показатели рыбок в аквариума" +
                    $"\n{AddFishCommad} - Добавить рыбку в аквариум\n{RemoveFishCommad} - Достать рыбку из аквариума");

                string inputCommand = Console.ReadLine();

                switch (inputCommand)
                {
                    case ShowInfoCommand:
                        _aquarium.ShowInfo();
                        break;

                    case AddFishCommad:
                        AddFish();
                        break;

                    case RemoveFishCommad:
                        RemoveFish();
                        break;

                    default:
                        Console.WriteLine("Вы ввели некорректную команду");
                        break;
                }

                _aquarium.AddYearFishes();
                _aquarium.RemoveDiedFishes();
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Все рыбки погибли. Вы плохой администратор!");
        }

        private void AddFish()
        {
            FishCreator creator = new FishCreator();

            Console.WriteLine("Введите цвет рыбы: ");
            string inputColor = Console.ReadLine();
            Console.WriteLine("Введите возраст рыбы: ");

            if (int.TryParse(Console.ReadLine(), out int inputAge))
                _aquarium.AddFish(creator.CreateNewFish(inputColor, inputAge));
            else
                Console.WriteLine("Вы ввели некорректное значение");
        }

        private void RemoveFish()
        {
            Console.WriteLine("Введите номер рыбки, которую хотите удалить: ");

            if (int.TryParse(Console.ReadLine(), out int inputRemoveNumber))
                _aquarium.RemoveFish(inputRemoveNumber - 1);
            else
                Console.WriteLine("Вы ввели некорректнеое значение");
        }
    }

    class Fish
    {
        public Fish(string color, int years)
        {
            Age = years;
            Color = color;
        }

        public int Age { get; private set; }
        public int MaxAge { get; private set; } = 25;
        public string Color { get; private set; }

        public void AddYear()
        {
            Age++;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Цвет - {Color} | Возраст - {Age}\n");
        }
    }

    class FishCreator
    {
        public Fish CreateNewFish(string inputColor, int inputAge)
        {
            Fish fish = new Fish(inputColor, inputAge);
            return fish;
        }

        public List<Fish> CreateDefaultFishes()
        {
            List<Fish> fishes = new List<Fish>()
            {
                new Fish("Красный", 9),
                new Fish("Синий", 10),
                new Fish("Розовый", 9),
                new Fish("Пурпурный", 3),
                new Fish("Ораньжевый", 8),
                new Fish("Желтый", 5),
            };

            return fishes;
        }
    }
}