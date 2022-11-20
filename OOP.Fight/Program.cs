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

                leftFighter.AdditionalDamage(leftFighter.Description, leftFighter.TrueAbilityChance, leftFighter.Ability);
                rightFighter.AdditionalDamage(rightFighter.Description, rightFighter.TrueAbilityChance, rightFighter.Ability);
                leftFighter.TakeDamage(rightFighter.Damage, rightFighter.AbilityDamage, rightFighter, rightFighter.IsAbility);
                rightFighter.TakeDamage(leftFighter.Damage, leftFighter.AbilityDamage, leftFighter, leftFighter.IsAbility);

                leftFighter.ShowInfo();
                rightFighter.ShowInfo();
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    class Fighter
    {
        protected int TrueHitChance = 7;
        protected bool _isHit;
        protected string Name;
        public string Description { get; protected set; }
        public int TrueAbilityChance { get; protected set; }
        public int Ability { get; protected set; }
        public bool IsAbility { get; private set; }

        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int AbilityDamage { get; private set; }

        public Fighter(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public virtual int AdditionalDamage(string descriptionAbility, int trueAbilityChance, int ability)
        {
            Random random = new Random();
            int maxChance = 10;
            int abilityChance = random.Next(maxChance);
            IsAbility = false;

            if (abilityChance < trueAbilityChance)
            {
                AbilityDamage = Damage;
                AbilityDamage += ability;
                IsAbility = true;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Name} использовал {descriptionAbility}");
                Console.ForegroundColor = ConsoleColor.White;

                return AbilityDamage;
            }
            else
            {
                IsAbility = false;
                return Damage;
            }
        }

        public void TakeDamage(int damage, int abilityDamage, Fighter fighter, bool isAbility)
        {


            Random random = new Random();
            int maxHitChance = 10;
            int hitChance = random.Next(maxHitChance);

            if (hitChance > TrueHitChance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{fighter.Name} промахнулся");
                Console.ForegroundColor = ConsoleColor.White;

                _isHit = false;
            }
            else if (isAbility)
            {
                _isHit = true;
                Health -= abilityDamage;
            }
            else
            {
                _isHit = true;
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
        public Tanjiro(int health, int damage) : base("Танзиро", health, damage) { }

        public override int AdditionalDamage(string description, int trueAbilityChance, int ability)
        {
            Description = "Танец огня";
            TrueAbilityChance = 3;
            Ability = 5;
            return base.AdditionalDamage(Description, TrueAbilityChance, Ability);

        }

    }

    class Zenitsu : Fighter
    {
        private int _trueSleep = 3;

        public Zenitsu(int health, int damage) : base("Зеницу", health, damage) { }

        public override int AdditionalDamage(string description, int trueAbilityChance, int ability)
        {
            Description = "Сон";
            TrueAbilityChance = 3;
            Ability = 7;
            return base.AdditionalDamage(Description, TrueAbilityChance, Ability);
        }

        //public Zenitsu(int health, int damage) : base("Зетицу", health, damage)
        //{
        //    int anger = 0;

        //    if (_isHit)
        //    {
        //        anger++;
        //    }

        //    if (anger == _trueSleep)
        //    {
        //        Sleep(damage);
        //        anger = 0;
        //    }
        //}

        //private void Sleep(int damage)
        //{
        //    double sleep = 2;
        //    damage *= (int)sleep;
        //}
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
