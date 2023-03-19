using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

/// <summary>
///
/// 1. 
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
        }
    }

    class Autoservise
    {
        private WarehouseDetails _warehouseDetails = new WarehouseDetails();

        public void Work()
        {
            _warehouseDetails.ShowInfoCount();

            Console.ReadKey();
        }
    }

    class WarehouseDetails
    {
        private DetailsCreator _detailsCreator = new DetailsCreator();

        private List<List<Detail>> _allDeatails = new List<List<Detail>>()
        {
            new List<Detail>(),
            new List<Detail>(),
            new List<Detail>(),
            new List<Detail>(),
            new List<Detail>(),
            new List<Detail>(),
        };

        public WarehouseDetails()
        {
            for (int i = 0; i < _allDeatails.Count; i++)
            {
                _allDeatails[i] = _detailsCreator.CreateList(i);
            }
        }

        public void ShowInfoCount()
        {
            for (int i = 0; i < _allDeatails.Count; i++)
            {
                Console.WriteLine($"{i} - Количество деталей = {_allDeatails[i].Count}");
            }
        }
    }

    class Detail
    {
        private static Random _random = new Random();
        public Detail(string title, int price)
        {
            Title = title;
            Price = price;
            
            
        }

        public string Title { get; private set; }
        public int Price { get; private set; }
        public bool isBroke { get; private set; }
    }

    class DetailsCreator
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

        public List<Detail> CreateList(int indexDetail)
        {
            int minCountDetail = 4;
            int maxCountDetail = 21;
            int countDetails = _random.Next(minCountDetail, maxCountDetail);
            List<Detail> list = new List<Detail>();

            for (int i = 0; i < countDetails; i++)
                list.Add(Create(indexDetail));

            return list;
        }

        private Detail Create(int indexDetail) =>
            new Detail(_defaultDetails[indexDetail].Title, _defaultDetails[indexDetail].Price);
    }

    class Car
    {
        private List<Detail> _details = new List<Detail>()
        {
            new Detail("Коробка", 55000),
        };

        private static Random _random = new Random();

        public int Money { get; private set; }
    }
}