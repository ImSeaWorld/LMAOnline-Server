using System;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace LMAOnline.Classes
{
    public class ConsoleManager
    {
        ConsoleEntry ce = new ConsoleEntry();
        mysql sql = new mysql();
        public string nigerian_kike;

        public ConsoleManager() { }

        public void AddConsole(string name, string email, string cpukey, int expireTime)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "INSERT INTO consoles (name, email, cpukey, enabled) VALUES (@Name, @Email, @CpuKey, 1)";
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        command.ExecuteNonQuery();
                    } connection.Close(); AddTime(cpukey, expireTime);
                }
            }
        }

        public void newAddConsole(string name, string email, string cpukey, int expireTime, bool console_type, bool bypass_bo2, bool bypass_ghost, bool bypass_aw, bool xosc = true, bool online = true)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "INSERT INTO consoles (name, email, cpukey, enabled, platinum, online, bo2, ghost, aw, consoletype) VALUES (@Name, @Email, @CpuKey, 1, @Platinum, @Online, @BO2, @GHOST, @AW, @ConsoleType)";
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        command.Parameters.AddWithValue("@Platinum", xosc);
                        command.Parameters.AddWithValue("@Online", online);
                        command.Parameters.AddWithValue("@BO2", bypass_bo2);
                        command.Parameters.AddWithValue("@GHOST", bypass_ghost);
                        command.Parameters.AddWithValue("@AW", bypass_aw);
                        command.Parameters.AddWithValue("@ConsoleType", console_type);
                        command.ExecuteNonQuery();
                    } connection.Close(); AddTime(cpukey, expireTime);
                }
            }
        }

        public void AddBannedConsole(string name, string cpukey, string reason)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "INSERT INTO banned (name, cpukey, reason, ip) VALUES (@Name, @CpuKey, @Reason, @IPAddr)";
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        command.Parameters.AddWithValue("@Reason", reason);
                        command.Parameters.AddWithValue("@IPAddr", ce.ip);
                        command.ExecuteNonQuery();
                    } connection.Close();
                }
            }
        }

        public void update_Console(string name, string email, string cpukey, int expireTime)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "UPDATE `consoles` SET `name`=@Name, `email`=@Email, `time`=DATE_ADD(NOW(), INTERVAL @Days DAY ), `enabled`=1 WHERE `cpukey`=@Cpukey";
                        command.Parameters.AddWithValue("@Cpukey", cpukey);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Days", expireTime);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public void AddTime(string cpukey, int days)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "UPDATE `consoles` SET `time`=DATE_ADD(NOW(), INTERVAL @Days DAY ), `enabled`=1 WHERE `cpukey`=@Cpukey";
                        command.Parameters.AddWithValue("@Days", days);
                        command.Parameters.AddWithValue("@Cpukey", cpukey);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public bool CheckIfExists(string CPUKey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT COUNT(*) FROM consoles WHERE cpukey=@Cpukey";
                        command.Parameters.AddWithValue("@Cpukey", CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (Convert.ToInt32(reader[0]) > 0) { connection.Close(); return true; }
                                else { connection.Close(); return false; }
                            }
                        }
                    } return false;
                }
            }
        }

        public bool GetfailedConsole(ref ConsoleEntry entry, string cpukey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT COUNT(*) FROM `failed` WHERE `cpukey`=@CpuKey";
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (Convert.ToInt32(reader[0]) > 0) { connection.Close(); return true; }
                                else { connection.Close(); return false; }
                            }
                        }
                    } return false;
                }
            }
        }

        public void GetBannedConsole(ref ConsoleEntry entry, string cpukey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT * FROM `failed` WHERE `cpukey`=@CpuKey";
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                entry.cpukey = cpukey.ToUpper();
                                entry.name = !reader.IsDBNull(reader.GetOrdinal("name")) ? (string)reader["name"] : "*No Name Given*";
                                entry.sip = !reader.IsDBNull(reader.GetOrdinal("ip")) ? reader["ip"].ToString() : "0.0.0.0";
                                entry.kvdata = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? (byte[])reader["kvdata"] : null;
                                entry.haskv = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? true : false;
                                entry.reason = !reader.IsDBNull(reader.GetOrdinal("reason")) ? (string)reader["reason"] : "*No Reason Given*";
                            } connection.Close();
                        }
                    }
                }
            }
        }

        public List<ConsoleEntry> GetBannedConsoles()
        {
            List<ConsoleEntry> list = new List<ConsoleEntry>();
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT * FROM banned";
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                ConsoleEntry item = new ConsoleEntry { cpukey = (string)reader["cpukey"] };
                                item.name = !reader.IsDBNull(reader.GetOrdinal("name")) ? (string)reader["name"] : null;
                                item.sip = !reader.IsDBNull(reader.GetOrdinal("ip")) ? (string)reader["ip"] : "0.0.0.0";
                                item.kvdata = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? (byte[])reader["kvdata"] : null;
                                item.reason = !reader.IsDBNull(reader.GetOrdinal("reason")) ? (string)reader["reason"] : null;
                                item.haskv = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? true : false;
                                list.Add(item);
                                connection.Close();
                                return list;
                            }
                        }
                    } return null;
                }
            }
        }

        public List<ConsoleEntry> GetfailedConsoles()
        {
            List<ConsoleEntry> list = new List<ConsoleEntry>();
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    while (sql.open(connection)) {
                        command.CommandText = "SELECT * FROM `failed`";
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                ConsoleEntry item = new ConsoleEntry {
                                    cpukey = (string)reader["cpukey"],
                                    sip = !reader.IsDBNull(reader.GetOrdinal("ip")) ? reader["ip"].ToString() : "0.0.0.0",
                                    kvdata = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? (byte[])reader["kvdata"] : null
                                }; list.Add(item);
                            } connection.Close(); return list;
                        }
                    } return null;
                }
            }
        }

        public bool GetConsole(ref ConsoleEntry entry, string cpukey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT * FROM consoles WHERE cpukey=@CpuKey";
                        command.Parameters.AddWithValue("@CpuKey", cpukey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                entry.name = (string)reader["name"];
                                entry.email = (string)reader["email"];
                                entry.cpukey = (string)reader["cpukey"];
                                entry.enabled = (bool)reader["enabled"];
                                entry.salt = !reader.IsDBNull(reader.GetOrdinal("salt")) ? (string)reader["salt"] : null;
                                entry.sip = !reader.IsDBNull(reader.GetOrdinal("ip")) ? (string)reader["ip"] : "0.0.0.0";
                                entry.titleid = !reader.IsDBNull(reader.GetOrdinal("titleid")) ? (string)reader["titleid"] : null;
                                entry.kvdata = !reader.IsDBNull(reader.GetOrdinal("kvdata")) ? (byte[])reader["kvdata"] : null;
                            } connection.Close(); return true;
                        }
                    } return false;
                }
            }
        }

        public List<ConsoleEntry> GetConsoles()
        {
            try
            {
                List<ConsoleEntry> list = new List<ConsoleEntry>();
                using (MySqlConnection connection = sql.iniHandle()) {
                    using (MySqlCommand command = connection.CreateCommand()) {
                        if (sql.open(connection)) {
                            command.CommandText = "SELECT * FROM consoles";
                            using (MySqlDataReader reader = command.ExecuteReader()) {
                                while (reader.Read()) {
                                    ConsoleEntry item = new ConsoleEntry {
                                        name = (string)reader["name"],
                                        email = (string)reader["email"],
                                        cpukey = (string)reader["cpukey"],
                                        enabled = (bool)reader["enabled"]
                                    };
                                    item.gamertag = !reader.IsDBNull(reader.GetOrdinal("gamertag")) ? (string)reader["gamertag"] : "*Not Signed In*";
                                    item.salt = !reader.IsDBNull(reader.GetOrdinal("salt")) ? (string)reader["salt"] : null;
                                    item.sip = !reader.IsDBNull(reader.GetOrdinal("ip")) ? (string)reader["ip"] : "0.0.0.0";
                                    item.titleid = !reader.IsDBNull(reader.GetOrdinal("titleid")) ? (string)reader["titleid"] : "*No Title*";
                                    returnTimeCalc(item.cpukey);
                                    int days = Convert.ToInt32(CalcTime());
                                    if (Convert.ToInt32(CalcTime()) == 0) {
                                        string[] feg = CalcRemainingTime().Split(':');
                                        item.remainingTime = String.Format("Hour(s): {0} | Minute(s): {1}", feg[0], feg[1]).Replace('-', ' ');
                                    } else { item.expireTime = days; }
                                    item.last_seen = !reader.IsDBNull(reader.GetOrdinal("lastseen")) ? (string)reader["lastseen"] : "*Hasn't Been Seen*";
                                    list.Add(item);
                                } connection.Close(); return list;
                            }
                        } return null;
                    }
                }
            } catch { return null; }
        }

        public void GlobalRemoveCPU(string table, string cpukey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = string.Format("DELETE FROM `{0}` WHERE cpukey=@CPUKey", table);
                        command.Parameters.AddWithValue("@CPUKey", cpukey);
                        command.ExecuteNonQuery();
                    } connection.Close();
                }
            }
        }

        #region Time Function
        public void returnTimeCalc(string CPUKey)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT time FROM consoles WHERE cpukey=@CpuKey";
                        command.Parameters.AddWithValue("@CpuKey", CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                nigerian_kike = reader["time"].ToString();
                            } connection.Close(); CalcTime(); CalcRemainingTime();
                        }
                    }
                }
            }
        }

        public string CalcTime() // Returns Time In Days
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT DATEDIFF(@time, NOW())";
                        command.Parameters.AddWithValue("@time", DateTime.Parse(nigerian_kike));
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) { return reader[0].ToString(); } connection.Close(); 
                        }
                    } return null;
                }
            }
        }

        public string CalcRemainingTime() // Returns Time In Hours & Min
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT TIMEDIFF(@time, NOW())";
                        command.Parameters.AddWithValue("@time", DateTime.Parse(nigerian_kike));
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) { return reader[0].ToString(); } connection.Close(); 
                        }
                    } return null;
                }
            }
        } 
        #endregion
    }
}