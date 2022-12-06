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
        private Queue<Buyer> _allBuyers;

        public List<Product> AllProductsShop = new List<Product>() { new Product("Молоко", 60), new Product("Хлеб", 25), new Product("Творог", 90), new Product("Куриное филе", 150), new Product("Шоколад", 55), new Product("Сыр", 100), new Product("Жвачка", 25), new Product("Колбаса", 110), new Product("Салат", 39), new Product("Сок", 80) };

        public void WorkShop()
        {
            CreateBuyers(_allBuyers, AllProductsShop);
        }

        private void CreateBuyers(Queue<Buyer> _allBuyers, List<Product> AllProductsShop)
        {
            int buyerInQueue = 7;
            int maxCountMoneyBuyer = 750;

            for (int i = 0; i < buyerInQueue; i++)
            {
                Random rand = new Random();
                int countMoney = rand.Next(maxCountMoneyBuyer);

                _allBuyers.Enqueue(new Buyer(countMoney, AllProductsShop));
            }
        }

    }

    class Buyer
    {
        public int Money;
        public Cart cart = new Cart();

        public Buyer(int money, List<Product> products)
        {
            Money = money;

            Random random = new Random();
            int countProducts = random.Next(products.Count);

            for (int i = 0; i < countProducts; i++)
            {
                int indexProduct = random.Next(products.Count);
                cart.Products.Add(products[indexProduct]);
            }

        }

        public void FillCart()
        {

        }
    }

    class Cart
    {
        public List<Product> Products = new List<Product>();

        public Cart()
        {

        }
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
