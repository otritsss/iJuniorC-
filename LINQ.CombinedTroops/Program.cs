using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.CombinedTroops
{
    class Program
    {
        static void Main()
        {
            Army army = new Army();
            army.Work();
        }
    }

    class Army
    {
        private List<Soldier> _squadOne = new List<Soldier>();
        private List<Soldier> _squadTwo = new List<Soldier>();

        public void Work()
        {
            bool isWork = true;

            _squadOne = FillSquad(_squadOne);
            _squadTwo = FillSquad(_squadTwo);

            while (isWork)
            {
                ShowInfo();

                Console.ReadKey();
                Console.Clear();

                TransferSoldiers();
            }
        }

        private void TransferSoldiers()
        {
            char neededSymbol = 'Б';

            var soldiers = _squadOne.Where(soldier => soldier.Surname.StartsWith(neededSymbol)).ToList();

            _squadTwo = _squadTwo.Union(soldiers).ToList();
            _squadOne = _squadOne.Except(soldiers).ToList();
        }

        private void ShowInfo()

        {
            foreach (var soldier in _squadOne)
                soldier.ShowInfo();

            int positionX = 30;
            int positionY = 0;

            foreach (var soldier in _squadTwo)
            {
                Console.SetCursorPosition(positionX, positionY++);
                soldier.ShowInfo();
            }
        }

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
        public Soldier(string surname)
        {
            Surname = surname;
        }

        public string Surname { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine(Surname);
        }
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