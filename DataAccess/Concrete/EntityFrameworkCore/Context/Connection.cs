using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class Connection
    {
        public static string connectionString { get; set; }

        public static IDbConnection _dbConnection
        {
            get
            {
                connectionString = "Data Source=.;Initial Catalog=ArticleManagementDB;Integrated Security=True;";
                return new SqlConnection(connectionString);
            }
        }
    }
}
