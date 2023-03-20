using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

/// <summary>
///
/// 1. Сделать команды:
///     1.1 Обслужить очередной автомобиль.
///         1.1.1 [ИндексАвто] Автомобиль - поломки - ..список..
///         1.1.2. Поломка обойдется в такую-то стоимость - [сумма]
///         1.1.3 Починить автомобиль? Да | Нет
///             1.1.3.1 Если починить не удалось, то накладывается штраф, а далее пункт - {2}
///             1.1.3.2 Если починить удалось, то делается перехрод в меню (или продумать переход ко вкладке покупки запчастей)
///     1.2 Заполнить склад запчастями.
///         1.2.1 Метод BuyDetails, в котором список дефолтных деталей, выбираешь индекс детали и ее кол-во, если денег недостаточно, то не можешь купить деталь.
///     1.3 Выход
///
/// 2. Сделать банкротсво автосервиса:
///     2.1 Если баланс магазина меньше -10000, то признается банкротом и игра завершается
///
/// 3. Сделать обслуживание автомобилей:
///     1.1 Заеха
/// 
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
        private WarehouseDetails _warehouseDetails = new WarehouseDetails();

        public void Work()
        {
            ServiceCar();
            ;
            Console.ReadKey();
        }

        public void ServiceCar()
        {
            Car car = new Car(_warehouseDetails.DetailsCreator.GetDefaultDetails());
        }
    }

    class WarehouseDetails
    {
        public DetailsCreator DetailsCreator = new DetailsCreator();
        private Dictionary<Detail, int> _details = new Dictionary<Detail, int>();

        public WarehouseDetails()
        {
            Fill();
        }

        public void Fill()
        {
            int maxDetailsCount = 10;

            for (int i = 0; i < DetailsCreator.GetDefaultDetails().Count; i++)
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

        public Detail GetDefaultDetail(int indexDetail)
        {
            return _defaultDetails[indexDetail];
        }

        public List<Detail> GetDefaultDetails()
        {
            List<Detail> returnDetails = new List<Detail>();

            foreach (var detail in _defaultDetails)
                returnDetails.Add(detail);

            return returnDetails;
        }
    }

    class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details)
        {
            _details = new List<Detail>(details);
            BrokeDetails();
        }

        public int Money { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine("----------------------");

            for (int i = 0; i < _details.Count; i++)
                Console.WriteLine($"{_details[i].IsBroke}");
        }

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