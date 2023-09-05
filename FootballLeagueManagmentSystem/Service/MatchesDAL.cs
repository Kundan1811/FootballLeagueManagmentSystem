using FootballLeagueManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FootballLeagueManagmentSystem.Service
{
    public class MatchesDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cse"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter dataAdapter;
        DataTable dt;
       
        public List<Matches> GetFixturesFromDB()
        {
            List<Matches> fixtures = new List<Matches>();

            using (SqlConnection connection = new SqlConnection("Data Source=SNORLAX; Initial Catalog=eFootballLeague; Integrated Security=True"))
            {
                connection.Open();
                string query = "SELECT * FROM Matches";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Matches fixture = new Matches();
                        fixture.MatchId = Convert.ToInt32(reader.GetValue(4).ToString());
                        fixture.HomeTeamName = reader.GetValue(2).ToString();
                        fixture.AwayTeamName = reader.GetValue(3).ToString();
                        fixture.HomeTeamScore = Convert.ToInt32(reader.GetValue(5).ToString());
                        fixture.AwayTeamScore = Convert.ToInt32(reader.GetValue(6).ToString());

                        //MatchId = Convert.ToInt32(reader["MatchId"]),
                        //HomeTeamId = Convert.ToInt32(reader["HomeTeamId"]),
                        //AwayTeamId = Convert.ToInt32(reader["AwayTeamId"]),
                        //HomeTeamName = Convert.ToString(reader["HomeTeamName"])

                        fixtures.Add(fixture);
                    }
                }
            }

            return fixtures;
        }


        public List<Matches> GenerateFixtures(List<Teams> teams)
        {
            List<Matches> fixtures = new List<Matches>();
            int numTeams = teams.Count;

            for (int i = 0; i <= numTeams-1; i++)
            {
                for (int j = 0; j <= numTeams-1; j++)
                {
                    if (i != j)
                    {
                        Matches fixture = new Matches
                        {
                           
                            HomeTeamId = teams[i].TeamId,
                            AwayTeamId = teams[j].TeamId,
                            HomeTeamName = teams[i].TeamName,
                            AwayTeamName = teams[j].TeamName,
                            HomeTeamScore =0,
                            AwayTeamScore = 0



                        };
                        SaveFixturesToDatabase(fixture);
                        fixtures.Add(fixture);
                    }

                }
            }

            return fixtures;
        }

        public void SaveFixturesToDatabase(Matches fixtures)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=SNORLAX; Initial Catalog=eFootballLeague; Integrated Security=True"))
            {
                connection.Open();

                
                    string query = "INSERT INTO Matches(HomeTeamId, AwayTeamId, HomeTeamName, AwayTeamName,HomeTeamScore,AwayTeamScore)  VALUES (@HomeTeamId, @AwayTeamId, @HomeTeamName, @AwayTeamName,@HomeTeamScore,@AwayTeamScore)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HomeTeamId", fixtures.HomeTeamId);
                        command.Parameters.AddWithValue("@AwayTeamId", fixtures.AwayTeamId);
                        command.Parameters.AddWithValue("@HomeTeamName", fixtures.HomeTeamName);
                        command.Parameters.AddWithValue("@AwayTeamName", fixtures.AwayTeamName);
                        command.Parameters.AddWithValue("@HomeTeamScore", fixtures.HomeTeamScore);
                        command.Parameters.AddWithValue("@AwayTeamScore", fixtures.AwayTeamScore);


                        command.ExecuteNonQuery();
                    }
                
            }
        }

       

       
    }
}