using System;
using System.Collections.Generic;

namespace OOP.Auroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            Autoservise autoservise = new Autoservise();
            autoservise.Work();
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
        private const string ServiceCarCommand = "1";
        private const string BuyComponents = "2";
        private const string ShowInfoWarehouseComponents = "3";
        private const string Exit = "4";

        private WarehouseComponents _warehouseComponents = new WarehouseComponents();
        private readonly int _repairService = 7000;
        private readonly int _forfeit = 3000;
        private int _balanceMoney;
        private int _indexRepairCar = 1;

        public void Work()
        {
            bool isWork = true;
            int minBalance = -10000;

            while (isWork && _balanceMoney > minBalance)
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

            if (_balanceMoney <= minBalance)
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
                _warehouseComponents.TryFindTitleComponentAvailability(inputComponentTtile))
                Console.WriteLine("Введите корректное значение/название");
            else if (_balanceMoney <= _warehouseComponents.GetPriceFillComponents(inputComponentTtile, countComponents))
                Console.WriteLine("На Ващем счету недостаточно средств");
            else
                _balanceMoney -= _warehouseComponents.FillBuyComponents(inputComponentTtile, countComponents);
        }

        private void ServiceCar()
        {
            Console.WriteLine($"\n{_indexRepairCar++} Автомобиль за сессию");

            ComponentsCreator componentsCreator = new ComponentsCreator();
            var componentsCar = _warehouseComponents.GetComponentsCreator().GetCopyDefaultComponents();
            Car car = new Car(componentsCar);
            List<Component> brokeComponents = new List<Component>();
            int priceRepair = 0;

            brokeComponents = DetectBrokeComponents(car, brokeComponents);
            ShowBrokeComponentCar(brokeComponents);

            if (_warehouseComponents.TryFindComponentsAvailability(brokeComponents))
            {
                RepairCarComponent(brokeComponents);
                priceRepair = CountRepairEstimate(brokeComponents, priceRepair);

                Console.WriteLine($"Стоимость ремонта = {priceRepair += _repairService}");
                _balanceMoney += priceRepair;
            }
            else
            {
                UserUtils.PrintTextDeffierentColors(ConsoleColor.Red,
                    $"Недостаточно деталей на складе\nВы выплатили штраф - {_forfeit}");
                _balanceMoney -= _forfeit;
            }

            foreach (var component in componentsCar)
                component.Repair();
        }

        private List<Component> DetectBrokeComponents(Car car, List<Component> brokeComponents)
        {
            List<Component> tempComponents = new List<Component>();
            List<Component> componentsCar = car.GetCopyComponent();

            brokeComponents = tempComponents;

            foreach (var component in componentsCar)
                if (component.IsBroken == true)
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
        private ComponentsCreator _componentsCreator = new ComponentsCreator();
        private List<Stack> _componentStacks = new List<Stack>();
        private int _countComponents;

        public WarehouseComponents()
        {
            FillDefaultComponents();
        }

        public ComponentsCreator GetComponentsCreator() =>
            _componentsCreator;

        public bool TryFindComponentsAvailability(List<Component> brokeComponents)
        {
            foreach (var brokeComponent in brokeComponents)
            {
                foreach (var component in _componentStacks)
                {
                    if (component.Get() == brokeComponent)
                        if (component.GetCount() <= 0)
                            return false;
                }
            }

            return true;
        }

        public bool TryFindTitleComponentAvailability(string componentInputTitle)
        {
            foreach (var component in _componentStacks)
                if (component.Get().Title == componentInputTitle)
                    return true;

            return false;
        }

        public int GetPriceFillComponents(string componentInputTitle, int countComponents)
        {
            int priceFillComponents = 0;

            foreach (var component in _componentStacks)
                if (component.Get().Title == componentInputTitle)
                    priceFillComponents += component.Get().Price;

            return priceFillComponents;
        }

        public int FillBuyComponents(string componentInputTitle, int countComponents)
        {
            int priceFillComponents = 0;

            foreach (var component in _componentStacks)
            {
                if (component.Get().Title == componentInputTitle)
                {
                    component.AddCount(countComponents);
                    priceFillComponents += component.Get().Price;
                }
            }

            return priceFillComponents;
        }

        public void RemoveComponent(Component componentInput)
        {
            foreach (var component in _componentStacks)
                if (component.Get() == componentInput)
                    component.ReduceCount();
        }

        public void ShowInfo()
        {
            Console.WriteLine("Наличие на складе");

            foreach (var component in _componentStacks)
                component.ShowInfo();
        }

        private void FillDefaultComponents()
        {
            int maxComponentsCount = 5;

            for (int i = 0; i < _componentsCreator.GetCopyDefaultComponents().Count; i++)
                _componentStacks.Add(new Stack(_componentsCreator.GetDefaultComponent(i),
                    UserUtils.GenerateRandomNumber(0, maxComponentsCount)));
        }
    }

    class Component
    {
        public Component(string title, int price)
        {
            Title = title;
            Price = price;
            IsBroken = false;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }

        public void MadeBroken()
        {
            IsBroken = true;
        }

        public void Repair()
        {
            IsBroken = false;
        }
    }

    class ComponentsCreator
    {
        private List<Component> _defaultComponents;

        public ComponentsCreator()
        {
            _defaultComponents = new List<Component>()
            {
                new Component("Коробка", 55000),
                new Component("Подвеска", 40000),
                new Component("Двигатель", 100000),
                new Component("Кондиционер", 2500),
                new Component("Ремень генератора", 500),
                new Component("Колесо", 1500)
            };
        }

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
            _components = components;
            BreakAComponent();
        }

        public List<Component> GetCopyComponent() =>
            new List<Component>(_components);

        private void BreakAComponent()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                int indexComponent = UserUtils.GenerateRandomNumber(0, _components.Count);
                _components[indexComponent].MadeBroken();
            }
        }
    }

    class Stack
    {
        private Component _component;
        private int _countComponents;

        public Stack(Component component, int count)
        {
            _component = component;
            _countComponents = count;
        }

        public int ReduceCount() => _countComponents--;

        public int AddCount(int count) => _countComponents += count;

        public int GetCount() => _countComponents;

        public Component Get() => _component;

        public void ShowInfo()
        {
            Console.WriteLine(
                $"Название - {_component.Title} | Цена - {_component.Price} | Количество - {_countComponents}");
        }
    }
}