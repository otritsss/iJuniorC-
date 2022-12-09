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

            if (int.TryParse(Console.ReadLine(), out int numberFighter) && numberFighter < fighters.Length && numberFighter >= 0)
                return fighters[numberFighter - 1];
            else
                Console.WriteLine("Вы ввели некорретное значение");

            return null;
        }
    }

    class Fighter
    {
        protected int HitCount = 0;

        public bool IsHit { get; protected set; }
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public Fighter(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public virtual bool CanTakeDamage()
        {
            return true;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            IsHit = true;
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
        public Tanjiro() : base("Танзиро", 100, 20) { }

        public override void Attack(Fighter fighter) // Радномный шанс прибывить к урону 5 ед.
        {
            Random random = new Random();
            int maxChance = 10;
            int activeDanceOfFire = 3;
            int abilityChance = random.Next(maxChance);

            if (fighter.CanTakeDamage() && abilityChance < activeDanceOfFire)
            {
                int danceOfFire = 5;
                fighter.TakeDamage(Damage + danceOfFire);
                ShowInfoAbility("Танец огня");

            }
            else if (fighter.IsHit)
            {
                fighter.TakeDamage(Damage);
            }
        }
    }

    class Zenitsu : Fighter
    {
        private int _hitCount = 0;

        public Zenitsu() : base("Зеницу", 100, 20) { }

        public override void Attack(Fighter fighter) // Каждый третий удар удвоенный урон
        {
            int activeSleep = 2;

            if (fighter.CanTakeDamage() && _hitCount == activeSleep)
            {
                int sleep = 2;
                fighter.TakeDamage(Damage * sleep);
                ShowInfoAbility("Сон");
                _hitCount = 0;
            }
            else if (fighter.IsHit)
            {
                fighter.TakeDamage(Damage);
                _hitCount++;
            }
        }
    }

    class Kanao : Fighter
    {
        public Kanao() : base("Канао", 100, 20) { }

        public override bool CanTakeDamage()
        {
            Random random = new Random();
            int maxHitChance = 10;
            int activeHitChance = 5;
            int hitChance = random.Next(maxHitChance);

            if (hitChance >= activeHitChance)
            {
                ShowInfoAbility("Уклонение от удара");
                IsHit = false;
                return false;
            }
            else
            {
                IsHit = true;
                return true;
            }
        }

        public override void Attack(Fighter fighter) // Может уклониться
        {
            if (fighter.CanTakeDamage())
                fighter.TakeDamage(Damage);
        }
    }

    class Nezuko : Fighter
    {
        private int _regeneration = 3;

        public Nezuko() : base("Незуко", 100, 20) { } // Каждый удар происходит регенерация здоровья

        public override void Attack(Fighter fighter)
        {
            Health += _regeneration;
            ShowInfoAbility("Регенирацию");

            if (fighter.CanTakeDamage())
                fighter.TakeDamage(Damage);
        }
    }

    class Inosuke : Fighter
    {
        private int _boarAttack = 1;
        private int _mana = 0;

        public Inosuke() : base("Иноске", 100, 20) { }

        public override void Attack(Fighter fighter) // Каждый второй удар прописывает двоечку вместо одного удара
        {
            _mana++;

            if (_mana == _boarAttack)
            {
                TakeDamage(Damage);
                TakeDamage(Damage);
                _mana = 0;
            }
            else
            {
                TakeDamage(Damage);
            }
        }
    }
}