using System;

using MySql.Data.MySqlClient;

namespace LMAOnline.Managers
{
    public class _24HourTimer
    {
        private static mysql sql = new mysql();

        // Checking if they have time left, if so, we don't add more time.
        public static bool timeLeft(ref TmpEntry entry)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT (CASE WHEN NOW() <= `time` THEN 1 ELSE 0 END) FROM `consoles` WHERE `cpukey`=@cpukey";
                        command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader())
                            if (reader.Read())
                                return Convert.ToBoolean(reader[0]);
                    } return true;
                }
            }
        }

        // Adding the time, then removing the day we added from `days`
        public static void AddTime(ref TmpEntry entry)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "UPDATE `consoles` SET `time`=DATE_ADD(NOW(), INTERVAL 1 DAY ), `days`=days-1, `enabled`=1 WHERE `cpukey`=@cpukey";
                        command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        public static bool hasDays(ref TmpEntry entry)
        {
            using (MySqlConnection connection = sql.iniHandle()) {
                using (MySqlCommand command = connection.CreateCommand()) {
                    if (sql.open(connection)) {
                        command.CommandText = "SELECT `days` FROM `consoles` WHERE `cpukey`=@cpukey";
                        command.Parameters.AddWithValue("@cpukey", entry.CPUKey);
                        using (MySqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read())
                                if (Convert.ToInt32(reader[0]) > 0)
                                    return true;
                                else return false;
                        }
                    } return false;
                }
            }
        }

        // If true, we can notify the user.
        public static bool canIncrimentTime(ref TmpEntry entry)
        {
            try {
                if (!timeLeft(ref entry)) {
                    if (hasDays(ref entry)) {
                        AddTime(ref entry);
                        return true;
                    }
                } return false;
            }
            catch (Exception ex) { /*Globals.write("Exception canIncrimentTime: {0}", ex.Message);*/ return false; }
        }
    }
}
