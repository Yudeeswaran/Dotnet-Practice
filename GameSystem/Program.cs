using GameSystem.Core;

namespace GameSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create game engine
            GameEngine engine = new GameEngine();

            // Start the game
            engine.StartGame();
        }
    }
}