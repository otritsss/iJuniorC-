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
        static private Random _random = new Random();
        private Queue<Buyer> _allBuyers = new Queue<Buyer>();
        private List<Product> _allProductsShop = new List<Product>() { new Product("Молоко", 60), new Product("Хлеб", 25), new Product("Творог", 90), new Product("Куриное филе", 150), new Product("Шоколад", 55), new Product("Сыр", 100), new Product("Жвачка", 25), new Product("Колбаса", 110), new Product("Салат", 39), new Product("Сок", 80) };

        public void WorkShop()
        {
            int numberBuyer = 0;
            int queueNumberBuyer = 0;
            CreateBuyers();

            while (_allBuyers.Count > 0)
            {
                Buyer buyer = _allBuyers.Peek();

                Console.WriteLine($"В очереди стоит {_allBuyers.Count} клиентов");
                buyer.ShowInfo(++numberBuyer);


                if (buyer.SumBuy > buyer.Money)
                {
                    Console.WriteLine("Вам не хватает денег, поэтому мы уберем некоторые товары из корзины");
                    buyer.RemoveProducts();

                    Console.Clear();
                    Console.WriteLine($"В очереди стоит {_allBuyers.Count} клиентов");
                    buyer.ShowInfo(numberBuyer);
                }
                else
                {
                    Console.WriteLine("Вам хватает денег. До свидания");
                    _allBuyers.Dequeue();
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
        public int SumBuy { get; private set; }
        public int Money { get; private set; }

        private Cart _cart = new Cart();

        static private Random _random = new Random();
        public Buyer(List<Product> products)
        {
            int maxCountMoneyBuyer = 750;
            Money = _random.Next(maxCountMoneyBuyer);

            int countProductsInCart = _random.Next(products.Count);

            for (int i = 0; i < countProductsInCart; i++)
            {
                int indexProduct = _random.Next(products.Count);
                _cart.Products.Add(products[indexProduct]);
                SumBuy += products[indexProduct].Price;
            }
        }
        public void RemoveProducts()
        {
            int indexRemoveProduct = _random.Next(1, _cart.Products.Count);
            _cart.Products.RemoveAt(indexRemoveProduct);
            SumBuy -= _cart.Products[indexRemoveProduct].Price;

            Console.WriteLine($"Убрали {_cart.Products[indexRemoveProduct].Title}");
        }

        public void ShowInfo(int number)
        {
            Console.Write($"{number}-й клиент. Денег: {Money}\nКорзина: ");

            for (int i = 0; i < _cart.Products.Count; i++)
                Console.Write($"{_cart.Products[i].Title}, ");

            Console.WriteLine();
        }
    }

    class Cart
    {
        public List<Product> Products = new List<Product>();
    }

    class Product
    {
        public string Title { get; private set; }
        public int Price { get; private set; }

        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }
    }
}
