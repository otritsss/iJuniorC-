using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.CombinedTroops
{
    class Program
    {
        static void Main()
        {
        }
    }

    class Army
    {
        private List<Soldier> FillSquad(List<Soldier> squad)
        {
            SoldierCreator soldierCreator = new SoldierCreator();

            int minCountSoldiers = 15;
            int maxCountSoldiers = 50;

            for (int i = 0; i < UserUtils.GetRandomNumber(minCountSoldiers, maxCountSoldiers); i++)
                squad.Add(soldierCreator.Create());

            return squad;
        }
    }

    class Soldier
    {
        public Soldier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class SoldierCreator
    {
        private List<string> _defaultSurnames;

        public SoldierCreator()
        {
            _defaultSurnames = new List<string>()
            {
                new string("Батяшин"),
                new string("Краснов"),
                new string("Шолмов"),
                new string("Максимов"),
                new string("Хартман"),
                new string("Соколовский"),
                new string("Бешкеков"),
                new string("Борисов"),
            };
        }

        public Soldier Create() => new Soldier(_defaultSurnames[UserUtils.GetRandomNumber(0, _defaultSurnames.Count)]);
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}