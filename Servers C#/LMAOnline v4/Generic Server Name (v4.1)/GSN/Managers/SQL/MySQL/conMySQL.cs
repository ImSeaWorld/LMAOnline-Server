using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace GSN.Managers
{
    public class conMySQL
    {
        private static MySQLHandle SQL = new MySQLHandle();
        
        public static bool getConsole(ref TmpEntry entry, bool getConBySesh = false) // 2 functions in one ;)
        {
            try {
                using (MySqlConnection connection = SQL.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (SQL.Open(connection)) {
                            command.CommandText = String.Format("SELECT * FROM `consoles` WHERE `{0}`=@arg", getConBySesh ? "session" : "cpukey");
                            command.Parameters.AddWithValue("@arg", getConBySesh ? entry.client_session : entry.client_cpukey);
                            using (MySqlDataReader reader = command.ExecuteReader()) {
                                if (reader.Read()) {
                                    entry.client_cpukey = (string)reader["cpukey"];
                                    entry.client_consoleID = (int)reader["id"];
                                    entry.client_name = (string)reader["name"];
                                    entry.client_db_curIP = (string)reader["ip"];
                                    //entry.client_db_lastIP = (string)reader["lastIP"];
                                    entry.client_session = (string)reader["session"];
                                    //entry.client_xexVersion = (int)reader["version"];
                                    //entry.client_gracePeriod_days = (int)reader["gracePeriod"];
                                    entry.client_days = (int)reader["daysLeft"];
                                    entry.client_daysUsed = (int)reader["daysUsed"];
                                    entry.client_dateExpire = Convert.ToDateTime(reader["expire"]);
                                    entry.client_enabled = (bool)reader["enabled"];
                                    entry.client_suspended = (bool)reader["suspended"];
                                    entry.client_banned = (bool)reader["banned"];
                                    //entry.client_xexChecks = (bool)reader["xexChecks"];
                                    connection.Close();
                                    return true;
                                }
                            }
                        } return false;
                    }
                }
            } catch (MySqlException mysqlEx) { GlobalFunc.Write("MySQL Exception! Error Details: {0} || {1}", mysqlEx.Message, mysqlEx.StackTrace); return false; }
        }

        public static void setConsole(ref TmpEntry entry)
        {
            try {
                using (MySqlConnection connection = SQL.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (SQL.Open(connection)) {
                            command.CommandText = "UPDATE `consoles` SET `ip`=@curIP, `gamertag`=@gamertag, `titleID`=@titleID, `session`=@session, `lastSeen`=NOW() WHERE `cpukey`=@cpukey";
                            command.Parameters.AddWithValue("@curIP", entry.client_ip);
                            command.Parameters.AddWithValue("@gamertag", entry.client_gt);
                            command.Parameters.AddWithValue("@titleID", entry.client_title);
                            command.Parameters.AddWithValue("@session", entry.client_session);
                            command.Parameters.AddWithValue("@cpukey", entry.client_cpukey);
                            command.ExecuteNonQuery(); connection.Close(); 
                        }
                    }
                }
            } catch (MySqlException mysqlEx) { GlobalFunc.Write("MySQL Exception! Error Details: {0} || {1}", mysqlEx.Message, mysqlEx.StackTrace); }
        }

        public static void updateOtherConsoles(ref TmpEntry entry, bool banned = false) // For BANNED and FAILED connections
        {
            try {
                using (MySqlConnection connection = SQL.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (SQL.Open(connection)) {
                            command.CommandText = String.Format("INSERT INTO `{0}` (`ip`, `kv`, `cpukey`) VALUES (@ipaddr, @kv, @cpukey) ON DUPLICATE KEY UPDATE `ip`=@ipaddr, `kv`=@kv", banned ? "banned" : "failed");
                            command.Parameters.AddWithValue("@cpukey", entry.client_cpukey);
                            command.Parameters.AddWithValue("@ipaddr", entry.client_ip);
                            command.Parameters.AddWithValue("@kv", entry.client_kv);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            } catch (MySqlException mysqlEx) { GlobalFunc.Write("MySQL Exception! Error Details: {0} || {1}", mysqlEx.Message, mysqlEx.StackTrace); }
        }

        public static bool autoUpdateTime(ref TmpEntry entry) // Update to the original 24 hour timer.
        {
            if (getConsole(ref entry)) {
                if (entry.client_days >= 1 && !entry.client_enabled) {
                    using (MySqlConnection connection = SQL.iniHandle()) {
                        using (MySqlCommand command = connection.CreateCommand()) {
                            if (SQL.Open(connection)) {
                                command.CommandText = "UPDATE `consoles` SET `expire`=DATE_ADD(NOW(), INTERVAL 1 DAY ), `daysLeft`=`daysLeft`-1, `enabled`=1, `daysUsed`=`daysUsed`+1 WHERE `id`=@id";
                                command.Parameters.AddWithValue("@id", entry.client_consoleID);
                                command.ExecuteNonQuery();
                                connection.Close();
                                return true;
                            }
                        }
                    }
                }
            } return false;
        }

        public static void addNewIP(ref TmpEntry entry)
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.Open(connection)) {
                        command.CommandText = "INSERT INTO `console_ip_history` (`cid`, `ip`) VALUES (@cid, @ip);";
                        command.Parameters.AddWithValue("@cid", entry.client_consoleID);
                        command.Parameters.AddWithValue("@ip", entry.client_ip);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public static void addSpoofyFag(ref TmpEntry entry)
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.Open(connection)) {
                        command.CommandText = "INSERT INTO `spoofy` (`cpukey`, `ip`, `attempts`, `time`, `kv`) VALUES (@cpukey, @ip, 1, NOW(), @kv) ON DUPLICATE KEY UPDATE `spoofy` SET `ip`=@ip, `attempts`=attempts+1, `kv`=@kv";
                        command.Parameters.AddWithValue("@cpukey", entry.client_cpukey);
                        command.Parameters.AddWithValue("@ip", entry.client_ip);
                        command.Parameters.AddWithValue("@kv", entry.client_kv);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public static void updateEnabledConsoles() // Update thread for people which used their time.
        {
            using (MySqlConnection connection = SQL.iniHandle())
                using (MySqlCommand command = connection.CreateCommand())
                    if (SQL.Open(connection))
                    { command.CommandText = "UPDATE `consoles` SET `enabled`=0 WHERE NOW() >= `expire`;UPDATE `consoles` SET `enabled`=1 WHERE NOW() <= `expire`"; command.ExecuteNonQuery(); connection.Close(); }
        }
    }
}
