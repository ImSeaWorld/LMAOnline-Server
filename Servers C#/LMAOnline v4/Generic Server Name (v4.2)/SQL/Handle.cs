using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GSN.Globals;
using MySql.Data.MySqlClient;

namespace GSN.SQL
{
    internal class Handle
    {
        private string SvrAddr, DBName, DBUser, DBPass;

        public Handle()
        {
            SvrAddr = GlobalVar.s_svrAddr;
            DBName = GlobalVar.s_dbName;
            DBUser = GlobalVar.s_dbUser;
            DBPass = GlobalVar.s_dbPass;
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
