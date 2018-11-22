using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace LMAOnline.Managers
{
    public class AnonSQL
    {
        private static mysql SQL = new mysql();

        public static bool getConsoleAnon(ref TmpEntry entry, bool getConsoleBySession = false) // arg = clientSession or CPUKey depending on the session bool.
        {
            using (MySqlConnection connection = SQL.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (SQL.open(connection)) {
                        command.CommandText = String.Format("SELECT * FROM `anon` WHERE `{0}`=@arg", getConsoleBySession ? "session" : "cpukey");
                        command.Parameters.AddWithValue("@arg", getConsoleBySession ? entry.ClientSession : entry.CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                entry.ClientName = (string)reader["name"];
                                entry.ClientEnabled = (bool)reader["enabled"];
                                entry.ClientBanned = (bool)reader["isbanned"];
                                entry.daysLeft = (int)reader["days"];
                                entry.ClientTime = Convert.ToDateTime(reader["time"]);
                                entry.CPUKey = getConsoleBySession ? (string)reader["cpukey"] : entry.CPUKey;
                                entry.ClientSession = !getConsoleBySession ? (!reader.IsDBNull(reader.GetOrdinal("session")) ? (string)reader["session"] : "") : entry.ClientSession;
                                connection.Close();
                                return true;
                            }
                        }
                    } return false;
                }
            }
        }

        public static bool addAnon(ref TmpEntry entry) // Inserting an anonymous user and giving them time.
        {
            try {
                using (MySqlConnection connection = SQL.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (SQL.open(connection)) {
                            command.CommandText = "INSERT INTO `anon` (`ip`, `cpukey`, `name`, `time`) VALUES (@ip, @cpukey, 'Anon', DATE_ADD(NOW(), INTERVAL 9999 DAY ))";
                            command.Parameters.AddWithValue("@ip", entry.ClientIP); 
                            command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                            command.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                    }
                } return false;
            } catch { Globals.write("Anon [{0}] addAnon Failed! CPUKey: {1}", entry.ClientIP, entry.CPUKey); return false; }
        }

        public static void saveConsoleAnon(ref TmpEntry entry) // Globally used save console information for clients.
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
    }
}
