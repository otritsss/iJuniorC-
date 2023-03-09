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
        private int maxCountFish = 10;
        private List<Fish> _allFishes = new List<Fish>();

        public bool IsLife
        {
            get
            {
                if (_allFishes.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            private set { }
        }

        public void CreateDefaultFish()
        {
            FishCreator creator = new FishCreator();
            _allFishes = creator.Default();
        }

        public void AddFish(Fish addFish)
        {
            _allFishes.Add(addFish);
        }

        public void RemoveFish(int indexRemoveFish)
        {
            _allFishes.RemoveAt(indexRemoveFish);
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
        const string ShowInfoCommand = "1";
        const string AddFishCommad = "2";
        const string RemoveFishCommad = "3";

        private Aquarium _aquarium = new Aquarium();

        public void Work()
        {
            _aquarium.CreateDefaultFish();

            while (_aquarium.IsLife)
            {
                Console.WriteLine($"Введите номер команды:\n1 - Посмотреть показатели рыбок в аквариума" +
                    $"\n2 - Добавить рыбку в аквариум\n3 - Достать рыбку из аквариума");

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
                }
            }

            Console.WriteLine("Все рыбки погибли. Вы плохой администратор!");
        }

        private void AddFish()
        {
            FishCreator creator = new FishCreator();

            Console.WriteLine("Введите цвет рыбы: ");
            string inputColor = Console.ReadLine();
            Console.WriteLine("Введите возраст рыбы: ");
            int inputAge = Convert.ToInt32(Console.ReadLine());

            _aquarium.AddFish(creator.Create(inputColor, inputAge));
        }

        private void RemoveFish()
        {
            Console.WriteLine("Введите номер рыбки, которую хотите удалить: ");
            int inputRemoveNumber = Convert.ToInt32(Console.ReadLine());
            _aquarium.RemoveFish(inputRemoveNumber);
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
        public Fish Create(string inputColor, int inputAge)
        {
            Fish fish = new Fish(inputColor, inputAge);

            return fish;
        }

        public List<Fish> Default()
        {
            List<Fish> fishes = new List<Fish>()
            {
                new Fish("Кранный", 5),
                new Fish("Синий", 1),
                new Fish("Розовый", 2),
                new Fish("Пурпурный", 3),
                new Fish("Ораньжевый", 8),
                new Fish("Желтый", 5),
            };

            return fishes;
        }
    }
}
