using System;

namespace GameSystem.Characters
{
    public class PlayerCharacter : GameCharacter
    {
        private int healCount;

        // Constructor chaining
        public PlayerCharacter(string name)
            : base(name, 100, 15)
        {
            healCount = 3;
        }

        public PlayerCharacter(string name, int health, int attackPower)
            : base(name, health, attackPower)
        {
            healCount = 3;
        }

        //Special ability that will be triggered randomly at a chance rate of 20 percent.

        private Random random = new Random();


        public override void Attack(GameCharacter target)
        {
            bool isCritical = random.Next(1, 101) <= 20; // 20% chance


            int damage = isCritical ? attackPower * 2 : attackPower;


            Console.WriteLine(isCritical
            ? $"{name} lands a CRITICAL HIT!"
            : $"{name} attacks normally.");


            target.TakeDamage(damage);
        }

        // Player-specific ability
        public void Heal()
        {
            if (healCount > 0)
            {
                health += 20;
                healCount--;
                Console.WriteLine($"{name} healed. Remaining heals: {healCount}");
            }
            else
            {
                Console.WriteLine($"{name} has no heals left!");
            }
        }

        // Polymorphic behavior
        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine($"Heals  : {healCount}");
        }
    }
}