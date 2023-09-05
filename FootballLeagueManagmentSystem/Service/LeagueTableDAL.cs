using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using FootballLeagueManagmentSystem.Models;
using System.Web.Mvc;

namespace FootballLeagueManagmentSystem.Service
{
    public class LeagueTableDAL
    {
        public List<LeagueTable> CalculateLeagueTable()
        {
            List<LeagueTable> leagueTable = new List<LeagueTable>();

            using (SqlConnection connection = new SqlConnection("Data Source=SNORLAX; Initial Catalog=eFootballLeague; Integrated Security=True"))
            {
                connection.Open();

                string query = @"
                    SELECT t.TeamId, t.TeamName,
                        COUNT(m.MatchId) AS Played,
                        SUM(CASE WHEN m.HomeTeamScore > m.AwayTeamScore THEN 1 ELSE 0 END) AS Win,
                        SUM(CASE WHEN m.HomeTeamScore < m.AwayTeamScore THEN 1 ELSE 0 END) AS Loss,
                        SUM(CASE WHEN m.HomeTeamScore = m.AwayTeamScore THEN 1 ELSE 0 END) AS Draw,
                        SUM(m.HomeTeamScore) AS GS,
                        SUM(m.AwayTeamScore) AS GA,
                        SUM(m.HomeTeamScore) - SUM(m.AwayTeamScore) AS GD,
                        SUM(CASE WHEN m.HomeTeamScore > m.AwayTeamScore THEN 3 WHEN m.HomeTeamScore = m.AwayTeamScore THEN 1 ELSE 0 END) AS Points
                    FROM Teams t
                    LEFT JOIN Matches m ON t.TeamId = m.HomeTeamId OR t.TeamId = m.AwayTeamId
                    GROUP BY t.TeamId, t.TeamName
                    ORDER BY Points DESC, GD DESC";


                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LeagueTable row = new LeagueTable
                        {
                            TeamId = Convert.ToInt32(reader["TeamId"]),
                            TeamName = reader["TeamName"].ToString(),
                            Played = Convert.ToInt32(reader["Played"]),
                            Win = Convert.ToInt32(reader["Win"]),
                            Loss = Convert.ToInt32(reader["Loss"]),
                            Draw = Convert.ToInt32(reader["Draw"]),
                            GS = Convert.ToInt32(reader["GS"]),
                            GA = Convert.ToInt32(reader["GA"]),
                            GD = Convert.ToInt32(reader["GD"]),
                            Points = Convert.ToInt32(reader["Points"])
                        };
                        leagueTable.Add(row);
                    }
                }
            }

            return leagueTable;
        }
    }
}