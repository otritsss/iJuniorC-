using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.BooksRepository
{
    class Program
    {
        static void Main()
        {
            BookRepository bookRepository = new BookRepository();
            bookRepository.Work();
        }
    }

    class BookRepository
    {
        private List<Book> _books = new List<Book>();

        public BookRepository()
        {
            AddDefoltBooks();
        }

        public void Work()
        {
            const string AddBookCommand = "1";
            const string RemoveBookCommand = "2";
            const string ShowAllBooksCommand = "3";
            const string ShowBooksByParameterCommand = "4";

            bool isOpenProgram = true;

            while (isOpenProgram)
            {
                Console.Write($"{AddBookCommand} - Добавить книгу в хранилище\n{RemoveBookCommand} - Убрать книгу с хранилища\n{ShowAllBooksCommand} - Показать все книги\n{ShowBooksByParameterCommand} - Показать все книги по параметру\nВведите команду: ");
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case AddBookCommand:
                        AddBook();
                        break;
                    case RemoveBookCommand:
                        RemoveBook();
                        break;
                    case ShowAllBooksCommand:
                        ShowAllBooks();
                        break;
                    case ShowBooksByParameterCommand:
                        ShowBooksByParameter();
                        break;
                    default:
                        Console.WriteLine("Введите корректное значение..");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddDefoltBooks()
        {
            Book[] defoltBooks = { new Book("Достоевский", "Преступление и наказание", 1865), new Book("Достоевский", "Идиот", 1867), new Book("Достоевский", "Белые ночи", 1848), new Book("Пушкин", "Капитанская дочка", 1836), new Book("Пушкин", "Евгений Онегин", 1833), new Book("Пушкин", "Медный всадник", 1833), new Book("Мартин", "Чистый код: создание, анализ и рефакторинг", 2013), new Book("Мартин", "Чистая архитектура. Искусство разработки программного обеспечения", 2018) };

            for (int i = 0; i < defoltBooks.Length; i++)
                _books.Add(defoltBooks[i]);
        }

        private void AddBook()
        {
            Console.Write("Введите автора книги: ");
            string inputAuthorBook = Console.ReadLine();
            Console.Write("Введите название: ");
            string inputTitleBook = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            string inputYearProductionBook = Console.ReadLine();

            if (int.TryParse(inputYearProductionBook, out int yearProductionBook))
                _books.Add(new Book(inputAuthorBook, inputTitleBook, yearProductionBook));
            else
                Console.WriteLine("Вы ввели некорректное значение...");
        }

        private void RemoveBook()
        {
            ShowAllBooks();

            Console.Write("Введите индекс книги которую хотите удалить: ");

            if (int.TryParse(Console.ReadLine(), out int inputIndexRemoveBook))
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (i == inputIndexRemoveBook)
                        _books.RemoveAt(inputIndexRemoveBook - 1);
                }
            }
            else
            {
                Console.WriteLine("Вы ввели некорректное значение");
            }
        }

        private void ShowAllBooks()
        {
            Console.SetCursorPosition(0, 15);

            for (int i = 0; i < _books.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _books[i].ShowInfoBook();
            }

            Console.SetCursorPosition(0, 4);
        }

        private void ShowBooksByParameter()
        {
            const string AuthoreShowCommand = "1";
            const string TitleShowCommand = "2";
            const string YearProductionShowCommand = "3";
            bool isSearchBook = false;

            Console.Write($"Показать книги\n{AuthoreShowCommand} - по фамилии?\n{TitleShowCommand} - по названию\n{YearProductionShowCommand} - по году выпуска\nВведите: ");
            string inputParameterBooks = Console.ReadLine();

            switch (inputParameterBooks)
            {
                case AuthoreShowCommand:
                    ShowBooksByAuthor(ref isSearchBook);
                    break;
                case TitleShowCommand:
                    ShowBooksByTitle(ref isSearchBook);
                    break;
                case YearProductionShowCommand:
                    ShowBooksByYearProduction(ref isSearchBook);
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение...");
                    break;
            }

            if (!isSearchBook)
                Console.WriteLine("Ничего не найдено..");
        }

        private void ShowBooksByAuthor(ref bool isSearchBook)
        {
            Console.Write("Введите автора: ");
            string userInputAuthor = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Author == userInputAuthor)
                {
                    _books[i].ShowInfoBook();
                    isSearchBook = true;
                }
            }
        }

        private void ShowBooksByTitle(ref bool isSearchBook)
        {
            Console.Write("Введите название: ");
            string userInputTitle = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].Title == userInputTitle)
                {
                    _books[i].ShowInfoBook();
                    isSearchBook = true;
                }
            }
        }

        private void ShowBooksByYearProduction(ref bool isSearchBook)
        {
            Console.Write("Введите год выпуска: ");

            if (int.TryParse(Console.ReadLine(), out int yearProduction))
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (_books[i].YearProduction == yearProduction)
                    {
                        _books[i].ShowInfoBook();
                        isSearchBook = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы ввели некорректное значение");
            }

        }
    }

    class Book
    {
        public string Author;
        public string Title;
        public int YearProduction;

        public Book(string author, string title, int yearProduction)
        {
            Author = author;
            Title = title;
            YearProduction = yearProduction;
        }

        public void ShowInfoBook()
        {
            Console.WriteLine($"Автор - {Author} | Название - {Title} | Год выпуска - {YearProduction}");
        }
    }
}