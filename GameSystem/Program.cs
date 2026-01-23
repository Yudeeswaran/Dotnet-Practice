using GameSystem.Core;

namespace GameSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            // the main object is created
            GameEngine engine = new GameEngine();

            // the function to start the game is called
            engine.StartGame();
        }
    }
}
