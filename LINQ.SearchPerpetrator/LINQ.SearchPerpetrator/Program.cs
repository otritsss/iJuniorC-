﻿using System;
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
        public BeingInCustody BeingInCustody;

        public Criminal(string name, string nationality, int height, int weight, BeingInCustody beingInCustody)
        {
            Name = name;
            Nationality = nationality;
            Height = height;
            Weight = weight;
            BeingInCustody = beingInCustody;
        }

        public void ShowInfo()
        {
            Console.WriteLine(
                $"Имя - {Name}, Национальность - {Nationality}, Рост - {Height}, Вес - {Weight}, Заключен под стражу? -{BeingInCustody}");
        }
    }

    class CriminalCreator
    {
        private List<string> _defaultNationalitys;
        private List<string> _defaultFirsNames;
        private List<string> _defaultFatherNames;
        private List<string> _defaultLastNames;

        public CriminalCreator()
        {
            _defaultNationalitys = new List<string>()
            {
                new string("Русский"),
                new string("Америконец"),
                new string("Украинец"),
                new string("Казах"),
                new string("Белорус"),
                new string("Филиппинец"),
                new string("Немец"),
            };

            _defaultFirsNames = new List<string>()
            {
                new string("Макс"),
                new string("Александр"),
                new string("Оскар"),
                new string("Никита"),
                new string("Марк"),
                new string("Евгений"),
            };

            _defaultFatherNames = new List<string>()
            {
                new string("Сергеевич"),
                new string("Алесандрович"),
                new string("Олегович"),
                new string("Романович"),
                new string("Василиевич"),
                new string("Василиевич"),
            };

            _defaultLastNames = new List<string>()
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

        public Criminal Create()
        {
            string firstName = _defaultFirsNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string fathertName = _defaultFatherNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string lastName = _defaultLastNames[UserUtils.GenerateRandomNumber(0, _defaultFirsNames.Count)];
            string name = lastName + " " + firstName + " " + fathertName;

            string nationality = _defaultNationalitys[UserUtils.GenerateRandomNumber(0, _defaultNationalitys.Count)];
            int minHeight = 150;
            int maxHeight = 200;
            int height = UserUtils.GenerateRandomNumber(minHeight, maxHeight);
            int minWeight = 50;
            int maxWeight = 110;
            int weight = UserUtils.GenerateRandomNumber(minWeight, maxWeight);
            int genderCount = 2;
            BeingInCustody beingInCustody = (BeingInCustody) UserUtils.GenerateRandomNumber(0, genderCount);

            Criminal criminal = new Criminal(name, nationality, height, weight, beingInCustody);
            return criminal;
        }
    }

    public enum BeingInCustody
    {
        InCustody,
        NotInCustody,
    }

    class UserUtils
    {
        private static Random _random = new Random();
        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}