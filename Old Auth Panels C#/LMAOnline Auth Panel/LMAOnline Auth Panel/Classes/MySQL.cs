using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace LMAOnline.Classes
{
    internal class mysql
    {
        public string database;
        public string passwd;
        public string serverAddr;
        public string username;

        public mysql()
        {
            serverAddr = "167.114.100.102";
            database = "seaworld_xblrogers";
            username = "seaworld_main";
            passwd = "ueJ5{g9D~O?r";
        }

        public MySqlConnection iniHandle()
        {
            return new MySqlConnection(string.Format("Server={0};Database={1};Uid={2};Pwd={3};convert zero datetime=True;Allow Zero Datetime=True;", new object[] { this.serverAddr, this.database, this.username, this.passwd }));
        }

        public bool open(MySqlConnection con)
        {
            try
            {
                con.Open();
                return true;
            }
            catch { return false; }
        }
    }
}
