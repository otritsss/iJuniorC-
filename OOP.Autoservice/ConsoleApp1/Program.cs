using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

/// <summar.y>
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
/// </summ.ary>

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

        public static void PrintTextDeffierentColors(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Autoservise
    {
        private const int RepairService = 7000;
        private const int Forfeit = 3000;
        private const string ServiceCarCommand = "1";
        private const string BuyComponents = "2";
        private const string ShowInfoWarehouseComponents = "3";
        private const string Exit = "4";

        private WarehouseComponents _warehouseComponents = new WarehouseComponents();
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
                    $"\n{ServiceCarCommand} - Обслужить автомобиль\n{BuyComponents} - Закупиться деталями\n{ShowInfoWarehouseComponents} - Посмотреть количество деталей на складе\n{Exit} - Выйти");

                switch (Console.ReadLine())
                {
                    case ServiceCarCommand:
                        ServiceCar();
                        break;

                    case BuyComponents:
                        FillComponents();
                        break;

                    case ShowInfoWarehouseComponents:
                        _warehouseComponents.ShowInfo();
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

        private void FillComponents()
        {
            _warehouseComponents.ShowInfo();

            Console.Write("\nТочно введите название детали: ");
            string inputComponentTtile = Console.ReadLine();
            Console.Write("Введите количество деталей, которое хотите добавить: ");
            string inputCountComponents = Console.ReadLine();

            if (int.TryParse(inputCountComponents, out int countComponents) == false &&
                _warehouseComponents.FindTitleComponentAvailability(inputComponentTtile))
                Console.WriteLine("Введите корректное значение/название");
            else if (_balanceMoney <= _warehouseComponents.GetPriceFillComponents(inputComponentTtile, countComponents))
                Console.WriteLine("На Ващем счету недостаточно средств");
            else
                _balanceMoney -=
                    _warehouseComponents.FillBuyComponents(inputComponentTtile, countComponents, _balanceMoney);
        }

        private void ServiceCar()
        {
            Console.WriteLine($"\n{_indexRepairCar++} Автомобиль за сессию");

            Car car = new Car(_warehouseComponents.ComponentsCreator.GetCopyDefaultComponents());
            List<Component> brokeComponents = new List<Component>();
            int priceRepair = 0;

            DetectBrokeComponents(car, brokeComponents);
            ShowBrokeComponentCar(brokeComponents);

            if (_warehouseComponents.FindComponentsAvailability(brokeComponents))
            {
                RepairCarComponent(brokeComponents);
                priceRepair = CountRepairEstimate(brokeComponents, priceRepair);

                Console.WriteLine($"Стоимость ремонта = {priceRepair += RepairService}");
                _balanceMoney += priceRepair;
            }
            else
            {
                UserUtils.PrintTextDeffierentColors(ConsoleColor.Red, "Недостаточно деталей на складе");
                _balanceMoney -= Forfeit;
            }
        }

        private List<Component> DetectBrokeComponents(Car car, List<Component> brokeComponents)
        {
            List<Component> componentsCar = car.GetCopyComponent();

            foreach (var component in componentsCar)
                if (component.IsBroke == true)
                    brokeComponents.Add(component);

            return brokeComponents;
        }

        private int CountRepairEstimate(List<Component> brokeComponents, int priceRepair)
        {
            foreach (var component in brokeComponents)
                priceRepair += component.Price;

            return priceRepair;
        }

        private void RepairCarComponent(List<Component> brokeComponents)
        {
            foreach (var component in brokeComponents)
            {
                component.Repair();
                _warehouseComponents.RemoveComponent(component);
            }
        }

        private void ShowBrokeComponentCar(List<Component> brokeComponents)
        {
            foreach (var component in brokeComponents)
                UserUtils.PrintTextDeffierentColors(ConsoleColor.Green, $"{component.Title} - сломано");
        }
    }

    class WarehouseComponents
    {
        public ComponentsCreator ComponentsCreator = new ComponentsCreator();
        private Dictionary<Component, int> _components = new Dictionary<Component, int>();
        private int _countComponents;

        public WarehouseComponents()
        {
            FillDefaultComponents();
        }

        public bool FindComponentsAvailability(List<Component> brokeComponents)
        {
            foreach (var component in brokeComponents)
                if (_components[component] <= 0)
                    return false;

            return true;
        }

        public bool FindTitleComponentAvailability(string componentInputTitle)
        {
            foreach (var component in _components.Keys)
                if (component.Title == componentInputTitle)
                    return true;

            return false;
        }

        public int GetPriceFillComponents(string componentInputTitle, int countComponents)
        {
            int priceFillComponents = 0;

            foreach (var component in _components.Keys)
                if (component.Title == componentInputTitle)
                    priceFillComponents += component.Price;

            return priceFillComponents;
        }

        public int FillBuyComponents(string componentInputTitle, int countComponents, int balance)
        {
            int priceFillComponents = 0;

            foreach (var component in _components.Keys)
            {
                if (component.Title == componentInputTitle && balance >= component.Price * countComponents)
                {
                    _components[component] += countComponents;
                    priceFillComponents += component.Price;
                }
            }

            return priceFillComponents;
        }

        public void RemoveComponent(Component componentInput)
        {
            _components[componentInput]--;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Наличие на складе");

            foreach (var component in _components)
                Console.WriteLine(
                    $"{component.Key.Title} Количество - {component.Value} | Цена - {component.Key.Price}");
        }

        private void FillDefaultComponents()
        {
            int maxComponentsCount = 1;

            for (int i = 0; i < ComponentsCreator.GetCopyDefaultComponents().Count; i++)
                _components.Add(ComponentsCreator.GetDefaultComponent(i),
                    UserUtils.GenerateRandomNumber(1, maxComponentsCount));
        }
    }

    class Component
    {
        public Component(string title, int price)
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

    class ComponentsCreator
    {
        private List<Component> _defaultComponents = new List<Component>()
        {
            new Component("Коробка", 55000),
            new Component("Подвеска", 40000),
            new Component("Двигатель", 100000),
            new Component("Кондиционер", 2500),
            new Component("Ремень генератора", 500),
            new Component("Колесо", 1500)
        };

        public Component GetDefaultComponent(int indexComponent) =>
            _defaultComponents[indexComponent];

        public List<Component> GetCopyDefaultComponents() =>
            new List<Component>(_defaultComponents);
    }

    class Car
    {
        private List<Component> _components;

        public Car(List<Component> components)
        {
            _components = new List<Component>(components);
            BrokeComponents();
        }

        public List<Component> GetCopyComponent() =>
            new List<Component>(_components);

        private void BrokeComponents()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                int indexComponent = UserUtils.GenerateRandomNumber(0, _components.Count);
                _components[indexComponent].MadeBroken();
            }
        }
    }
}