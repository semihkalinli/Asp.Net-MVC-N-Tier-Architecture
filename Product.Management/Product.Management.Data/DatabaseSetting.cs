using System.Configuration;
using System.Data.SqlClient;

namespace Product.Management.Data
{
    public class DatabaseSetting
    {
        public SqlConnection OpenMSSQLConnection()
        {
            
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
        

    }
}
