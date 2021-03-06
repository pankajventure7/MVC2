using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebApplication9;
using WebApplication9.Models;
using WebApplication9.Repository;

namespace WebApplication6.Repository
{
    public class ProductConcrete : IProduct
    {


        // Delete
        public void DeleteProduct(int expenseId)
        {
            using var con = new SqlConnection(SharedConnection.Value);
            var param = new DynamicParameters();
            param.Add("@ProductId", expenseId);
            var result = con.Execute("Usp_Delete_Product", param, null, 0, CommandType.StoredProcedure);
        }

        //  Get List of Products
        public IEnumerable<Product> GetProducts()
        {
            using var con = new SqlConnection(SharedConnection.Value);
            return con.Query<Product>("Usp_GetAll_Products", null, null, true, 0, CommandType.StoredProcedure).ToList();
        }

        //  Get Single Product
        public List<Product> GetProductByProductId(int expenseId)
        {



            List<Product> productList = new List<Product>();



            using (IDbConnection dbCoon = new SqlConnection(SharedConnection.Value))
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", expenseId);
                var item = dbCoon.Query<Product>("Usp_Get_Productby_ProductId", param, commandType: CommandType.StoredProcedure).ToList();
                if (item != null && item.Count > 0)
                {
                    productList.AddRange(item);
                }
                return productList;
            }
        }


        //  Insert
        public void InsertProduct(HashSet<ProductVm> product)
        {

            var result = 0;


                try
                {
                    using var con = new SqlConnection(SharedConnection.Value);
                    con.Open();
                    var transaction = con.BeginTransaction();
                    try
                    {
                        foreach (ProductVm product1 in product)
                        {
                            var param = new DynamicParameters();
                            param.Add("@Name", product1.Name);
                            param.Add("@type", product1.type);
                            param.Add("@Price", product1.Price);
                            param.Add("@PaidBy", product1.PaidBy);
                            param.Add("@Date", product1.date);
                            result = con.Execute("Usp_Insert_Product", param, transaction, 0, CommandType.StoredProcedure);
                          
                        }
                    if (result > 0)
                    {
                        transaction.Commit();
                    }

                }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            
        }

        // Update
        public void UpdateProduct(Product product)
        {
            using var con = new SqlConnection(SharedConnection.Value);
            con.Open();
            var transaction = con.BeginTransaction();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Name", product.Name);
                param.Add("@type", product.Type);
                param.Add("@Price", product.Price);
                param.Add("@PaidBy", product.PaidBy);
                param.Add("@Date", product.date);
                param.Add("@ProductId", product.ExpenseId);
                var result = con.Execute("Usp_Update_Product", param, transaction, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        // Check Product Exists
        public bool CheckProductExists(int productId)
        {
            using var con = new SqlConnection(SharedConnection.Value);
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            var result = con.Query<bool>("Usp_CheckProductExists", param, null, false, 0, CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }
        public List<dynamic> getSum()
         {
            List<dynamic> productList = new List<dynamic>();
            using (IDbConnection dbCoon = new SqlConnection(SharedConnection.Value))
            {
               
                List<dynamic> sum = dbCoon.Query("add_price", null, null, true, 0, CommandType.StoredProcedure).ToList();
                productList.AddRange(sum);
                return productList;
            }
        }
        public void BulkUpload()
        {

        }

        public void InsertProduct(List<ProductVm> product)
        {
            throw new NotImplementedException();
        }
    }
}