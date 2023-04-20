using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.Amnesty
{
    class Program
    {
        static void Main()
        {
            Prision prision = new Prision();
            prision.Open();
        }
    }

    class Prision
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public void Open()
        {
            FillCriminals();
            ShowInfo();

            Console.WriteLine("Нажмите на любую клавищу, чтобы удалить ненужных перступников");
            Console.ReadKey();

            RemoveCriminals();
            ShowInfo();
        }

        private void RemoveCriminals()
        {
            _criminals.RemoveAll(criminal => criminal.CrimeName == "Антиправительственное");
        }

        private Criminal AddCriminal()
        {
            CriminalCreator creator = new CriminalCreator();
            Criminal criminal = creator.Create();
            return criminal;
        }

        private void FillCriminals()
        {
            int countCriminals = 1000;

            for (int i = 0; i < countCriminals; i++)
                _criminals.Add(AddCriminal());
        }

        private void ShowInfo()
        {
            Console.WriteLine($"Количество преступников - {_criminals.Count}");

            foreach (var criminal in _criminals)
                criminal.ShowInfo();
        }
    }

    class Criminal
    {
        public string Name { get; private set; }
        public string CrimeName { get; private set; }

        public Criminal(string name, string crimeName)
        {
            Name = name;
            CrimeName = crimeName;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Фио - {Name} | Преступление - {CrimeName}");
        }
    }

    class CriminalCreator
    {
        private List<string> _defaultFirsNames;
        private List<string> _defaultFatherNames;
        private List<string> _defaultLastNames;
        private List<string> _defaultCrimeNames;

        public CriminalCreator()
        {
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

            _defaultCrimeNames = new List<string>()
            {
                new string("Перевел бабушку через дорогу"),
                new string("Украл жвачку"),
                new string("Антиправительственное"),
                new string("Проспал школу"),
                new string("Перешел по пешеходному переходу"),
                new string("Съел морковь"),
            };
        }

        public Criminal Create()
        {
            string firstName = _defaultFirsNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string fathertName = _defaultFatherNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string lastName = _defaultLastNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string name = lastName + " " + firstName + " " + fathertName;

            string crimeName = _defaultCrimeNames[UserUtils.GenerateRandomNumber(0, _defaultCrimeNames.Count)];

            Criminal criminal = new Criminal(name, crimeName);
            return criminal;
        }
    }

    static class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(maxValue, maxValue);
    }
}