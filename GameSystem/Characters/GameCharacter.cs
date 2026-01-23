using System;

namespace GameSystem.Characters
{

    // Abstract CLass - This hides the implementation, Cause its just a template and we dont need any implementation for it.
    public abstract class GameCharacter
    {

        // encapsulation 
        protected string name;
        protected int health;
        protected int attackPower;

        // Base constructor
        protected GameCharacter(string name, int health, int attackPower)
        {
            this.name = name;
            this.health = health;
            this.attackPower = attackPower;
        }

        // getter - properties
        public string Name
        {
            get
            {
                return name;
            }
        }


        public int Health
        {
            get
            {
                return health;
            }
        }

        // Overriding
        public virtual void Attack(GameCharacter target)
        {
            Console.WriteLine($"{name} attacks {target.name} for {attackPower} damage");
            target.TakeDamage(attackPower);
        }

        protected void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0) health = 0;
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine($"Name   : {name}");
            Console.WriteLine($"Health : {health}");
            Console.WriteLine($"Attack : {attackPower}");
        }
    }
}
