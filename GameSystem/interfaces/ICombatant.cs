using GameSystem.Models.Characters;

namespace GameSystem.interfaces
{
    public interface ICombatant
    {
        string Name { get; }
        int Health { get; }
        void Attack(GameCharacter target);
    }
}
