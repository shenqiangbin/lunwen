using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Dapper;

namespace TimerService.SQL
{
    public class MySqlHelper
    {
        private static MySqlConnection _conn;

        public static MySqlConnection GetConn()
        {
            //不要每次都是新建一个，这样连接过多就会崩溃
            if (_conn == null)
            {
                string connStr = "server=192.168.103.90;database=thesismgmt;Uid=thesismgmt;Pwd=123456;";
                connStr = "server=127.0.0.1;database=thesisdb;Uid=root;Pwd=123456;";
                connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
                MySqlConnection conn = new MySqlConnection(connStr);
                _conn = conn;
            }

            if (_conn.State == System.Data.ConnectionState.Closed)
                _conn.Open();

            return _conn;
        }

    }
}
