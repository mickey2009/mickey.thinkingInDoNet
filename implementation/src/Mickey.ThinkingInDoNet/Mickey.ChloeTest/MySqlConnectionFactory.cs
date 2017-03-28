using Chloe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.ChloeTest
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;

        public MySqlConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            SqlConnection conn = new MySqlConnection(this._connString);
            return conn;
        }
    }
}
