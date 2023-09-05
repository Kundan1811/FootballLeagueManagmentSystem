using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FootballLeagueManagmentSystem.Models
{
    public class Teams
    {
        [Display(Name = "User Id")]
        public int TeamId { get; set; }
        [Required(ErrorMessage = "Team Name is Mandatory")]
        [Display(Name = "User Team Name")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Player Name  is Mandatory")]
        [Display(Name = "Player Name ")]   
        public string PlayerName { get; set; }
      
      
    }
}