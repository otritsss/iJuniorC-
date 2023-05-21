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
        private const string ShowTopByLevelCommand = "1";
        private const string ShowTopByForceCommand = "2";

        private List<Player> _players = new List<Player>();

        public void Work()
        {
            bool isWork = true;

            FillPlayers();

            while (isWork)
            {
                Console.WriteLine(
                    $"{ShowTopByLevelCommand} - показать ТОП-3 игроков по уровню\n{ShowTopByForceCommand} - показать ТОП-3 игроков по силе");
                string userInputCommand = Console.ReadLine();

                switch (userInputCommand)
                {
                    case ShowTopByLevelCommand:
                        ShowTopPlayers(GetTopPlayersByLevel());
                        break;

                    case ShowTopByForceCommand:
                        ShowTopPlayers(GetTopPlayersByForce());
                        break;

                    default:
                        Console.WriteLine("Вы ввели некорретное значение");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private List<Player> GetTopPlayersByLevel()
        {
            var topThreePlayers = _players.OrderByDescending(player => player.Level).Take(3).ToList();
            return topThreePlayers;
        }

        private List<Player> GetTopPlayersByForce()
        {
            var topThreePlayers = _players.OrderByDescending(player => player.Force).Take(3).ToList();
            return topThreePlayers;
        }

        private void FillPlayers()
        {
            PlayerCreator playerCreator = new PlayerCreator();
            int countPlayers = 10;

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

        private void ShowTopPlayers(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
                players[i].ShowInfo();
        }
    }

    class Player
    {
        public Player(string name, int level, int force)
        {
            Name = name;
            Level = level;
            Force = force;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Force { get; private set; }


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
                new string("ПуЛя_В_гЛаЗ_и_Ты_УнИТаЗ"),
                new string("Mr.ПеLьМЕshKa"),
                new string("СУ_-ха_-РИ_-к"),
                new string("казявкин"),
                new string("Кексуальная козявка"),
                new string("Па3итИФф"),
                new string("Tor4oK"),
                new string("Сильвестр в столовой"),
                new string("Ляськи_Масяськи"),
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