using System;
using Microsoft.Data.SqlClient;
using GameSystem.Models.Characters;

namespace GameSystem.Services
{
    public class PlayerDataService
    {
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=TestDB;Trusted_Connection=True;";

        public void SavePlayer(PlayerCharacter player)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(ConnectionString);
                using SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Players (Name, Health) VALUES (@name, @health)",
                    conn);

                cmd.Parameters.AddWithValue("@name", player.Name);
                cmd.Parameters.AddWithValue("@health", player.Health);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.WriteLine("Database error while saving player.");
            }
        }

        public void ShowLeaderboard()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(ConnectionString);
                using SqlCommand cmd = new SqlCommand(
                    "SELECT Name, Health FROM Players ORDER BY Health DESC",
                    conn);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- Leaderboard ---");
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"{reader.GetString(0)} | Health: {reader.GetInt32(1)}");
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Database error while loading leaderboard.");
            }
        }
    }
}
