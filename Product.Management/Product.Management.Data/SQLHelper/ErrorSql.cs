using Microsoft.AspNetCore.Http;
using Product.Management.Data.Models;
using System;
using System.Data.SqlClient;
using System.Web;
namespace Product.Management.Data.SQLHelper
{
    public class ErrorSql : DatabaseSetting
    {
        public ErrorSql()
        {
        }

        public bool LogInfoAdd(LogInfo form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("Insert into LogInfo Values(@Url,@Message,@CreatedDateTime,@IpAdress)", con);
                    command.Parameters.AddWithValue("@Url", form.Url);
                    command.Parameters.AddWithValue("@Message", form.Message);
                    command.Parameters.AddWithValue("@CreatedDateTime", form.CreatedDateTime);
                    command.Parameters.AddWithValue("@IpAdress", form.IpAdress);
                    var res = command.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
                return false;
                throw;
            }
        }
    }
}
