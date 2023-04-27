using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ.Hospital
{
    class Program
    {
        static void Main()
        {
        }
    }

    class Hospital
    {
        private List<Patient> _patients = new List<Patient>();

        public void Work()
        {
            bool isWork = true;

            FillPatients();

            while (isWork)
            {
            }
        }

        private void FillPatients()
        {
            PatientCreator patientCreator = new PatientCreator();
            int patientsCount = 40;

            for (int i = 0; i < patientsCount; i++)
                _patients.Add(patientCreator.Create());
        }
    }

    class Patient
    {
        public Patient(string name, int age, string diseaseName)
        {
            Name = name;
            Age = age;
            DiseaseName = diseaseName;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string DiseaseName { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя пациента {Name} | Возраст - {Age} | Заболевание - {DiseaseName}");
        }
    }

    class PatientCreator
    {
        private List<string> _defaultFirstNames;
        private List<string> _defaultFatherNames;
        private List<string> _defaultLastNames;
        private List<string> _defaultDiseaseNames;

        public PatientCreator()
        {
            _defaultFirstNames = new List<string>()
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

            _defaultDiseaseNames = new List<string>()
            {
                new string("Спид"),
                new string("Ветрянка"),
                new string("Лейкемия"),
                new string("Камни в почках"),
                new string("Заикание"),
                new string("ОРВИ")
            };
        }

        public Patient Create()
        {
            string firstName = _defaultFirstNames[UserUtils.GenerateRandomNumber(0, _defaultFirstNames.Count)];
            string fatherName = _defaultFatherNames[UserUtils.GenerateRandomNumber(0, _defaultFatherNames.Count)];
            string lastName = _defaultLastNames[UserUtils.GenerateRandomNumber(0, _defaultLastNames.Count)];
            string name = lastName + firstName + fatherName;

            int minAge = 7;
            int maxAge = 62;
            int age = UserUtils.GenerateRandomNumber(minAge, maxAge);

            string diseaseName = _defaultDiseaseNames[UserUtils.GenerateRandomNumber(0, _defaultDiseaseNames.Count)];

            Patient patient = new Patient(name, age, diseaseName);

            return patient;
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    }
}