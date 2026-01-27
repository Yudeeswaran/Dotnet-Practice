namespace GameSystem.Core
{
    // Represents any character that can participate in combat
    public interface ICombatant
    {
        void Attack(GameSystem.Characters.GameCharacter target);
        int Health { get; }
    }
}