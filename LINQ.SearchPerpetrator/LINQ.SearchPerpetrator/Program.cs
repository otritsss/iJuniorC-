using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.Auroservice
{
    class Program
    {
        static void Main()
        {
            Database database = new Database();
            database.Open();
        }
    }

    class Database
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public void Open()
        {
            bool isWork = true;
            FillCriminals();

            while (isWork)
            {
                int inputHeight;
                int inputWeight = 0;
                string inputNationality = null;

                Console.Write("Введите рост: ");

                if (int.TryParse(Console.ReadLine(), out inputHeight))
                {
                    Console.Write("Введите вес: ");

                    if (int.TryParse(Console.ReadLine(), out inputWeight))
                    {
                        Console.Write("Введите национальность: ");
                        inputNationality = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели некорректное значение");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели некорректное значение");
                }

                if (SearchCrimonals(inputHeight, inputWeight, inputNationality).Count != 0)
                    ShowInfoSearchCriminals(SearchCrimonals(inputHeight, inputWeight, inputNationality));
                else
                    Console.WriteLine("Не удалось найти преступников с такими параметрами");


                Console.ReadKey();
                Console.Clear();
            }
        }

        private List<Criminal> SearchCrimonals(int inputHeight, int inputWeight, string inputNationality)
        {
            List<Criminal> filtredCriminals;

            filtredCriminals = _criminals.Where(criminal => criminal.Height == inputHeight)
                .Where(criminal => criminal.Weight == inputWeight)
                .Where(criminal => criminal.Nationality == inputNationality)
                .Where(criminal => criminal.IsBeingInCustody == true)
                .ToList();

            return filtredCriminals;
        }

        private void ShowInfoSearchCriminals(List<Criminal> filtredCriminals)
        {
            foreach (var crimnal in filtredCriminals)
                crimnal.ShowInfo();
        }

        private Criminal AddCriminal()
        {
            CriminalCreator creator = new CriminalCreator();
            Criminal criminal = creator.Create();
            return criminal;
        }

        private void FillCriminals()
        {
            int countCriminals = 100000;

            for (int i = 0; i < countCriminals; i++)
                _criminals.Add(AddCriminal());
        }
    }

    class Criminal
    {
        public string Name { get; private set; }
        public string Nationality { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public bool IsBeingInCustody;

        public Criminal(string name, string nationality, int height, int weight, bool beingInCustody)
        {
            Name = name;
            Nationality = nationality;
            Height = height;
            Weight = weight;
            IsBeingInCustody = beingInCustody;
        }

        public void ShowInfo()
        {
            Console.WriteLine(
                $"Имя - {Name}, Национальность - {Nationality}, Рост - {Height}, Вес - {Weight}, Заключен под стражу? - {IsBeingInCustody}");
        }
    }

    class CriminalCreator
    {
        private List<string> _defaultNationalitys;
        private List<string> _defaultFirsNames;
        private List<string> _defaultFatherNames;
        private List<string> _defaultLastNames;

        public CriminalCreator()
        {
            _defaultNationalitys = new List<string>()
            {
                new string("Русский"),
                new string("Америконец"),
                new string("Украинец"),
                new string("Казах"),
                new string("Белорус"),
                new string("Филиппинец"),
                new string("Немец"),
            };

            _defaultFirsNames = new List<string>()
            {
                new string("Макс"),
                new string("Александр"),
                new string("Оскар"),
                new string("Никита"),
                new string("Марк"),
                new string("Евгений"),
            };

            _defaultFatherNames = new List<string>()
            {
                new string("Сергеевич"),
                new string("Алесандрович"),
                new string("Олегович"),
                new string("Романович"),
                new string("Василиевич"),
                new string("Василиевич"),
            };

            _defaultLastNames = new List<string>()
            {
                new string("Сергеев"),
                new string("Соколовский"),
                new string("Хартман"),
                new string("Гительман"),
                new string("Шабутдинов"),
                new string("Шабутдинов"),
                new string("Гришаков"),
            };
        }

        public Criminal Create()
        {
            string firstName = _defaultFirsNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string fathertName = _defaultFatherNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string lastName = _defaultLastNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string name = lastName + " " + firstName + " " + fathertName;

            string nationality = _defaultNationalitys[UserUtils.GenerateRandomNumber(0, _defaultNationalitys.Count)];
            int minHeight = 160;
            int maxHeight = 181;
            int height = UserUtils.GenerateRandomNumber(minHeight, maxHeight);
            int minWeight = 60;
            int maxWeight = 91;
            int weight = UserUtils.GenerateRandomNumber(minWeight, maxWeight);
            int optionsCount = 2;
            bool isBeingInCustody =
                true
                    ? UserUtils.GenerateRandomNumber(0, optionsCount) == 1
                    : UserUtils.GenerateRandomNumber(0, optionsCount) == 0;

            Criminal criminal = new Criminal(name, nationality, height, weight, isBeingInCustody);
            return criminal;
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}