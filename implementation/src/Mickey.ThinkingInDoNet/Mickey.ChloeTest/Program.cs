using Chloe;
using Chloe.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.ChloeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbContext context = new MySqlContext("");
            IQuery<User> q = context.Query<User>();
        }
    }
}
