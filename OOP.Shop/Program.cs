using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Shop
{
    class Program
    {
        static void Main()
        {
            Shop shop = new Shop();
            shop.Work();
        }
    }

    class Shop
    {
        private const string SeePlayerInventoryCommand = "1";
        private const string SeeProductsCommand = "2";
        private const string BuyProduct = "3";

        private Seller _seller = new Seller(100);
        private Buyer _buyer = new Buyer(600);

        public void Work()
        {
            bool isOpenProgram = true;

            while (isOpenProgram)
            {
                Console.WriteLine($"Ваш баланс - {_buyer.Money}\nБаланс продавца - {_seller.Money}\n\n{SeePlayerInventoryCommand} - Посмотреть свой инвентарь\n{SeeProductsCommand} - Посмотреть список товаров продовца\n{BuyProduct} - Купить вещь у продавца");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SeePlayerInventoryCommand:
                        _buyer.ShowProducts();
                        break;
                    case SeeProductsCommand:
                        _seller.ShowProducts();
                        break;
                    case BuyProduct:
                        Deal();
                        break;
                    default:
                        Console.WriteLine("Вы ввели некорректное значение...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Deal()
        {
            _seller.ShowProducts();

            Console.Write("Введите номер товара: ");
            string inputProduct = Console.ReadLine();

            if (int.TryParse(inputProduct, out int indexProduct) == false)
            {
                Console.WriteLine("Вы ввели некорретное значение..");
            }
            else if (_seller.GetProduct(indexProduct, out Product product))
            {
                if (_buyer.Money >= product.Price)
                {
                    _buyer.BuyProduct(product);
                    _seller.SellProduct(indexProduct - 1);
                }
                else
                {
                    Console.WriteLine($"Вам не хватает {product.Price - _buyer.Money} денег");
                }
            }
        }
    }

    class Human
    {
        protected List<Product> Products;
        public int Money { get; protected set; }

        public Human(int money)
        {
            Money = money;
        }

        public void ShowProducts()
        {
            for (int i = 0; i < Products.Count; i++)
                Console.WriteLine($"{i + 1}. {Products[i].Title} - {Products[i].Price}");
        }
    }

    class Seller : Human
    {
        public Seller(int money) : base(money)
        {
            Products = new List<Product>();

            Products.Add(new Product("Алмазный меч", 500));
            Products.Add(new Product("Железная кирка", 100));
            Products.Add(new Product("Зелье скорости", 200));
            Products.Add(new Product("Алмазный шлем", 250));
        }

        public bool GetProduct(int indexProduct, out Product product)
        {
            if (indexProduct < 1 || indexProduct > Products.Count)
            {
                Console.WriteLine("Вы дебил? Выберите одно число из тех, которые написаны!");
                product = null;
                return false;
            }
            else
            {
                product = Products[indexProduct - 1];
                return true;
            }
        }

        public void SellProduct(int indexProduct)
        {
            Money += Products[indexProduct].Price;
            Products.RemoveAt(indexProduct);
        }
    }

    class Buyer : Human
    {
        public Buyer(int money) : base(money)
        {
            Products = new List<Product>();
        }

        public void BuyProduct(Product product)
        {
            Products.Add(product);
            Money -= product.Price;
        }
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