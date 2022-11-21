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
        Fighter[] fighters = { new Tanjiro(100, 20), new Zenitsu(100, 20), new Kanao(100, 20), new Nezuko(100, 20), new Inosuke(100, 20) };
        public void Battle()
        {
            for (int i = 0; i < fighters.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                fighters[i].ShowInfo();
            }

            Console.Write("Введите номер бойца левого ринга: ");
            Fighter leftFighter = fighters[Convert.ToInt32(Console.ReadLine()) - 1];
            Console.Write("Введите номер бойца правого ринга: ");
            Fighter rightFighter = fighters[Convert.ToInt32(Console.ReadLine()) - 1];

            int round = 1;

            while (leftFighter.Health > 0 && rightFighter.Health > 0)
            {
                Console.WriteLine($"{round++}-е столкновение");

                leftFighter.AdditionalDamage(rightFighter);
                rightFighter.AdditionalDamage(leftFighter);
                leftFighter.TakeDamage(rightFighter);
                rightFighter.TakeDamage(leftFighter);

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
    }

    class Fighter
    {
        public bool IsHit { get; protected set; } = true;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
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

        public virtual void AdditionalDamage(Fighter fighter) { }

        public void ShowInfoAbility(string descriptionAbility)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} использовал {descriptionAbility}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public virtual void TakeDamage(Fighter fighter)
        {
            Random random = new Random();
            int maxHitChance = 10;
            int hitChance = random.Next(maxHitChance);
            int trueHitChance = 7;

            if (hitChance > trueHitChance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{fighter.Name} промахнулся");
                Console.ForegroundColor = ConsoleColor.White;

                fighter.IsHit = false;
            }

            if (fighter.IsAbility && hitChance < trueHitChance)
            {
                fighter.IsHit = true;
                Health -= fighter.AbilityDamage;
            }
            else if (hitChance < trueHitChance)
            {
                fighter.IsHit = true;
                Health -= fighter.Damage;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} | Здоровье - {Health} | Урон - {Damage}");
        }
    }

    class Tanjiro : Fighter
    {
        private int _trueDanceOfFire = 3;
        public Tanjiro(int health, int damage) : base("Танзиро", health, damage) { }

        public override void AdditionalDamage(Fighter fighter)
        {
            Random random = new Random();
            int maxChance = 10;
            int abilityChance = random.Next(maxChance);

            if (abilityChance < _trueDanceOfFire)
            {
                int danceOfFire = 5;
                AbilityDamage = Damage + danceOfFire;
                IsAbility = true;
                ShowInfoAbility("Танец огня");
            }
            else
            {
                IsAbility = false;
            }
        }

    }

    class Zenitsu : Fighter
    {
        public Zenitsu(int health, int damage) : base("Зеницу", health, damage) { }

        public override void AdditionalDamage(Fighter fighter)
        {
            IsAbility = false;

            if (IsHit == false) // если он промахивается, то cледующий удар + 11 урона
            {
                int sleep = 11;
                IsAbility = true;
                AbilityDamage = Damage + sleep;
                ShowInfoAbility("Сон");
            }
        }
    }

    class Kanao : Fighter
    {
        private bool _isBodyControl;
        public Kanao(int health, int damage) : base("Канао", health, damage) { }

        public override void AdditionalDamage(Fighter fighter) // если она в прошлый раз промахнулась, то в следующий раз точно попадает и с каждым ударом прибавляется по 1 ед. урона
        {
            if (IsHit == false)
            {
                _isBodyControl = true;
                ShowInfoAbility("Идеальный контроль над телом");
            }

            if (_isBodyControl)
                Damage++;

        }
    }

    class Nezuko : Fighter
    {
        private int _regeneration = 3;
        public Nezuko(int health, int damage) : base("Незуко", health, damage) { }

        public override void AdditionalDamage(Fighter fighter)
        {
            Health += _regeneration;
            ShowInfoAbility("Регенерацию");
        }
    }

    class Inosuke : Fighter
    {
        private int _boarAttack = 2;
        private int _angry = 0;
        private int _trueBoarAttack = 2;
        public Inosuke(int health, int damage) : base("Иноске", health, damage) { }

        public override void AdditionalDamage(Fighter fighter)
        {
            if (IsHit == false)
                _angry++;

            if (_angry >= _trueBoarAttack)
            {
                _angry = 0;
                AbilityDamage = Damage * _boarAttack;
                IsAbility = true;
            }
        }
    }
}
