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
        public void DeleteProduct(int productId)
        {
            using var con = new SqlConnection(SharedConnection.Value);
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            var result = con.Execute("Usp_Delete_Product", param, null, 0, CommandType.StoredProcedure);
        }

        //  Get List of Products
        public IEnumerable<Product> GetProducts()
        {
            using var con = new SqlConnection(SharedConnection.Value);
            return con.Query<Product>("Usp_GetAll_Products", null, null, true, 0, CommandType.StoredProcedure).ToList();
        }

        //  Get Single Product
        public List<Product> GetProductByProductId(int productId)
        {



            List<Product> productList = new List<Product>(); 

            using (IDbConnection dbCoon = new SqlConnection(SharedConnection.Value))
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", productId);

                var item = dbCoon.Query<Product>("Usp_Get_Productby_ProductId", param, null, true, 0, commandType: CommandType.StoredProcedure);
                productList.Add((Product)item);

                return productList; //(Product)dbCoon.Query<Product>("Usp_Get_Productby_ProductId", param, null, true, 0,commandType:CommandType.StoredProcedure);
            }

        }


        //  Insert
        public void InsertProduct(ProductVm product)
        {
            try
            {
                using var con = new SqlConnection(SharedConnection.Value);
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Name", product.Name);
                    param.Add("@Quantity", product.Quantity);
                    param.Add("@Color", product.Color);
                    param.Add("@Price", product.Price);
                    param.Add("@ProductCode", product.ProductCode);
                    var result = con.Execute("Usp_Insert_Product", param, transaction, 0, CommandType.StoredProcedure);
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
            catch (Exception)
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
                param.Add("@Quantity", product.Quantity);
                param.Add("@Color", product.Color);
                param.Add("@Price", product.Price);
                param.Add("@ProductCode", product.ProductCode);
                param.Add("@ProductId", product.ProductId);
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
    }
}