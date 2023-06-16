using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace BrawlbotsMVC.Models
{
    public class RobotData
    {
        private string connectionString = "Server=127.0.0.1;Database=battlebots_db;User=root;Password=;AllowUserVariables=true;";

        public int id { get; set; }
        public string name { get; set; }
        public string Weapon { get; set; }
        public string type_of_movement { get; set; }

        public string weight_class { get; set; }

        public string team_name { get; set; }
        public int win_count { get; set; }
        public int loss_count { get; set; }

        public bool Deleted { get; set; }


        public List<RobotData> FetchAll()
        {

            List<RobotData> returnList = new List<RobotData>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT `id`,`name`,`weapon`,`type_of_movement`,`weight_class`,`team_name`,`win_count`,`loss_count` FROM robots WHERE  Deleted  =  0";
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RobotData robot = new RobotData();
                        robot.id = reader.GetInt32(0);
                        robot.name = reader.GetString(1);
                        robot.Weapon = reader.GetString(2);
                        robot.type_of_movement = reader.GetString(3);
                        robot.weight_class = reader.GetString(4);
                        robot.team_name = reader.GetString(5);
                        robot.win_count = reader.GetInt32(6);
                        robot.loss_count = reader.GetInt32(7);


                        returnList.Add(robot);
                    }
                }
            }

            return returnList;
        }


        public RobotData FetchSingleRobot(int id )
        {

            RobotData robot = new RobotData();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                string sqlQuery = "SELECT `id`,`name`,`weapon`,`type_of_movement`,`weight_class`,`team_name` FROM robots WHERE Deleted  =  0 AND id = " + id;
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

               

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        robot.id = reader.GetInt32(0);
                        robot.name = reader.GetString(1);
                        robot.Weapon = reader.GetString(2);
                        robot.type_of_movement = reader.GetString(3);
                        robot.weight_class = reader.GetString(4);
                        robot.team_name = reader.GetString(5);
                    }
                }
                return robot;
            }  
        }

        public int CreateRobot(RobotData CreateRobot)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                string name = CreateRobot.name;
                string Weapon = CreateRobot.Weapon;
                string type_of_movement = CreateRobot.type_of_movement;
                string weight_class = CreateRobot.weight_class;
                string team_name = CreateRobot.team_name;
                int id = CreateRobot.id;
                string sqlQuery = "";

                sqlQuery = "INSERT INTO robots (`name`,Weapon,type_of_movement,weight_class,team_name) values(  '" + name + "','" + Weapon + "','" + type_of_movement + "','" + weight_class + "','" + team_name + "')";


                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
             
                connection.Open();
                int newID = command.ExecuteNonQuery();
                return newID;
            }

        }



           public bool deleteRobot(int id)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string sqlQuery = "update  robots set Deleted  = 1  where  id = " + id;
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

        public int UpdateRobotData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
               
                string sqlQuery = "UPDATE robots set name  = @name, Weapon =   @Weapon, type_of_movement  =  @type_of_movement, weight_class  = @weight_class, team_name = @team_name  WHERE  id  = " + id;
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@weapon", Weapon);
                    command.Parameters.AddWithValue("@type_of_movement", type_of_movement);
                    command.Parameters.AddWithValue("@weight_class", weight_class);
                    command.Parameters.AddWithValue("@team_name", team_name);
                    int newID = command.ExecuteNonQuery();
                    return newID;
                }
            }
        }
    }
}
