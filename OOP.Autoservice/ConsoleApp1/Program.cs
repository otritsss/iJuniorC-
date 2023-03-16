using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Auroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            DeatailsCreator creator = new DeatailsCreator();
            creator.CreateList();
        }
    }

    class Autoservise
    {
    }

    class WarehouseDetails
    {
    }

    class Detail
    {
        public Detail(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
    }

    class DeatailsCreator
    {
        private static Random _random = new Random();

        private List<Detail> _defaultDetails = new List<Detail>()
        {
            new Detail("Коробка", 55000),
            new Detail("Подвеска", 40000),
            new Detail("Двигатель", 100000),
            new Detail("Кондиционер", 2500),
            new Detail("Ремень генератора", 500),
            new Detail("Колесо", 1500)
        };

        public List<Detail> CreateList()
        {
            int minCountDetail = 4;
            int maxCountDetail = 21;
            int countDetails = _random.Next(minCountDetail, maxCountDetail);
            List<Detail> list = new List<Detail>();

            for (int i = 0; i < countDetails; i++)
                list.Add(Create());

            return list;
        }

        private Detail Create()
        {
            Detail detail = _defaultDetails[_random.Next(_defaultDetails.Count)];
            return detail;
        }
    }

    class Human
    {
    }
}