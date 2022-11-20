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
        Fighter[] fighters = { new Tanjiro(100, 20), new Zenitsu(100, 20) };
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

                leftFighter.AdditionalDamage();
                rightFighter.AdditionalDamage();
                leftFighter.TakeDamage(rightFighter.Damage, rightFighter.AbilityDamage, leftFighter, rightFighter.IsAbility, rightFighter.IsHit);
                rightFighter.TakeDamage(leftFighter.Damage, leftFighter.AbilityDamage, rightFighter, leftFighter.IsAbility, leftFighter.IsHit);

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
        protected int TrueHitChance = 7;
        public bool IsHit { get; protected set; } = true;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int TrueAbilityChance { get; protected set; }
        public int Ability { get; protected set; }
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

        public virtual int AdditionalDamage()
        {
            return 0;
        }

        public void ShowInfoAbility(string descriptionAbility)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} использовал {descriptionAbility}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void TakeDamage(int damage, int abilityDamage, Fighter fighter, bool isAbility, bool isHit)
        {
            Random random = new Random();
            int maxHitChance = 10;
            int hitChance = random.Next(maxHitChance);

            if (hitChance > TrueHitChance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{fighter.Name} промахнулся");
                Console.ForegroundColor = ConsoleColor.White;

                isHit = false;
            }
            else if (isAbility)
            {
                IsHit = true;
                Health -= abilityDamage;
            }
            else
            {
                IsHit = true;
                Health -= damage;
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

        public override int AdditionalDamage()
        {
            Random random = new Random();
            int maxChance = 10;
            int abilityChance = random.Next(maxChance);
            IsAbility = false;

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
                return Damage;
            }
        }

    }

    class Zenitsu : Fighter
    {
        private int _trueSleep = 1;
        public Zenitsu(int health, int damage) : base("Зеницу", health, damage) { }

        public override int AdditionalDamage()
        {
            IsAbility = false;

            if (IsHit == false) // если он промахивается, то cледующий удар + 10 урона
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

    class Kanao
    {

    }

    class Nezuko
    {

    }

    class Inosuke
    {

    }
}
