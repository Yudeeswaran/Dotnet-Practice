using System;

namespace GameSystem.Characters
{
    public class PlayerCharacter : GameCharacter
    {
        private int healCount;

        // use constructor chaining .. cause we dont really have the idea about name which is present in the constructor of base class
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
                Console.WriteLine("No heals left!");
            }
        }
    }
}
