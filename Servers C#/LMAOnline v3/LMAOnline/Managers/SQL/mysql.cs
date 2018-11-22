using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace LMAOnline.Managers
{
    internal class mysql
    {
        public string database, passwd, serverAddr, username;

        public mysql()
        {
            serverAddr = Globals.svrAddr;
            database = Globals.DBName;
            username = Globals.DBUser;
            passwd = Globals.DBPass;
        }

        public MySqlConnection iniHandle()
        {
            return new MySqlConnection(string.Format("Server={0};Database={1};Uid={2};Pwd={3};convert zero datetime=True;Allow Zero Datetime=True;", new object[] { this.serverAddr, this.database, this.username, this.passwd }));
        }

        public bool open(MySqlConnection con)
        {
            try { con.Open(); return true; }
            catch { return false; }
        }
    }
}
