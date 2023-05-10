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

        public void Open()
        {
            Fill();
        }

        private void Fill()
        {
            StewCreator stewCreator = new StewCreator();
            int countStews = 1000;

            for (int i = 0; i < countStews; i++)
                _stews.Add(stewCreator.Create());
        }
    }

    class Stew
    {
        public Stew(int yearOfCreation)
        {
            YearOfCreation = yearOfCreation;
        }

        public int ExpirationDate { get; private set; } = 10;
        public int YearOfCreation { get; private set; }
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