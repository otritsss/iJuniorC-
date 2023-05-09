using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.Hospital
{
    class Program
    {
        static void Main()
        {
            Server server = new Server();
            server.Work();
        }
    }

    class Server
    {
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                FillPlayers();
                ShowInfo();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void FillPlayers()
        {
            PlayerCreator playerCreator = new PlayerCreator();
            int countPlayers = 50;

            for (int i = 0; i < countPlayers; i++)
            {
                Player addPlayer = playerCreator.Create();

                if (_players.Any(player => player.Name.StartsWith(addPlayer.Name)))
                    FixRepeatNamePlayer(addPlayer);

                _players.Add(addPlayer);
            }
        }

        private void FixRepeatNamePlayer(Player getPlayer)
        {
            int maxRepeatCount = _players.Where(player => player.Name.StartsWith(getPlayer.Name))
                .Select(player =>
                    int.TryParse(player.Name.Substring(getPlayer.Name.Length), out int numberLastPlayer)
                        ? numberLastPlayer
                        : 0).Max();

            getPlayer.FixRepeatName(maxRepeatCount);
        }

        private void ShowInfo()
        {
            foreach (var player in _players)
                player.ShowInfo();
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Force { get; private set; }

        public Player(string name, int level, int force)
        {
            Name = name;
            Level = level;
            Force = force;
        }

        public void FixRepeatName(int maxRepeatCount)
        {
            Name += (maxRepeatCount + 1).ToString();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name} | Уровень - {Level} | Сила {Force}");
        }
    }

    class PlayerCreator
    {
        private List<string> _defaultNames;

        public PlayerCreator()
        {
            _defaultNames = new List<string>()
            {
                new string("pro100 женек"),
                // new string("pro100 женек"),
                // new string("ПуЛя_В_гЛаЗ_и_Ты_УнИТаЗ"),
                // new string("Mr.ПеLьМЕshKa"),
                // new string("СУ_-ха_-РИ_-к"),
                // new string("казявкин"),
                // new string("Кексуальная козявка"),
                // new string("Па3итИФф"),
                // new string("Tor4oK"),
                // new string("Сильвестр в столовой"),
                // new string("Ляськи_Масяськи"),
            };
        }

        public Player Create()
        {
            int maxValue = 100;
            string name = _defaultNames[UserUtils.GenerateRandomNumber(0, _defaultNames.Count)];
            int Level = UserUtils.GenerateRandomNumber(0, maxValue);
            int Force = UserUtils.GenerateRandomNumber(0, maxValue);

            Player player = new Player(name, Level, Force);
            return player;
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}