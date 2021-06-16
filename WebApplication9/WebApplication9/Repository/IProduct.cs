using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication9.Models;

namespace WebApplication9.Repository
{
   public interface IProduct
    {
        void InsertProduct(ProductVm product); // C

        IEnumerable<Product> GetProducts(); // R

        List<Product> GetProductByProductId(int productId); // R

        void UpdateProduct(Product product); //U

        void DeleteProduct(int productId); //D
    }
}
