using GameSystem.Services;

namespace GameSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine engine = new GameEngine();
            engine.StartGame();
        }
    }
}
