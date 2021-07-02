using Product.Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Product.Management.Data.SQLHelper
{
    public class ProductSql : DatabaseSetting
    {
        public ProductSql()
        {
        }
        public Tuple<IEnumerable<Products>, int> GetProducts(int catId,int subcatId,int start, int length, string orderBy, string search)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    string whereStr = "";
                    if (catId == 0 && subcatId==0)
                        whereStr = "";
                    else if(catId > 0 && subcatId == 0)
                        whereStr = "where CategoryId="+catId+ "";
                    else
                        whereStr = "where CategoryId=" + catId + " and SubcategoryId="+subcatId+"";

                    var sqlStr = "SELECT * FROM Products "+ whereStr + " order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    int _countData;
                    List<Products> products = new List<Products>();
                    if (!string.IsNullOrEmpty(search))
                    {
                        string temp = "";
                        if (whereStr.Length > 0)
                            temp = whereStr + " and";
                        else
                            temp = "where";
                        sqlStr = "SELECT * FROM Products "+temp+" Name like '%" + search + "%' order by " + orderBy + " OFFSET " + start + " ROWS FETCH NEXT " + length + " ROWS ONLY";
                    }
                    using (SqlCommand command = new SqlCommand("SELECT count(*) FROM Products "+ whereStr + "", con))
                    {
                        _countData = Convert.ToInt32(command.ExecuteScalar());
                    }

                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            products.Add(new Products()
                            {
                                Id = reader.GetInt32(0),
                                //CategoryId = reader.GetInt32(1),
                                //SubcategoryId = reader.GetInt32(2),
                                Name = reader.GetString(3),
                                Price = reader.GetSqlMoney(4),
                                KdvRate = reader.GetDecimal(5)
                            });
                        }
                    }
                    IEnumerable<Products> response = products;
                    return Tuple.Create(response, _countData);
                }
            }
            catch (System.Exception ex)
            {
                string exx = ex.Message;
                throw;
            }
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
        public IEnumerable<Subcategories> GetSelectListSubcategories(int catId)
        {
            try
            {
                var sqlStr = "SELECT * FROM Subcategories where CategoryId="+catId;
                List<Subcategories> _category = new List<Subcategories>();
                using (var con = OpenMSSQLConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _category.Add(new Subcategories()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(2)
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
        public bool ProductAdd(Products form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("Insert into Products Values(@CategoryId,@SubcategoryId,@Name,@Price,@KdvRate)", con);
                    command.Parameters.AddWithValue("@CategoryId", form.CategoryId);
                    command.Parameters.AddWithValue("@SubcategoryId", form.SubcategoryId);
                    command.Parameters.AddWithValue("@Name", form.Name);
                    command.Parameters.AddWithValue("@Price", form.Price);
                    command.Parameters.AddWithValue("@KdvRate", form.KdvRate);
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
        public bool ProductUpdate(Products form)
        {
            try
            {
                using (var con = OpenMSSQLConnection())
                {
                    SqlCommand command = new SqlCommand("UPDATE Products SET CategoryId=@CategoryId,SubcategoryId=@SubcategoryId ,Name=@Name,Price=@Price,KdvRate=@KdvRate Where Id=@Id", con);
                    command.Parameters.AddWithValue("@Id", form.Id);
                    command.Parameters.AddWithValue("@CategoryId", form.CategoryId);
                    command.Parameters.AddWithValue("@SubcategoryId", form.SubcategoryId);
                    command.Parameters.AddWithValue("@Name", form.Name);
                    command.Parameters.AddWithValue("@Price", form.Price);
                    command.Parameters.AddWithValue("@KdvRate", form.KdvRate);
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
        public Products ProductDetail(int id)
        {
            try
            {
                var sqlStr = "SELECT * FROM Products Where Id=" + id;
                Products _product = new Products();
                using (var con = OpenMSSQLConnection())
                {
                    using (SqlCommand command = new SqlCommand(sqlStr, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _product = new Products()
                            {
                                Id = reader.GetInt32(0),
                                CategoryId = reader.GetInt32(1),
                                SubcategoryId = reader.GetInt32(2),
                                Name = reader.GetString(3),
                                Price = reader.GetSqlMoney(4),
                                KdvRate = reader.GetDecimal(5)
                            };
                        }
                    }
                }
                return _product;
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
                throw;
            }
        }
    }
}
