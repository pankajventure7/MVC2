using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Repository;
using WebApplication9.Repository;

namespace WebApplication9.Controllers
{
    [Route("api/ValuesController")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private readonly IProduct _product;
        public ValuesController()
        {
            _product = new ProductConcrete();
        }

        [HttpGet]
        [Route("GetDetails")]

        public string Details(int id)
        {
            string JsonString = string.Empty;
            var details = _product.GetProductByProductId(id);

            JsonString = JsonConvert.SerializeObject(new 
            {

            });



            return JsonString;




        }
    }
}
