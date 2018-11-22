using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace GSN.Managers
{
    internal class MySQLHandle
    {
        private string SvrAddr, DBName, DBUser, DBPass;

        public MySQLHandle() // Initialize variables
        {
            SvrAddr = Globals.s_svrAddr;
            DBName = Globals.s_dbName;
            DBUser = Globals.s_dbUser;
            DBPass = Globals.s_dbPass;
        }

        public MySqlConnection iniHandle()
        {
            return new MySqlConnection(String.Format("Server={0};Database={1};Uid={2};Pwd={3};convert zero datetime=True;Allow Zero Datetime=True;", this.SvrAddr, this.DBName, this.DBUser, this.DBPass));
        }

        public bool Open(MySqlConnection con)
        {
            try { con.Open(); return true; }
            catch (MySqlException myEx) { GlobalFunc.Write("MySQL Exception: {0} || {1}", myEx.Source, myEx.Message); return false; }
            catch { return false; }
        }
    }
}
