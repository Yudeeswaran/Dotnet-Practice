using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using GameSystem.Models.Characters;

namespace GameSystem.Services
{
    public class PlayerDataService
    {
        private readonly string _connectionString;

        // Constructor Injection
        public PlayerDataService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveMatch(
            DateTime start,
            DateTime end,
            PlayerCharacter winner,
            List<PlayerCharacter> players)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                int matchId;

                // 1️⃣ Insert Match using SP
                using (SqlCommand cmd = new SqlCommand("dbo.InsertMatch", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StartedAt", start);
                    cmd.Parameters.AddWithValue("@EndedAt", end);
                    cmd.Parameters.AddWithValue("@WinnerName", winner.Name);

                    matchId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2️⃣ Insert MatchPlayers
                foreach (var player in players)
                {
                    using SqlCommand cmd = new SqlCommand("dbo.InsertMatchPlayer", conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MatchId", matchId);
                    cmd.Parameters.AddWithValue("@PlayerName", player.Name);
                    cmd.Parameters.AddWithValue("@FinalHealth", player.Health);

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                Console.WriteLine("Match saved successfully.");
            }
            catch (SqlException ex)
            {
                transaction.Rollback();

                Console.WriteLine("SQL ERROR:");
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowLeaderboard()
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            using SqlCommand cmd = new SqlCommand("dbo.GetLeaderboard", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- Leaderboard ---");

                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int played = reader.GetInt32(1);
                    int won = reader.GetInt32(2);

                    double winPercent = played == 0
                        ? 0
                        : (double)won / played * 100;

                    Console.WriteLine(
                        $"{name} | Played: {played} | Won: {won} | Win%: {winPercent:F1}%");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL ERROR:");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
