using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.SoldiersReport
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
        private List<Soldier> _soldiers = new List<Soldier>();

        public void Work()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Fill();
                ShowMainParameters();
            }
        }

        private void Fill()
        {
            SoldierCreator soldierCreator = new SoldierCreator();
            int countSoldiers = 50;

            for (int i = 0; i < countSoldiers; i++)
                _soldiers.Add(soldierCreator.Create());
        }

        private void ShowMainParameters()
        {
            var newSoldiers = _soldiers.Select(soldier => new
            {
                MainParameters = soldier.Name + " " + soldier.Rank
            });

            foreach (var soldier in newSoldiers)
                Console.WriteLine(soldier.MainParameters);
        }
    }

    class SoldierCreator
    {
        private List<string> _defaultName;
        private List<string> _defaultRank;
        private List<string> _defaultArmament;

        public SoldierCreator()
        {
            _defaultArmament = new List<string>()
            {
                new string("Максим"),
                new string("Никита"),
                new string("Дмитрий"),
                new string("Роман"),
                new string("Александр"),
                new string("Оскар"),
            };

            _defaultRank = new List<string>()
            {
                new string("Рядовой"),
                new string("Младший лейтенант"),
                new string("Старший лейтенант"),
                new string("Прапорщик"),
                new string("Ефрейтор"),
            };

            _defaultArmament = new List<string>()
            {
                new string("АК-47"),
                new string("Глок-18"),
                new string("Юсп"),
                new string("Дробовик"),
                new string("Снайперская винктовка"),
            };
        }

        public Soldier Create()
        {
            int maxTermOfMilitarySerive = 60;

            return new Soldier(_defaultName[UserUtils.GetRandomNumber(0, _defaultName.Count)],
                _defaultRank[UserUtils.GetRandomNumber(0, _defaultRank.Count)],
                _defaultArmament[UserUtils.GetRandomNumber(0, _defaultArmament.Count)],
                UserUtils.GetRandomNumber(0, maxTermOfMilitarySerive));
        }
    }

    class Soldier
    {
        public Soldier(string name, string rank, string armament, int termOfMilitarySerive)
        {
            Name = name;
            Rank = Rank;
            Armament = armament;
            TermOfMilitarySerive = termOfMilitarySerive;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }
        public string Armament { get; private set; }
        public int TermOfMilitarySerive { get; private set; }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}