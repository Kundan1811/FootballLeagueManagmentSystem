using FootballLeagueManagmentSystem.Models;
using FootballLeagueManagmentSystem.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballLeagueManagmentSystem.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {

        MatchesDAL matchesDAL=new MatchesDAL();
        TeamDAL teamDAL=new TeamDAL();  
        public ActionResult MatchesIndex()
        {
           
            var data = matchesDAL.GetFixturesFromDB();
            return View(data);
        }
   
        public ActionResult AddMatchResult()
        {
          return View();
        }
        [HttpPost]
        public ActionResult AddMatchResult(int matchId, int homeTeamScore, int awayTeamScore)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=SNORLAX; Initial Catalog=eFootballLeague; Integrated Security=True"))
                {
                    connection.Open();

                    string query = "UPDATE Matches SET HomeTeamScore = @HomeTeamScore, AwayTeamScore = @AwayTeamScore WHERE MatchId = @MatchId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MatchId", matchId);
                        command.Parameters.AddWithValue("@HomeTeamScore", homeTeamScore);
                        command.Parameters.AddWithValue("@AwayTeamScore", awayTeamScore);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ViewBag.Message = "Match result added successfully.";
                        }
                        else
                        {
                            ViewBag.Message = "Failed to add match result.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
            }

            return View();
        }
        public ActionResult GenerateFixtures()
        {

            matchesDAL.GenerateFixtures(teamDAL.GetTeams());
            return RedirectToAction("MatchesIndex","Matches");
        }


    }
}