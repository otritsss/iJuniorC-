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
        Fighter[] fighters = { new Tanjiro(), new Zenitsu(), new Kanao(), new Nezuko(), new Inosuke() };

        public Fighter ChoiceFighter(string text)
        {
            Fighter fighter = new Fighter("No name", 0, 0);
            Console.Write($"Введите номер бойнца {text} ринга: ");

            if (int.TryParse(Console.ReadLine(), out int numberFighter))
            {
                Console.WriteLine("Вы ввели некорретное значение");
            }
            else
            {
                switch (numberFighter)
                {
                    case 1:
                        return fighter = new Tanjiro();
                        break;
                    case 2:
                        return fighter = new Zenitsu();
                        break;
                    case 3:
                        return fighter = new Kanao();
                        break;
                    case 4:
                        return fighter = new Tanjiro();
                        break;
                    case 5:
                        return fighter = new Inosuke();
                        break;
                    default:
                        Console.WriteLine("Вы ввели некорректное значение");
                        break;
                }
            }

            return fighter;

        }
        public void Battle()
        {
            for (int i = 0; i < fighters.Length; i++)
            {
                Console.Write($"{i + 1}. ");
                fighters[i].ShowInfo();
            }

            Fighter leftFighter = ChoiceFighter("левого");
            Fighter rightFighter = ChoiceFighter("правого");

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

        public virtual int AdditionalDamage(Fighter fighter) => 0;

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

            if (Health < 0)
                Health = 0;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} | Здоровье - {Health} | Урон - {Damage}");
        }
    }

    class Tanjiro : Fighter
    {
        private int _trueDanceOfFire = 3;
        public Tanjiro() : base("Танзиро", 100, 20) { }

        public override int AdditionalDamage(Fighter fighter)
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

        public override int AdditionalDamage(Fighter fighter)
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
        public Kanao() : base("Канао", 100, 200) { }

        public override int AdditionalDamage(Fighter fighter) // если она в прошлый раз промахнулась, то в следующий раз точно попадает и с каждым ударом прибавляется по 1 ед. урона
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

        public override int AdditionalDamage(Fighter fighter)
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
        private int _trueBoarAttack = 2;
        public Inosuke() : base("Иноске", 100, 20) { }

        public override int AdditionalDamage(Fighter fighter)
        {
            if (IsHit == false)
                _angry++;

            if (_angry >= _trueBoarAttack)
            {
                _angry = 0;
                AbilityDamage = Damage * _boarAttack;
                IsAbility = true;
                return AbilityDamage;
            }

            return Damage;
        }
    }
}