using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Net;

namespace LMAOnline_Tokens.Config
{
    public class Functions
    {
        MySQLh sql = new MySQLh();

        public void saveToken(string token, string creator, string days)
        {
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "INSERT INTO `tokens` (`token`, `days`, `creator`) VALUES (@token, @days, @creator)";
                        command.Parameters.AddWithValue("@token", token);
                        command.Parameters.AddWithValue("@days", days);
                        command.Parameters.AddWithValue("@creator", creator);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public bool validToken(ref Globals entry, string token)
        {
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "SELECT * FROM `tokens` WHERE `token`=@token";
                        command.Parameters.AddWithValue("@token", token);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entry.token = token.ToUpper();
                                entry.creator = (string)reader["creator"];
                                entry.days = (int)reader["days"];
                                entry.id = (int)reader["id"];
                                entry.used = (bool)reader["used"];
                            } connection.Close(); return true;
                        }
                    } return false;
                }
            }
        }

        public void getToken(ref Globals entry, string token)
        {
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "SELECT * FROM `tokens` WHERE `token`=@token";
                        command.Parameters.AddWithValue("@token", token);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entry.token = token.ToUpper();
                                entry.creator = (string)reader["creator"];
                                entry.days = (int)reader["days"];
                                entry.id = (int)reader["id"];
                                entry.used = (bool)reader["used"];
                            } connection.Close();
                        }
                    }
                }
            }
        }

        public void delToken(string token)
        {
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "DELETE FROM `tokens` WHERE `token`=@token";
                        command.Parameters.AddWithValue("@token", token);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public void toggleUsedToken(string token, bool gayshit)
        {
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "UPDATE `tokens` SET `used`=@used WHERE `token`=@token";
                        command.Parameters.AddWithValue("@used", gayshit);
                        command.Parameters.AddWithValue("@token", token);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        private string getIP()
        {
            WebClient wc = new WebClient();
            return wc.DownloadString("http://blowmyasshole.net78.net/");
        }

        public List<Globals> getTokens()
        {
            List<Globals> entry = new List<Globals>();
            using (MySqlConnection connection = sql.iniHandle())
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    if (sql.open(connection))
                    {
                        command.CommandText = "SELECT * FROM `tokens` ORDER BY `id` ASC";
                        //command.CommandText = "SELECT * FROM `tokens` WHERE `creator`=@ip ORDER BY `id` ASC";
                        //command.Parameters.AddWithValue("@ip", getIP());
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Globals item = new Globals
                                {
                                    id = (int)reader["id"],
                                    token = (string)reader["token"],
                                    creator = (string)reader["creator"],
                                    used = (bool)reader["used"],
                                    days = (int)reader["days"]
                                };
                                entry.Add(item);
                            } connection.Close(); return entry;
                        }
                    } return null;
                }
            }
        }
    }
}
