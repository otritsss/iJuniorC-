using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.Auroservice
{
    class Program
    {
        static void Main()
        {
        }
    }

    class Database
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public void Open()
        {
        }
    }

    class Criminal
    {
        public string Name;
        public string Nationality;
        public int Height;
        public int Weight;
        public bool IsInCustody;

        public void ShowInfo()
        {
            Console.WriteLine(
                $"Имя - {Name}, Национальность - {Nationality}, Рост - {Height}, Вес - {Weight}, Заключен под стражу? -{IsInCustody}");
        }
    }

    class CriminalCreator
    {
        private List<string> DefaultNationalitys = new List<string>()
        {
            new string("Русский"),
            new string("Америконец"),
            new string("Украинец"),
            new string("Казах"),
            new string("Белорус"),
            new string("Филиппинец"),
            new string("Немец"),
        };

        private List<string> DefaultFirsNames = new List<string>()
        {
            new string("Макс"),
            new string("Александр"),
            new string("Оскар"),
            new string("Никита"),
            new string("Марк"),
            new string("Евгений"),
        };

        private List<string> DefaultFatherNames = new List<string>()
        {
            new string("Сергеевич"),
            new string("Алесандрович"),
            new string("Олегович"),
            new string("Романович"),
            new string("Василиевич"),
            new string("Василиевич"),
        };

        private List<string> DefaultLastNames = new List<string>()
        {
            new string("Сергеев"),
            new string("Соколовский"),
            new string("Хартман"),
            new string("Гительман"),
            new string("Шабутдинов"),
            new string("Шабутдинов"),
            new string("Гришаков"),
        };
    }
}