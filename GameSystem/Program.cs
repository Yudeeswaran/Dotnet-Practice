using GameSystem.Services;

namespace GameSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Server=(localdb)\\MSSQLLocalDB;Database=TestDB;Integrated Security=true;TrustServerCertificate=true;";

            DatabaseInitializer.Initialize(connectionString);

            GameEngine engine = new GameEngine(connectionString);
            engine.StartGame();
        }

    }
}
