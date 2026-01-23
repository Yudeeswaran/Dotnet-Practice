using System;
using GameSystem.Characters;

namespace GameSystem.Core
{
    // Here is where the game starts, it is the engine of the game
    public class GameEngine
    {
        public void StartGame()
        {
            Console.WriteLine("================================");
            Console.WriteLine(" TURN BASED TWO PLAYER GAME ");
            Console.WriteLine("================================\n");

            Console.Write("Enter Player 1 name: ");
            string? p1Input = Console.ReadLine();
            string p1 = string.IsNullOrWhiteSpace(p1Input) ? "Player 1" : p1Input;

            PlayerCharacter player1 = new PlayerCharacter(p1);

            Console.Write("Enter Player 2 name: ");
            string? p2Input = Console.ReadLine();
            string p2 = string.IsNullOrWhiteSpace(p2Input) ? "Player 2" : p2Input;

            PlayerCharacter player2 = new PlayerCharacter(p2);

            Console.WriteLine("\n--- INITIAL STATS ---");
            player1.DisplayStats();
            Console.WriteLine("--------------------");
            player2.DisplayStats();

            bool isRunning = true;
            PlayerCharacter currentPlayer;
            PlayerCharacter opponent;

            while (isRunning)
            {
                currentPlayer = player1;
                opponent = player2;

                for (int i = 0; i < 2 && isRunning; i++)
                {
                    Console.WriteLine($"\n{currentPlayer.Name}'s Turn");
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Heal");
                    Console.WriteLine("3. Show Stats");
                    Console.WriteLine("4. Give Up");

                    Console.Write("Choose option: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            currentPlayer.Attack(opponent);
                            if (opponent.Health <= 0)
                            {
                                Console.WriteLine($"{currentPlayer.Name} wins!");
                                isRunning = false;
                            }
                            break;

                        case "2":
                            currentPlayer.Heal();
                            break;

                        case "3":
                            currentPlayer.DisplayStats();
                            opponent.DisplayStats();
                            break;

                        case "4":
                            Console.WriteLine($"{currentPlayer.Name} gave up!");
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }

                    // swap turns
                    var temp = currentPlayer;
                    currentPlayer = opponent;
                    opponent = temp;
                }
            }

            Console.WriteLine("\nGame Over.");
        }
    }
}
