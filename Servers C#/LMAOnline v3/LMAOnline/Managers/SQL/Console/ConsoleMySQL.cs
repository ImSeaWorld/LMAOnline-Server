using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace LMAOnline.Managers
{
    public class ConsoleMySQL
    {
        private static mysql SQL = new mysql();

        public static bool getConsole(ref TmpEntry entry, bool getConsoleBySession = false) // arg = clientSalt or CPUKey depending on the session bool.
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.open(connection)) {
                        command.CommandText = String.Format("SELECT * FROM `consoles` WHERE `{0}`=@arg", getConsoleBySession ? "session" : "cpukey");
                        command.Parameters.AddWithValue("@arg", getConsoleBySession ? entry.ClientSession : entry.CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                entry.ClientName = (string)reader["name"];
                                entry.ClientEnabled = (bool)reader["enabled"];
                                entry.ClientBanned = (bool)reader["isbanned"];
                                entry.ClientXeXChecks = (bool)reader["xexchecks"];
                                entry.daysLeft = (int)reader["days"];
                                entry.CPUKey = getConsoleBySession ? (string)reader["cpukey"] : entry.CPUKey;
                                entry.ClientSession = !getConsoleBySession ? (!reader.IsDBNull(reader.GetOrdinal("session")) ? (string)reader["session"] : "") : entry.ClientSession;
                                entry.KVDat = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? (byte[])reader["kvdata"] : null;
                                connection.Close();
                                return true;
                            }
                        }
                    } return false;
                }
            }
        }

        public static bool getToken(ref TmpEntry entry)
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.open(connection)) {
                        command.CommandText = "SELECT * FROM `tokens` WHERE `token`=@token";
                        command.Parameters.AddWithValue("@token", entry.inToken);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                entry.outToken = (string)reader["token"];
                                entry.TokenUsed = (bool)reader["used"];
                                entry.TokenDays = (int)reader["days"];
                                entry.TokenID = (int)reader["tid"];
                                connection.Close();
                                return true;
                            }
                        }
                    } return false;
                }
            }
        }

        public static void saveConsole(ref TmpEntry entry) // Globally used save console information for clients.
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.open(connection)) {
                        command.CommandText = "UPDATE `consoles` SET `salt`=@salt, `session`=@session, `enabled`=@enabled, `ip`=@ipaddr, `titleid`=@titleid, `gamertag`=@gt, `lastseen`=NOW() WHERE `cpukey`=@cpukey";
                        command.Parameters.AddWithValue("@salt", entry.ClientSalt);
                        command.Parameters.AddWithValue("@session", entry.ClientSession);
                        command.Parameters.AddWithValue("@enabled", entry.ClientEnabled);
                        command.Parameters.AddWithValue("@ipaddr", entry.ClientIP);
                        command.Parameters.AddWithValue("@titleid", entry.ClientTitle);
                        command.Parameters.AddWithValue("@gt", entry.ClientGT);
                        command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public static void saveToken(ref TmpEntry entry)
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.open(connection)) {
                        command.CommandText = "UPDATE `tokens` SET `used`=1, `timeused`=NOW(), `cpukey`=@cpukey WHERE `tid`=@tid";
                        command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                        command.Parameters.AddWithValue("@tid", entry.TokenID);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public static void updateOtherConsoles(ref TmpEntry entry, bool banned = false) // For BANNED and FAILED connections
        {
            try {
                using (MySqlConnection connection = SQL.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (SQL.open(connection)) {
                            command.CommandText = String.Format("INSERT INTO `{0}` (`ip`, `kvdata`, `cpukey`) VALUES (@ipaddr, @kvdata, @cpukey) ON DUPLICATE KEY UPDATE `ip`=@ipaddr, `kvdata`=@kvdata", banned ? "banned" : "failed");
                            command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                            command.Parameters.AddWithValue("@ipaddr", entry.ClientIP);
                            command.Parameters.AddWithValue("@kvdata", entry.KVDat);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            } catch (MySqlException ex) { Globals.write("MySQL Exception: {0} || {1}", ex.Message, ex.InnerException); }
        }

        public static bool autoUpdateTime(ref TmpEntry entry) // Update to the original 24 hour timer.
        {
            if (getConsole(ref entry)) {
                Globals.write("FUCK {0} Days Left | {1}", entry.daysLeft, (bool)(DateTime.Now <= entry.ClientTime));
                if (entry.daysLeft >= 1 && !(entry.ClientTime >= DateTime.Now)) { // See if they have time left. No need to open a connection where none is needed.
                    using (MySqlConnection connection = SQL.iniHandle()) {
                        using (MySqlCommand command = connection.CreateCommand()) {
                            if (SQL.open(connection)) {
                                command.CommandText = "UPDATE `consoles` SET `time`=DATE_ADD(NOW(), INTERVAL 1 DAY ), `days`=days-1, `enabled`=1 WHERE `cpukey`=@cpukey";
                                command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                                command.ExecuteNonQuery();
                                connection.Close();
                                return true;
                            }
                        }
                    }
                }
            } return false;
        }

        public static void updateEnabledConsoles() // Update thread for people which used their time.
        {
            using (MySqlConnection connection = SQL.iniHandle())
                using (MySqlCommand command = connection.CreateCommand())
                    if (SQL.open(connection))
                    { command.CommandText = "UPDATE `consoles` SET `enabled`=0 WHERE NOW() >= `time`;UPDATE `consoles` SET `enabled`=1 WHERE NOW() <= `time`"; command.ExecuteNonQuery(); connection.Close(); }
        }
    }
}