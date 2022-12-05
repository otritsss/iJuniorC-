namespace OOP.Fight
{
    class Program
    {
        static void Main()
        {
            Fight fight = new Fight();
            fight.Battle();
        }
    }

    class Fight
    {
        public void Battle()
        {
            Fighter leftFighter;
            Fighter rightFighter;

            do
            {
                leftFighter = ChoiceFighter("левого");
                rightFighter = ChoiceFighter("правого");

                Console.Clear();

            } while (leftFighter == null || rightFighter == null);

            int round = 1;

            while (leftFighter.Health > 0 && rightFighter.Health > 0)
            {
                Console.WriteLine($"{round++}-е столкновение");

                leftFighter.Attack(rightFighter);
                rightFighter.Attack(leftFighter);

                leftFighter.ShowInfo();
                rightFighter.ShowInfo();
            }

            if (leftFighter.Health <= 0 && rightFighter.Health <= 0)
                Console.WriteLine("Ничья. Оба оппонента погибли");
            else if (leftFighter.Health <= 0)
                Console.WriteLine($"Победу одержал - {rightFighter.Name}");
            else if (rightFighter.Health <= 0)
                Console.WriteLine($"Победу одержал - {leftFighter.Name}");

            Console.ReadKey();
            Console.Clear();
        }

        private Fighter ChoiceFighter(string text)
        {
            Console.WriteLine("Нажмите на любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();

            Fighter[] fighters = { new Tanjiro(), new Zenitsu(), new Kanao(), new Nezuko(), new Inosuke() };

            for (int i = 0; i < fighters.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                fighters[i].ShowInfo();
            }

            Console.Write($"Введите номер бойца {text} ринга: ");

            try
            {
                int numberFighter = Convert.ToInt32(Console.ReadLine());
                Fighter fighter = fighters[numberFighter - 1];
                return fighter;
            }
            catch (Exception)
            {
                Console.WriteLine("Вы ввели некорретное значение");
            }

            return null;
        }
    }

    class Fighter
    {
        protected int HitCount = 0;
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public Fighter(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void Attack(Fighter fighter)
        {
            TakeDamage(Damage);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} | Здоровье - {Health} | Урон - {Damage}");
        }

        protected void ShowInfoAbility(string descriptionAbility)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} использовал {descriptionAbility}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Tanjiro : Fighter
    {
        private int _activeDanceOfFire = 3;
        public Tanjiro() : base("Танзиро", 100, 20) { }

        public override void Attack(Fighter fighter)
        {
            Random random = new Random();
            int maxChance = 10;
            int abilityChance = random.Next(maxChance);

            if (abilityChance < _activeDanceOfFire)
            {
                int danceOfFire = 5;
                fighter.TakeDamage(Damage + danceOfFire);
                ShowInfoAbility("Танец огня");

            }
            else
            {
                fighter.TakeDamage(Damage);
            }
        }
    }

    class Zenitsu : Fighter
    {
        private int _activeSleep = 2;
        private int _hitCount = 0;
        public Zenitsu() : base("Зеницу", 100, 20) { }

        public override void Attack(Fighter fighter)
        {
            if (_hitCount == _activeSleep)
            {
                int sleep = 2;
                fighter.TakeDamage(Damage * 2);
                ShowInfoAbility("Сон");
                _hitCount = 0;
            }
            else
            {
                fighter.TakeDamage(Damage);
                _hitCount++;
            }
        }
    }

    class Kanao : Fighter
    {
        private bool _isBodyControl;
        public Kanao() : base("Канао", 100, 20) { }

        public override void Attack(Fighter fighter) // если она в прошлый раз промахнулась, то в следующий раз точно попадает и с каждым ударом прибавляется по 1 ед. урона
        {

        }
    }

    class Nezuko : Fighter
    {
        private int _regeneration = 3;
        public Nezuko() : base("Незуко", 100, 20) { }

        public override void Attack(Fighter fighter)
        {

        }
    }

    class Inosuke : Fighter
    {
        private int _boarAttack = 2;
        private int _angry = 0;
        private int _activeBoarAttack = 2;
        public Inosuke() : base("Иноске", 100, 20) { }

        public override void Attack(Fighter fighter)
        {

        }
    }
}