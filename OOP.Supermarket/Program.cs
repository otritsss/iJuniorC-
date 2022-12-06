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

        public List<Product> AllProductsShop = new List<Product>() { new Product("Молоко", 60), new Product("Хлеб", 25), new Product("Творог", 90), new Product("Куриное филе", 150), new Product("Шоколад", 55), new Product("Сыр", 100), new Product("Жвачка", 25), new Product("Колбаса", 110), new Product("Салат", 39), new Product("Сок", 80) };

        public void WorkShop()
        {
            CreateBuyers();

            int number = 0;

            foreach (var buyer in _allBuyers)
            {
                buyer.ShowInfo(number++);
                Console.WriteLine();
            }
        }

        private void CreateBuyers()
        {
            int buyerInQueue = 7;

            for (int i = 0; i < buyerInQueue; i++)
            {
                _allBuyers.Enqueue(new Buyer(AllProductsShop));
            }
        }
    }

    class Buyer
    {
        public int Money;
        public Cart cart = new Cart();

        static private Random _random = new Random();
        public Buyer(List<Product> products)
        {
            int maxCountMoneyBuyer = 750;
            Money = _random.Next(maxCountMoneyBuyer);

            int countProductsInCart = _random.Next(products.Count);

            for (int i = 0; i < countProductsInCart; i++)
            {
                int indexProduct = _random.Next(products.Count);
                cart.Products.Add(products[indexProduct]);
            }
        }

        public void ShowInfo(int number)
        {
            Console.Write($"{number + 1}. Денег: {Money}\n Корзина: ");

            for (int i = 0; i < cart.Products.Count; i++)
            {
                Console.Write($"{cart.Products[i].Title}, ");
            }
        }
    }

    class Cart
    {
        public List<Product> Products = new List<Product>();
    }

    class Product
    {
        public string Title;
        public int Price;

        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }
    }
}
