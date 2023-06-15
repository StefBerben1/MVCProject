using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;

namespace BrawlbotsMVC.Models
{
    public class WedstrijdenData
    {
        private string connectionString = "Server=127.0.0.1;Database=battlebots_db;User=root;Password=;AllowUserVariables=true;";


        public int id { get; set; }
        public string arena_name { get; set; }
       
        public DateTime match_date { get; set; }

        public int winner_id { get; set; }
        public int loser_id { get; set; }

        public int win_count { get; set; }
        public int loss_count { get; set; }




        public List<WedstrijdenData> FetchAllMatchData()
        {

            List<WedstrijdenData> returnList = new List<WedstrijdenData>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT `id`,`arena_name`,`match_date`,`winner_id`,`loser_id` FROM matches WHERE  Deleted  =  0";
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WedstrijdenData wedstrijden = new WedstrijdenData();
                        wedstrijden.id = reader.GetInt32(0);
                        wedstrijden.arena_name = reader.GetString(1);
                        wedstrijden.match_date = reader.GetDateTime(2);
                        wedstrijden.winner_id = reader.GetInt32(3);
                        wedstrijden.loser_id = reader.GetInt32(4);
                        returnList.Add(wedstrijden);
                    }
                    reader.Close();
                }
            }

            return returnList;
            
        }


        public int CreateMatch(WedstrijdenData wedstrijden)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                string arena_name = wedstrijden.arena_name;
                DateTime match_date = wedstrijden.match_date;
                string match_dateConverted = match_date.ToString("yyyy-MM-dd HH:mm:ss");

                string sqlQuery = "";
                int winner_id = 0;
                int loser_id = 0;
                int win_count = 0;
                int loss_count = 0;
                int win_countLoser = 0;
                int loss_countLoser = 0;

                sqlQuery = "SELECT id FROM `robots` WHERE Deleted = 0 ORDER BY RAND();";
                connection.Open();
                MySqlCommand randomDBRow = new MySqlCommand(sqlQuery, connection);
                winner_id = Convert.ToInt32(randomDBRow.ExecuteScalar());

                sqlQuery = "SELECT id FROM `robots` WHERE Deleted = 0 AND id != " + winner_id + ";";
                MySqlCommand randomWinnerID = new MySqlCommand(sqlQuery, connection);
                loser_id = Convert.ToInt32(randomWinnerID.ExecuteScalar());



                sqlQuery = "INSERT INTO matches (`arena_name`,match_date,winner_id,loser_id) values(  '" + arena_name + "','" + match_dateConverted + "','" + winner_id + "','" + loser_id + "')";

                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                int newID = command.ExecuteNonQuery();
                connection.Close();

                sqlQuery = "SELECT `win_count`,`loss_count` FROM robots WHERE Deleted = 0 AND id = " + winner_id + ";";
                connection.Open();
                MySqlCommand robotMatchDataWinner = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader reader = robotMatchDataWinner.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       
                       win_count = reader.GetInt32(0);
                       loss_count = reader.GetInt32(1);
                       win_count = win_count + 1;
                      
                       
                    }
                    reader.Close();
                }

                connection.Close();
                sqlQuery = "SELECT `win_count`,`loss_count` FROM robots WHERE Deleted = 0 AND id = " + loser_id + ";";
                connection.Open();
                MySqlCommand robotMatchDataLoser = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader readerLoser = robotMatchDataLoser.ExecuteReader();
                if (readerLoser.HasRows)
                {
                    while (readerLoser.Read())
                    {

                        win_countLoser = readerLoser.GetInt32(0);
                        loss_countLoser = readerLoser.GetInt32(1);
                        loss_countLoser = loss_countLoser + 1;

                    }
                    reader.Close();
                }
                connection.Close();
                sqlQuery = "UPDATE robots SET win_count = " + win_count + "," + " loss_count = " + loss_count + "" + " WHERE id = " + winner_id + ";";
                connection.Open();
                MySqlCommand insertMatchResultWinner = new MySqlCommand(sqlQuery, connection);
                int test = insertMatchResultWinner.ExecuteNonQuery();

                sqlQuery = "UPDATE robots SET win_count = " + win_countLoser + "," + " loss_count = " + loss_countLoser + "" + " WHERE id = " + winner_id + ";";

                MySqlCommand insertMatchResultLoser = new MySqlCommand(sqlQuery, connection);
                int test1 = insertMatchResultLoser.ExecuteNonQuery();

                connection.Close();
                return newID;
                
            }

        }

        public bool deleteMatch(int id)
               {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "update  matches set Deleted  = 1  where  id = " + id;
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                connection.Open();
                int newID = command.ExecuteNonQuery();
                if (newID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<WedstrijdenData> FetchLeaderbord()
        {

            List<WedstrijdenData> returnList = new List<WedstrijdenData>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT `id`,`arena_name`,`match_date`,`winner_id`,`loser_id` FROM matches WHERE  Deleted  =  0";
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WedstrijdenData wedstrijden = new WedstrijdenData();
                        wedstrijden.id = reader.GetInt32(0);
                        wedstrijden.arena_name = reader.GetString(1);
                        wedstrijden.match_date = reader.GetDateTime(2);
                        wedstrijden.winner_id = reader.GetInt32(3);
                        wedstrijden.loser_id = reader.GetInt32(4);
                        returnList.Add(wedstrijden);
                    }
                    reader.Close();
                }
            }

            return returnList;

        }
    }







}
