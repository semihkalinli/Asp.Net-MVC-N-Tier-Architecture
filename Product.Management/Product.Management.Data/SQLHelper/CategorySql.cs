using Product.Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Product.Management.Data.SQLHelper
{
    public class CategorySql : DatabaseSetting
    {
        public CategorySql()
        {

        }
        public Tuple<IEnumerable<Categories>, int> GetCategories(int start, int length, string orderBy, string search)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    var sqlStr = "SELECT * FROM Categories order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    int _countData;
                    List<Categories> _categories = new List<Categories>();
                    if (!string.IsNullOrEmpty(search))
                    {
                        sqlStr = "SELECT * FROM Categories where Name like '%" + search + "%' order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    }
                    using (SqlCommand command = new SqlCommand("SELECT count(*) FROM Categories", con))
                    {
                        _countData = Convert.ToInt32(command.ExecuteScalar());
                    }

                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _categories.Add(new Categories()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            });
                        }
                    }
                    IEnumerable<Categories> response = _categories;
                    return Tuple.Create(response, _countData);
                }
            }
            catch (System.Exception ex)
            {
                string exx = ex.Message;
                throw;
            }
        }

        public bool CategoryAdd(Categories form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("Insert into Categories Values(@Name,@Description)", con);
                    command.Parameters.AddWithValue("@Name", form.Name);
                    command.Parameters.AddWithValue("@Description", form.Description);
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
        public bool CategoryUpdate(Categories form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("UPDATE Categories SET Name=@Name, Description=@Description Where Id=@Id", con);
                    command.Parameters.AddWithValue("@Id", form.Id);
                    command.Parameters.AddWithValue("@Name", form.Name);
                    command.Parameters.AddWithValue("@Description", form.Description);
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
        public Categories CategoryDetail(int id)
        {
            try
            {
                var sqlStr = "SELECT * FROM Categories Where Id="+id;
                Categories _category = new Categories();
                using (var con = OpenMSSQLConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _category= new Categories()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                        }
                    }
                }
                return _category;
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
                throw;
            }
        }
       
    }
}
