using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.CardDeck
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Work();
        }
    }

    class Game
    {
        private const string YesCommand = "1";
        private const string NoCommand = "2";

        private Deck _deck = new Deck();
        private Player _player = new Player("Nikita");

        public void Work()
        {
            bool isOpenProgram = true;

            while (_deck.IsEmpty == false && isOpenProgram)
            {
                Console.Write($"Берете карту?\n{YesCommand}) Да\n{NoCommand}) Нет\nВведите команду: ");
                string choiceAction = Console.ReadLine();

                if (choiceAction == YesCommand)
                {
                    Console.WriteLine("Сколько карт желаете взять?");
                    string inputCountCards = Console.ReadLine();

                    if (int.TryParse(inputCountCards, out int countCards))
                        Trade(countCards);
                    else
                        Console.WriteLine("Вы ввели некорректное значение..");
                }
                else
                {
                    isOpenProgram = false;
                    Console.WriteLine("Ладно, как хочешь");
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Карты закончились. Пока)");
        }

        private void Trade(int inputCountCards)
        {
            for (int i = 0; i < inputCountCards; i++)
            {
                _player.AddCard(_deck.GetCard());
                _player.ShowPulledСard(_deck.GetCard());
                _deck.RemoveCard(_deck.GetCard());
            }
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();
        public bool IsEmpty
        {
            get
            {
                if (_cards.Count == 0)
                    return true;
                else
                    return false;
            }
            private set { }
        }
        public Deck()
        {
            List<string> suits = new List<string>() { "Крести", "Буби", "Пик", "Черви" };
            List<string> titles = new List<string>() { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            AddCards(suits, titles);
        }

        public Card GetCard()
        {
            Random random = new Random();
            int randomNumber = random.Next(_cards.Count);
            return _cards[randomNumber];
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }

        private void AddCards(List<string> suits, List<string> titles)
        {
            for (int i = 0; i < suits.Count; i++)
            {
                for (int j = 0; j < titles.Count; j++)
                {
                    _cards.Add(new Card($"{titles[j]} {suits[i]}"));
                }
            }
        }
    }

    class Card
    {
        public string Title { get; private set; }

        public Card(string title)
        {
            Title = title;
        }
    }

    class Player
    {
        private List<Card> _playingCards = new List<Card>();
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        public void ShowPulledСard(Card card)
        {
            Console.WriteLine($"Игрок вытянул карту '{card.Title}'");
        }

        public void AddCard(Card card)
        {
            _playingCards.Add(card);
        }
    }
}
