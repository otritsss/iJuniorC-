using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train
{
    class Program
    {
        static void Main()
        {
            TrainRide trainRide = new TrainRide();
            trainRide.Work();
        }
    }

    class TrainRide
    {
        private Train _train = new Train();

        public void Work()
        {
            const string CreateRouteCommand = "1";
            const string SellTicketsCommand = "2";
            const string CreateTrainCommand = "3";
            const string SendTrainCommand = "4";

            bool isOpenProgramm = true;

            while (isOpenProgramm)
            {
                if (_train.IsCreateRoute)
                    _train.ShowInfo();

                Console.Write($"Здравствуйте, Вы попали в составлние плана поезда.\n{CreateRouteCommand} - Создать маршрут\n{SellTicketsCommand} - Продать билеты\n{CreateTrainCommand} - Сформировать поезд\n{SendTrainCommand} - Отправить поезд\nВведите команду: ");
                string inputCommand = Console.ReadLine();

                switch (inputCommand)
                {
                    case CreateRouteCommand:
                        _train.CreateRoute();
                        break;
                    case SellTicketsCommand:
                        _train.GeneratePassenger();
                        break;
                    case CreateTrainCommand:
                        _train.CreateTrain();
                        break;
                    case SendTrainCommand:
                        _train.SendTrain();
                        break;
                    default:
                        Console.WriteLine("Введите корректное значение..");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Train
    {
        private Passenger _passengers = new Passenger(0);
        private Wagon _wagon = new Wagon();

        public string BoardingPoint { get; private set; }
        public string DropOffPoint { get; private set; }
        public bool IsCreateRoute { get; private set; }
        private bool _isCreateTrain;
        private bool _isGeneratePassenger;

        private int _wagonsCount;

        public void GeneratePassenger()
        {
            if (!IsCreateRoute)
            {
                Console.WriteLine("Маршрут еще не выбран");
            }
            else if (!_isGeneratePassenger)
            {
                _passengers = new Passenger();
                _isGeneratePassenger = true;
                Console.WriteLine($"На рейс купили {_passengers.Count} билета");
            }
            else
            {
                Console.WriteLine("Сначала отправьте текущий поезд...");
            }
        }

        public void CreateTrain()
        {
            if (_passengers.Count == 0)
            {
                Console.WriteLine($"Автовокзал еще не продал билеты пассажирам");
            }
            else if (_isCreateTrain)
            {
                Console.WriteLine("Куда тыкаешь, вагоны уже выделены!");
            }
            else
            {
                _wagonsCount = (int)Math.Ceiling(_passengers.Count / _wagon.CountFreePlace);
                Console.WriteLine($"На этот рейс выделено {_wagonsCount} вагона, вместимостью по {_wagon.CountFreePlace} мест");
                _isCreateTrain = true;
            }
        }

        public void CreateRoute()
        {
            if (!IsCreateRoute)
            {
                Console.Write("Введите точку отправления: ");
                string inputBoardingPoint = Console.ReadLine();
                Console.Write("Введите точку прибытия: ");
                string inputDropOffPoint = Console.ReadLine();

                BoardingPoint = inputBoardingPoint;
                DropOffPoint = inputDropOffPoint;

                IsCreateRoute = true;
            }
            else
            {
                Console.WriteLine("Сначала отправьте текущий поезд...");
            }
        }

        public void SendTrain()
        {
            if (!_isCreateTrain)
            {
                Console.WriteLine($"Сначала составьте поезд");
            }
            else if (_isCreateTrain)
            {
                Console.WriteLine($"Поезд отправлен..");

                IsCreateRoute = false;
                _isCreateTrain = false;
                _isGeneratePassenger = false;
            }
        }

        public void ShowInfo()
        {
            if (!_isCreateTrain)
                Console.WriteLine($"Рейс: {BoardingPoint} - {DropOffPoint} | {_passengers.Count} пассажиров | Выделено {0} вагонов\n");
            else
                Console.WriteLine($"Рейс: {BoardingPoint} - {DropOffPoint} | {_passengers.Count} пассажиров | Выделено {_wagonsCount}\n");
        }
    }

    class Wagon
    {
        public double CountFreePlace { get; private set; }

        public Wagon()
        {
            Random random = new Random();
            int minCountFreePlace = 10;
            int maxCountFreePlace = 20;
            CountFreePlace = random.Next(minCountFreePlace, maxCountFreePlace);
        }
    }

    class Passenger
    {
        public int Count { get; private set; }

        public Passenger()
        {
            Random random = new Random();
            int minCountPassenger = 20;
            int maxCountPassenger = 101;
            Count = random.Next(minCountPassenger, maxCountPassenger);
        }

        public Passenger(int count)
        {
            Count = count;
        }
    }
}