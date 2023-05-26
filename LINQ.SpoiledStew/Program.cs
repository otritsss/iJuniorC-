using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.SpoiledStew
{
    class Program
    {
        static void Main()
        {
            Stock stock = new Stock();
            stock.Open();
        }
    }

    class Stock
    {
        private List<Stew> _stews = new List<Stew>();
        private int _currentYear = 2023;

        public void Open()
        {
            Fill();
            ShowStews("Просроченные банки", GetExpiredStews());

            Console.ReadKey();
        }

        private void Fill()
        {
            StewCreator stewCreator = new StewCreator();
            int countStews = 1000;

            for (int i = 0; i < countStews; i++)
                _stews.Add(stewCreator.Create());
        }

        private List<Stew> GetExpiredStews() =>
            _stews.Where(stew => stew.YearOfCreation <= _currentYear - stew.CountYearsShelfLife).ToList();

        private void ShowStews(string text, List<Stew> stews)
        {
            for (int i = 0; i < stews.Count; i++)
            {
                Console.Write($"Банка {i + 1} - ");
                stews[i].ShowInfo();
            }
        }
    }

    class Stew
    {
        public Stew(int yearOfCreation)
        {
            YearOfCreation = yearOfCreation;
        }

        public int CountYearsShelfLife { get; private set; } = 10;
        public int YearOfCreation { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Год производства: {YearOfCreation}");
        }
    }

    class StewCreator
    {
        public Stew Create()
        {
            int minYearOfCreation = 2000;
            int maxYearOfCreation = 2024;

            Stew stew = new Stew(UserUtils.GenerateRandomNumber(minYearOfCreation, maxYearOfCreation));

            return stew;
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}