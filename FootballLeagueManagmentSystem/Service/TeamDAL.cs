using FootballLeagueManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace FootballLeagueManagmentSystem.Service
{
    public class TeamDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cse"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter dataAdapter;
        DataTable dt;
        public List<Teams> GetTeams()
        {
            cmd = new SqlCommand("sp_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            dataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            dataAdapter.Fill(dt);
            List<Teams> list = new List<Teams>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Teams
                {
                   TeamId = Convert.ToInt32(dr["TeamId"]),
                    TeamName = dr["TeamName"].ToString(),
                    PlayerName = dr["PlayerName"].ToString()
                });
            }
            return list;
        }
        public bool InsertTeams(Teams team)
        {
            try
            {
                cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tname", team.TeamName);
                cmd.Parameters.AddWithValue("@pname", team.PlayerName);
               
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool UpdateTeams(Teams team)
        {
            try
            {
                cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tname", team.TeamName);
                cmd.Parameters.AddWithValue("@pname", team.PlayerName);             
                cmd.Parameters.AddWithValue("@id", team.TeamId);

                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int DeleteTeams(int id)
        {
            try
            {
                cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}