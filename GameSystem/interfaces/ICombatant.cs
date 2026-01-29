namespace GameSystem.interfaces
{
    // Represents any character that can participate in combat
    public interface ICombatant
    {
        void Attack(GameSystem.Models.Characters.GameCharacter target);
        int Health { get; }
    }
}