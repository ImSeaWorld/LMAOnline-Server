using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

using GSN.Server;
using GSN.Globals;

namespace GSN.SQL
{
    public class ClientSQL
    {
        private static Handle SQL = new Handle();

        public static bool Get(ref Tmp.Entry Entry, bool GetBySession = false)
        {
            using (MySqlConnection Connection = SQL.iniHandle()) {
                using (MySqlCommand Command = Connection.CreateCommand()) {
                    if (SQL.Open(Connection)) {
                        Command.CommandText = String.Format("SELECT * FROM `consoles` WHERE `{0}`=@arg;", GetBySession ? "session" : "cpukey");
                        Command.Parameters.AddWithValue("@arg", GetBySession ? Entry.Session : Entry.CPUKey);
                        using (MySqlDataReader Reader = Command.ExecuteReader()) {
                            if (Reader.Read()) {
                                Entry.Session       = GetBySession ? Entry.Session : (string)Reader["session"];
                                Entry.CPUKey        = (string)Reader["cpukey"];
                                Entry.Name          = (string)Reader["name"];
                                Entry.Suspended     = (bool)Reader["suspended"];
                                Entry.Lifetime      = (bool)Reader["lifetime"];
                                Entry.Enabled       = (bool)Reader["enabled"];
                                Entry.Banned        = (bool)Reader["banned"];
                                Entry.Days          = (int)Reader["daysLeft"];
                                Entry.Used          = (int)Reader["daysUsed"];
                                Entry.ID            = (int)Reader["id"];
                                Entry.Expire        = Convert.ToDateTime(Reader["expire"]);
                                return true;
                            }
                        }
                    } return false;
                }
            }
        }

        public static bool Set(ref Tmp.Entry Entry)
        {
            try {
                using (MySqlConnection Connection = SQL.iniHandle()) {
                    using (MySqlCommand Command = Connection.CreateCommand()) {
                        if (SQL.Open(Connection)) {
                            Command.CommandText = "UPDATE `consoles` SET `ip`=@ip, `gamertag`=@gt, `titleID`=@title, `session`=@session, `lastSeen`=NOW() WHERE `cpukey`=@cpukey;";
                            Command.Parameters.AddWithValue("@ip", Entry.IP);
                            Command.Parameters.AddWithValue("@gt", Entry.GamerTag);
                            Command.Parameters.AddWithValue("@title", Entry.Title);
                            Command.Parameters.AddWithValue("@session", Entry.Session);
                            Command.Parameters.AddWithValue("@cpukey", Entry.CPUKey);
                            Command.ExecuteNonQuery();
                            return true;
                        } return false;
                    }
                }
            } catch { return false; }
        }

        public static bool Increment(ref Tmp.Entry Entry, bool Check = false)
        {
            if (Get(ref Entry)) {
                if (Entry.Days >= 1 && !Entry.Enabled) {
                    if (Check) return true;
                    using (MySqlConnection Connection = SQL.iniHandle()) {
                        using (MySqlCommand Command = Connection.CreateCommand()) {
                            if (SQL.Open(Connection)) {
                                Command.CommandText = "UPDATE `consoles` SET `expire`=DATE_ADD(NOW(), INTERVAL 1 DAY), `daysLeft`=`daysLeft`-1, `enabled`=1, `daysUsed`=`daysUsed`+1 WHERE `id`=@id;";
                                Command.Parameters.AddWithValue("@id", Entry.ID);
                                Command.ExecuteNonQuery();
                                return true;
                            }
                        }
                    }
                }
            } return false;
        }

        public static void CronCheck()
        {
            using (MySqlConnection Connection = SQL.iniHandle()) {
                using (MySqlCommand Command = Connection.CreateCommand()) {
                    if (SQL.Open(Connection)) {
                        Command.CommandText = "UPDATE `consoles` SET `enabled`=0 WHERE NOW() >= `expire`;UPDATE `consoles` SET `enabled`=1 WHERE NOW() <= `expire`;";
                        Command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
