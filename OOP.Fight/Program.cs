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

                leftFighter.UseSuperpower(rightFighter);
                rightFighter.UseSuperpower(leftFighter);
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
        public bool IsHit { get; protected set; } = true;
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public bool IsAbility { get; protected set; }
        public int AbilityDamage { get; protected set; }

        public Fighter(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public virtual int UseSuperpower(Fighter fighter) => 0;

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual void Attack(Fighter fighter)
        {
            Random random = new Random();
            int maxHitChance = 10;
            int hitChance = random.Next(maxHitChance);
            int activeHitChance = 7;

            if (hitChance >= activeHitChance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Name} промахнулся");
                Console.ForegroundColor = ConsoleColor.White;

                IsHit = false;
            }

            if (IsAbility && hitChance < activeHitChance)
            {
                IsHit = true;
                fighter.TakeDamage(AbilityDamage);
            }
            else if (hitChance < activeHitChance)
            {
                IsHit = true;
                fighter.TakeDamage(Damage);
            }

            if (Health < 0)
                Health = 0;
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

        public override int UseSuperpower(Fighter fighter)
        {
            Random random = new Random();
            int maxChance = 10;
            int abilityChance = random.Next(maxChance);

            if (abilityChance < _activeDanceOfFire)
            {
                int danceOfFire = 5;
                AbilityDamage = Damage + danceOfFire;
                IsAbility = true;
                ShowInfoAbility("Танец огня");
                return AbilityDamage;
            }
            else
            {
                IsAbility = false;
            }

            return Damage;
        }
    }

    class Zenitsu : Fighter
    {
        public Zenitsu() : base("Зеницу", 100, 20) { }

        public override int UseSuperpower(Fighter fighter)
        {
            IsAbility = false;

            if (IsHit == false) // если он промахивается, то cледующий удар + 11 урона
            {
                int sleep = 11;
                IsAbility = true;
                AbilityDamage = Damage + sleep;
                ShowInfoAbility("Сон");
                return AbilityDamage;
            }

            return Damage;
        }
    }

    class Kanao : Fighter
    {
        private bool _isBodyControl;
        public Kanao() : base("Канао", 100, 20) { }

        public override int UseSuperpower(Fighter fighter) // если она в прошлый раз промахнулась, то в следующий раз точно попадает и с каждым ударом прибавляется по 1 ед. урона
        {
            if (IsHit == false)
            {
                _isBodyControl = true;
                ShowInfoAbility("Идеальный контроль над телом");
            }

            if (_isBodyControl)
                Damage++;

            return Damage;
        }
    }

    class Nezuko : Fighter
    {
        private int _regeneration = 3;
        public Nezuko() : base("Незуко", 100, 20) { }

        public override int UseSuperpower(Fighter fighter)
        {
            Health += _regeneration;
            ShowInfoAbility("Регенерацию");
            return Damage;
        }
    }

    class Inosuke : Fighter
    {
        private int _boarAttack = 2;
        private int _angry = 0;
        private int _activeBoarAttack = 2;
        public Inosuke() : base("Иноске", 100, 20) { }

        public override int UseSuperpower(Fighter fighter)
        {
            IsAbility = false;

            if (IsHit == false)
                _angry++;

            if (_angry >= _activeBoarAttack)
            {
                _angry = 0;
                AbilityDamage = Damage * _boarAttack;
                IsAbility = true;
                ShowInfoAbility("Атаку кабана");
                return AbilityDamage;
            }

            return Damage;
        }
    }
}