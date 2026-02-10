using System;

namespace GameSystem.Models.Characters
{
    public class PlayerCharacter : GameCharacter
    {
        private int healCount = 3;

        public PlayerCharacter(string name)
            : base(name, 100, 15) { }

        public override void Attack(GameCharacter target)
        {
            bool isCritical = random.Next(1, 101) <= 20;
            int damage = isCritical ? attackPower * 2 : attackPower;

            Console.WriteLine(isCritical
                ? $"{name} lands a CRITICAL HIT!"
                : $"{name} attacks normally.");

            target.TakeDamage(damage);
        }

        public void Heal()
        {
            if (healCount <= 0)
            {
                Console.WriteLine($"{name} has no heals left!");
                return;
            }

            health = Math.Min(maxHealth, health + 20);
            healCount--;

            Console.WriteLine($"{name} healed. Remaining heals: {healCount}");
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine($"Heals left: {healCount}");
        }
    }
}
