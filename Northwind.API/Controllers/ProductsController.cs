using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.API.DataAccess;
using Northwind.API.Entities;

namespace Northwind.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private IProductDal _productDal;

        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        //because the method name is GET:
        //It will work with api/products
        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }

        //Product id in the routing should be same with parameter productID
        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There is no product with Id= {productId}");
                }
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }
        
        // If you want to read the data from JSON/Text you need to add [from body]
        // without it will not read the product

        [HttpPost("")]
        public IActionResult Post([FromBody]Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return BadRequest();
        }
        [HttpPut("")]
        public IActionResult Put([FromBody]Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return BadRequest();
        }
    }
}