using FootballLeagueManagmentSystem.Models;
using FootballLeagueManagmentSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballLeagueManagmentSystem.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        TeamDAL teamDAL = new TeamDAL();
        // GET: User
        public ActionResult List()
        {
            var data = teamDAL.GetTeams();
            return View(data);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult Create(Teams team)
        {
            if (teamDAL.InsertTeams(team))
            {
                TempData["InsertMsg"] = "<script>alert('User saved successfull..') </script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["InsertErrorMsg"] = "<script>alert('Data not saved..') </script>";

            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var data = teamDAL.GetTeams().Find(a => a.TeamId == id);
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var data = teamDAL.GetTeams().Find(a => a.TeamId == id);
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Teams team)
        {
            if (teamDAL.UpdateTeams(team))
            {
                TempData["UpdateMsg"] = "<script>alert('User Updated successfull..') </script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["UpdateErrorMsg"] = "<script>alert('Data not updated..') </script>";

            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            int r = teamDAL.DeleteTeams(id);
            if (r > 0)
            {
                TempData["DeleteMsg"] = "<script>alert('User Deleted successfull..') </script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["DeleteErrorMsg"] = "<script>alert('Data not deleted..') </script>";

            }
            return View();
        }
    }
}