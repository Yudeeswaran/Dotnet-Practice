using System;
using System.Collections.Generic;
using System.Linq;
using GameSystem.Models.Characters;

namespace GameSystem.Services
{
    public class GameEngine
    {
        private readonly PlayerDataService _dataService;

        private readonly string _connectionString;

        public GameEngine(string connectionString)
        {
            _connectionString = connectionString;
            _dataService = new PlayerDataService(connectionString);
        }

        public void StartGame()
        {
            DateTime startTime = DateTime.Now;

            Console.Write("Enter Player 1 name: ");
            string p1 = Console.ReadLine() ?? "Player 1";

            Console.Write("Enter Player 2 name: ");
            string p2 = Console.ReadLine() ?? "Player 2";

            PlayerCharacter player1 = new PlayerCharacter(p1);
            PlayerCharacter player2 = new PlayerCharacter(p2);

            List<PlayerCharacter> players = new() { player1, player2 };

            PlayerCharacter currentPlayer = player1;
            PlayerCharacter opponent = player2;

            PlayerCharacter? winner = null;

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"\n{currentPlayer.Name}'s Turn");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Heal");
                Console.WriteLine("3. Show Stats");
                Console.WriteLine("4. Give Up");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out int input) ||
                    !Enum.IsDefined(typeof(PlayerAction), input))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                PlayerAction action = (PlayerAction)input;

                try
                {
                    switch (action)
                    {
                        case PlayerAction.Attack:
                            currentPlayer.Attack(opponent);
                            break;

                        case PlayerAction.Heal:
                            currentPlayer.Heal();
                            break;

                        case PlayerAction.ShowStats:
                            players.ForEach(p => p.DisplayStats());
                            continue;

                        case PlayerAction.GiveUp:
                            Console.WriteLine($"{currentPlayer.Name} gave up!");
                            winner = opponent;   // Correct winner on surrender
                            isRunning = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                // Check if someone died
                if (players.Any(p => p.Health <= 0))
                {
                    winner = players.First(p => p.Health > 0);
                    Console.WriteLine($"\n{winner.Name} WINS!");
                    isRunning = false;
                }

                // Swap turns only if game still running
                if (isRunning)
                {
                    (currentPlayer, opponent) = (opponent, currentPlayer);
                }
            }

            TimeSpan duration = DateTime.Now - startTime;
            Console.WriteLine($"\nGame duration: {duration.TotalMinutes:F1} minutes");

            // Save match ONLY if winner exists
            if (winner != null)
            {
                _dataService.SaveMatch(startTime, DateTime.Now, winner, players);
            }

            _dataService.ShowLeaderboard();
        }
    }
}
