using System;
using System.Collections.Generic;
using System.Linq;
using GameSystem.Models.Characters;

namespace GameSystem.Services
{
    public class GameEngine
    {
        private readonly PlayerDataService dataService = new PlayerDataService();

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

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"\n{currentPlayer.Name}'s Turn");
                Console.WriteLine("1. Attack\n2. Heal\n3. Show Stats\n4. Give Up");
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
                            isRunning = false;
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Unexpected error during turn.");
                }

                if (players.Any(p => p.Health <= 0))
                {
                    var winner = players.First(p => p.Health > 0);
                    Console.WriteLine($"\n{winner.Name} WINS!");
                    isRunning = false;
                }

                (currentPlayer, opponent) = (opponent, currentPlayer);
            }

            TimeSpan duration = DateTime.Now - startTime;
            Console.WriteLine($"\nGame duration: {duration.TotalMinutes:F1} minutes");

            players.ForEach(p => dataService.SavePlayer(p));
            dataService.ShowLeaderboard();
        }
    }
}
