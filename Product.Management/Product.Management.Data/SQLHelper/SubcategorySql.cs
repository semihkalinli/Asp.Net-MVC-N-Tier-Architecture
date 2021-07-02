using Product.Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Product.Management.Data.SQLHelper
{
    public class SubcategorySql : DatabaseSetting
    {
        public SubcategorySql()
        {
        }
        public IEnumerable<Categories> GetSelectListCategories()
        {
            try
            {
                var sqlStr = "SELECT * FROM Categories";
                List<Categories> _category = new List<Categories>();
                using (var con = OpenMSSQLConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _category.Add(new Categories()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
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
        public Tuple<IEnumerable<Subcategories>, int> GetSubcategories(int categoryID, int start, int length, string orderBy, string search)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    string whereStr = "";
                    if (categoryID == 0)
                        whereStr = "";
                    else
                        whereStr = "where CategoryId=" + categoryID + "";

                    var sqlStr = "SELECT * FROM Subcategories " + whereStr + " order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    int _countData;
                    List<Subcategories> _subcategories = new List<Subcategories>();
                    if (!string.IsNullOrEmpty(search))
                    {
                        string temp = "";
                        if (whereStr.Length > 0)
                            temp = whereStr + " and";
                        else
                            temp = "where";
                        sqlStr = "SELECT * FROM Subcategories " + temp + " Name like '%" + search + "%' order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    }
                    using (SqlCommand command = new SqlCommand("SELECT count(*) FROM Subcategories " + whereStr + "", con))
                    {
                        _countData = Convert.ToInt32(command.ExecuteScalar());
                    }

                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _subcategories.Add(new Subcategories()
                            {
                                Id = reader.GetInt32(0),
                                CategoryId= reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Description = reader.GetString(3)
                            });
                        }
                    }
                    IEnumerable<Subcategories> response = _subcategories;
                    return Tuple.Create(response, _countData);
                }
            }
            catch (System.Exception ex)
            {
                string exx = ex.Message;
                throw;
            }
        }
        public bool SubcategoryAdd(Subcategories form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("Insert into Subcategories Values(@CategoryId,@Name,@Description)", con);
                    command.Parameters.AddWithValue("@CategoryId", form.CategoryId);
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
        public bool SubcategoryUpdate(Subcategories form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("UPDATE Subcategories SET Name=@Name,CategoryId=@CategoryId ,Description=@Description Where Id=@Id", con);
                    command.Parameters.AddWithValue("@Id", form.Id);
                    command.Parameters.AddWithValue("@CategoryId", form.CategoryId);
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
        public Subcategories SubcategoryDetail(int id)
        {
            try
            {
                var sqlStr = "SELECT * FROM Subcategories Where Id=" + id;
                Subcategories _category = new Subcategories();
                using (var con = OpenMSSQLConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _category = new Subcategories()
                            {
                                Id = reader.GetInt32(0),
                                CategoryId = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Description = reader.GetString(3)
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
