using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.WorkShop();
        }
    }

    class Shop
    {
        private Queue<Buyer> _allBuyers = new Queue<Buyer>();
        private List<Product> _allProductsShop;

        public Shop()
        {
            _allProductsShop = new List<Product>() { new Product("Молоко", 60), new Product("Хлеб", 25),
                new Product("Творог", 90), new Product("Куриное филе", 150), new Product("Шоколад", 55),
                new Product("Сыр", 100), new Product("Жвачка", 25), new Product("Колбаса", 110),
                new Product("Салат", 39), new Product("Сок", 80) };
        }

        public void WorkShop()
        {
            int numberBuyer = 1;
            CreateBuyers();

            while (_allBuyers.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Buyer buyer = _allBuyers.Peek();

                Console.WriteLine($"В очереди стоит {_allBuyers.Count} клиентов");
                buyer.ShowInfo(numberBuyer);

                if (buyer.GetPoructsCount() == 0)
                {
                    Console.WriteLine("У Вас не осталось товаров в корзине, потому что вы бедный чел. До свидания");
                    _allBuyers.Dequeue();
                    numberBuyer++;
                }
                else if (buyer.SumBuy > buyer.Money)
                {
                    Console.WriteLine("Вам не хватает денег, поэтому мы уберем некоторые товары из корзины");
                    buyer.RemoveProduct();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вам хватает денег. До свидания");
                    _allBuyers.Dequeue();
                    numberBuyer++;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CreateBuyers()
        {
            int buyerInQueue = 7;

            for (int i = 0; i < buyerInQueue; i++)
                _allBuyers.Enqueue(new Buyer(_allProductsShop));
        }
    }

    class Buyer
    {
        private static Random _random = new Random();
        private List<Product> _products = new List<Product>();

        public Buyer(List<Product> products)
        {
            int maxCountMoney = 750;
            Money = _random.Next(maxCountMoney);

            int countProductsInCart = _random.Next(1, products.Count);

            for (int i = 0; i < countProductsInCart; i++)
            {
                int indexProduct = _random.Next(products.Count);
                _products.Add(products[indexProduct]);
                SumBuy += products[indexProduct].Price;
            }
        }

        public int SumBuy { get; private set; }
        public int Money { get; private set; }

        public int GetPoructsCount() => _products.Count;

        public void RemoveProduct()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            int indexRemoveProduct = _random.Next(_products.Count);
            Console.WriteLine($"Убрали {_products[indexRemoveProduct].Title}");
            SumBuy -= _products[indexRemoveProduct].Price;
            _products.RemoveAt(indexRemoveProduct);
        }

        public void ShowInfo(int number)
        {
            Console.Write($"{number}-й клиент. Денег: {Money}\nКорзина: ");

            for (int i = 0; i < _products.Count; i++)
                Console.WriteLine($"{_products[i].Title} - {_products[i].Price} руб");

            Console.WriteLine();
        }
    }

    class Product
    {
        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
    }
}
