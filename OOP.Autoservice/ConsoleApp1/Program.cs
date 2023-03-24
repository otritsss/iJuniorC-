using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

/// <summary>
///
/// 1. Сделать команды:                                                                                                                                             --
///     1.1 Обслужить очередной автомобиль.                                                                                                                         --
///         1.1.1 [ИндексАвто] Автомобиль - поломки - ..список..                                                                                                    --  YES
///         1.1.2. Поломка обойдется в такую-то стоимость - [сумма]                                                                                                 --  YES
///         1.1.3 Починить автомобиль? Да | Нет                                                                                                                     --
///             1.1.3.1 Если починить не удалось, то накладывается штраф, а далее пункт - {2}                                                                       --
///             1.1.3.2 Если починить удалось, то делается перехрод в меню (или продумать переход ко вкладке покупки запчастей)                                     --
///     1.2 Заполнить склад запчастями.                                                                                                                             --
///         1.2.1 Метод BuyDetails, в котором список дефолтных деталей, выбираешь индекс детали и ее кол-во, если денег недостаточно, то не можешь купить деталь.   --
///     1.3 Выход                                                                                                                                                   --  YES
///
/// 2. Сделать банкротсво автосервиса:                                                                                                                              --
///     2.1 Если баланс магазина меньше -10000, то признается банкротом и игра завершается                                                                          --
///
/// Вопрос:
/// 1. По поводу передачи
/// Ответ:
/// 1.
///     1.1 Сделал в классе Detail метод Repair                                                                                                                      --  YES
///     1.2 Сделал передачу копии листа через метод                                                                                                                  --  YES
///     1.3 Автосервис методом ВыявлениеСлмоанныхДеталей выясняет какие детали сломаны и вызывает метод Detail.Repair                                                --  YES
/// 
/// </summary>

namespace OOP.Auroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            Autoservise autoservise = new Autoservise();
            autoservise.Work();

            Console.ReadKey();
        }
    }

    static class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }

    class Autoservise
    {
        private const int RepairService = 7000;
        private const int Forfeit = 3000;
        private const string ServiceCarCommand = "1";
        private const string BuyDetails = "2";
        private const string ShowInfoWarehouseDetails = "3";
        private const string Exit = "4";

        private WarehouseDetails _warehouseDetails = new WarehouseDetails();
        private int _balanceMoney;
        private int _indexRepairCar = 1;

        public void Work()
        {
            bool isWork = true;
            int minBalance = -100000;

            while (isWork || _balanceMoney > minBalance)
            {
                Console.WriteLine(
                    $"Баланс автосервиса: {_balanceMoney}" +
                    $"\n{ServiceCarCommand} - Обслужить автомобиль\n{BuyDetails} - Закупиться деталями\n{ShowInfoWarehouseDetails} - Посмотреть количество деталей на складе\n{Exit} - Выйти");

                switch (Console.ReadLine())
                {
                    case ServiceCarCommand:
                        ServiceCar();
                        break;

                    case BuyDetails:
                        FillDetails();
                        break;

                    case ShowInfoWarehouseDetails:
                        _warehouseDetails.ShowInfo();
                        break;

                    case Exit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели некорретное значнеие");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            if (_balanceMoney > minBalance)
                Console.WriteLine("Вы признаны банкротом!");
            else
                Console.WriteLine("Всего доброго!");
        }

        private void FillDetails()
        {
            Console.Write("Точно введите название детали: ");
            string inputDetailTtile = Console.ReadLine();
            Console.Write("Введите количество деталей, которое хотите добавить: ");
            string inputCountDetails = Console.ReadLine();

            if (int.TryParse(inputCountDetails, out int countDetails) == false)
            {
                Console.WriteLine("Введите корректное значение");
            }
            else
            {
                _warehouseDetails.AddDetail(inputDetailTtile, countDetails);
            }
        }

        private void ServiceCar()
        {
            Console.WriteLine($"\n{_indexRepairCar++} Автомобиль за сессию");

            Car car = new Car(_warehouseDetails.DetailsCreator.GetCopyDefaultDetails());
            List<Detail> brokeDetails = new List<Detail>();
            int priceRepair = 0;

            DetectBrokeDetails(car, brokeDetails);
            ShowBrokeDetailCar(brokeDetails);

            if (_warehouseDetails.TryAvailabilityDetails(brokeDetails))
            {
                RepairCarDetail(brokeDetails);
                RepairEstimate(brokeDetails, ref priceRepair);
            }

            Console.WriteLine($"Стоимость ремонта = {priceRepair}");
            _balanceMoney += priceRepair;
        }

        private List<Detail> DetectBrokeDetails(Car car, List<Detail> brokeDetails)
        {
            List<Detail> detailsCar = car.GetCopyDetails();

            foreach (var detail in detailsCar)
                if (detail.IsBroke == true)
                    brokeDetails.Add(detail);

            return brokeDetails;
        }

        private int RepairEstimate(List<Detail> brokeDeatails, ref int priceRepair)
        {
            foreach (var detail in brokeDeatails)
                priceRepair += detail.Price;

            return priceRepair;
        }

        private void RepairCarDetail(List<Detail> brokeDetails)
        {
            foreach (var detail in brokeDetails)
            {
                detail.Repair();
                _warehouseDetails.RemoveDetail(detail);
            }
        }

        private void ShowBrokeDetailCar(List<Detail> brokeDeatails)
        {
            foreach (var detail in brokeDeatails)
                Console.WriteLine($"{detail.Title} - сломана");
        }
    }

    class WarehouseDetails
    {
        public DetailsCreator DetailsCreator = new DetailsCreator();
        private Dictionary<Detail, int> _details = new Dictionary<Detail, int>();
        private int _countDetails;

        public WarehouseDetails()
        {
            Fill();
        }

        public bool TryAvailabilityDetails(List<Detail> brokeDetails)
        {
            foreach (var detail in brokeDetails)
            {
                if (_details[detail] > 0)
                    return true;
                else
                    return false;
            }

            return false;
        }

        public void AddDetail(string detailInputTitle, int countDetails)
        {
            foreach (var detail in _details.Keys)
            {
                if (detail.Title == detailInputTitle)
                    _details[detail] += countDetails;
                else
                    Console.WriteLine("Вы ввели название некорректно");
            }
        }


        public void RemoveDetail(Detail detailInput)
        {
            if (_details.ContainsKey(detailInput) && _details[detailInput] > 0)
                _details[detailInput]--;
            else
                Console.WriteLine($"{detailInput.Title} Недостаточно на складе");


            // foreach (var detail in _details.Keys)
            // {
            //     if (detail == detailInput && _details[detail] > 0)
            //         _details[detail]--;
            //     else
            //         Console.WriteLine($"{detail.Title} Недостаточно на складе");
            // }
        }

        public void ShowInfo()
        {
            Console.WriteLine("Наличие на складе");

            foreach (var detail in _details)
                Console.WriteLine($"{detail.Key.Title} Количество - {detail.Value}");
        }

        public void Fill(ref int balanceAutosrvice)
        {
            int countAddDetail = 3;

            foreach (var detail in _details.Keys)
            {
                if (_details[detail] == 0)
                {
                    _details[detail] += countAddDetail;
                    balanceAutosrvice -= detail.Price;
                }
            }
        }

        private void Fill()
        {
            int maxDetailsCount = 10;

            for (int i = 0; i < DetailsCreator.GetCopyDefaultDetails().Count; i++)
                _details.Add(DetailsCreator.GetDefaultDetail(i), UserUtils.GenerateRandomNumber(1, maxDetailsCount));
        }
    }

    class Detail
    {
        public Detail(string title, int price)
        {
            Title = title;
            Price = price;
            IsBroke = false;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
        public bool IsBroke { get; private set; }

        public void MadeBroken()
        {
            IsBroke = true;
        }

        public void Repair()
        {
            if (IsBroke == false)
                Console.WriteLine("Деталь не сломана");
            else
                IsBroke = false;
        }
    }

    class DetailsCreator
    {
        private List<Detail> _defaultDetails = new List<Detail>()
        {
            new Detail("Коробка", 55000),
            new Detail("Подвеска", 40000),
            new Detail("Двигатель", 100000),
            new Detail("Кондиционер", 2500),
            new Detail("Ремень генератора", 500),
            new Detail("Колесо", 1500)
        };

        public Detail GetDefaultDetail(int indexDetail) =>
            _defaultDetails[indexDetail];

        public List<Detail> GetCopyDefaultDetails() =>
            new List<Detail>(_defaultDetails);
    }

    class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details)
        {
            _details = new List<Detail>(details);
            BrokeDetails();
        }

        public List<Detail> GetCopyDetails() =>
            new List<Detail>(_details);

        private void BrokeDetails()
        {
            for (int i = 0; i < _details.Count; i++)
            {
                int indexDetail = UserUtils.GenerateRandomNumber(0, _details.Count);
                _details[indexDetail].MadeBroken();
            }
        }
    }
}