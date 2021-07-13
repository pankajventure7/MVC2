using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Repository;
using WebApplication9.Models;
using WebApplication9.Repository;

namespace WebApplication9.Controllers
{
    [Route("api/ValuesController")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private readonly IProduct _product;
        private readonly ProductVm product;
        private readonly Product product1;
        public ValuesController()
        {
            _product = new ProductConcrete();
            product = new ProductVm();
            product1 = new Product();
        }

        [HttpGet]
        [Route("GetDetails")]

        public List<Product> Details(int id)
        {
             var details = _product.GetProductByProductId(id);
              return details;
        }

        [HttpGet]
        [Route("GetAllDetails")]
        public List<Product> getAll()
        {
           
            var details = _product.GetProducts();
            return (List<Product>)details;

        }
        
        [HttpPost]
        [Route("Create")]
        public void add(List<ProductVm> product)
        {
            _product.InsertProduct(product);
        }

        [HttpPut]
        [Route("Update")]
        public void update(Product product)
        {
            _product.UpdateProduct(product);
        }
        [HttpGet]
        [Route("sum price")]
        public List<dynamic> getSum()
        {
         var sum  =  _product.getSum();
         return sum;
        }

        [HttpDelete]
        [Route("Delete")]
        public void getSum(int id)
        {
            _product.DeleteProduct(id);
            
        }



    }
}
