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
            CreateBuyers();

            foreach (var buyer in _allBuyers)
            {
                Console.WriteLine($"В очереди стоит {_allBuyers.Count} клиентов");

                Console.WriteLine($"{++numberBuyer}-й клиент. Здравствуйте, сумма Вашей покупки составляет - {buyer.SumBuy}. У вас {buyer.Money} денег");

                if (buyer.SumBuy <= buyer.Money)
                {
                    Console.WriteLine("Вам хватает средств, поэтому Ваша покупка прошла успешно. До свидания!");
                }
                else
                {
                    Console.WriteLine($"Вам не хватает {buyer.SumBuy - buyer.Money} денег, поэтому мы уберем один или несколько товаров из корзины");
                    buyer.RemoveProduct();
                    Console.WriteLine($"Теперь сумма Вашей покупки состовляет {buyer.SumBuy}. Ваша покупка прошла успешно. До свидания!");
                }
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
        public void RemoveProduct()
        {
            while (SumBuy > Money)
            {
                int indexRemoveProduct = _random.Next(_cart.Products.Count);
                _cart.Products.RemoveAt(indexRemoveProduct);
                SumBuy -= _cart.Products[indexRemoveProduct].Price;
            }
        }

        public void ShowInfo(int number)
        {
            Console.Write($"{number + 1}. Денег: {Money}\n Корзина: ");

            for (int i = 0; i < _cart.Products.Count; i++)
                Console.Write($"{_cart.Products[i].Title}, ");
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
