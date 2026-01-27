using System;
using GameSystem.Core;

namespace GameSystem.Characters
{
    // Abstract class acts as a template for all characters
    public abstract class GameCharacter : ICombatant
    {
        // Encapsulation
        protected string name;
        protected int health;
        protected int attackPower;

        // Base constructor (forces valid initialization)
        protected GameCharacter(string name, int health, int attackPower)
        {
            this.name = name;
            this.health = health;
            this.attackPower = attackPower;
        }

        // Read-only properties
        public string Name
        {
            get { return name; }
        }

        public int Health
        {
            get { return health; }
        }

        // Virtual method (polymorphism)
        public virtual void Attack(GameCharacter target)
        {
            Console.WriteLine($"{name} attacks {target.name} for {attackPower} damage");
            target.TakeDamage(attackPower);
        }

        protected void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
                health = 0;
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine($"Name   : {name}");
            Console.WriteLine($"Health : {health}");
            Console.WriteLine($"Attack : {attackPower}");
        }
    }
}