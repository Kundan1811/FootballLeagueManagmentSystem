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
    public class LeagueTableController : Controller
    {
        LeagueTableDAL tableDAL = new LeagueTableDAL();
        MatchesDAL matchesDAL = new MatchesDAL();

        public ActionResult Index()
        {
            List<LeagueTable> leagueTable = tableDAL.CalculateLeagueTable();
            return View(leagueTable);
        }
    }
}


















