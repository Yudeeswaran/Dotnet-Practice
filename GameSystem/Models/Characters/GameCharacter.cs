using System;
using GameSystem.interfaces;

namespace GameSystem.Models.Characters
{
    public abstract class GameCharacter : ICombatant
    {
        protected static readonly Random random = new Random();

        protected string name;
        protected int health;
        protected int attackPower;
        protected int maxHealth;

        protected GameCharacter(string name, int health, int attackPower)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.attackPower = attackPower;
        }

        public string Name => name;
        public int Health => health;

        public virtual void Attack(GameCharacter target)
        {
            Console.WriteLine($"{name} attacks {target.name} for {attackPower}");
            target.TakeDamage(attackPower);
        }

        public virtual void TakeDamage(int damage)
        {
            bool isBlocked = random.Next(1, 101) <= 50;

            if (isBlocked)
            {
                Console.WriteLine($"{name} blocked the attack!");
                return;
            }

            health = Math.Max(0, health - damage);
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"Name: {name} | Health: {health}/{maxHealth} | Attack: {attackPower}";
        }
    }
}
