using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Northwind.API.Entities;
using Northwind.API.Model;

namespace Northwind.API.DataAccess
{
    public class EfProductDal: EfEntityRepositoryBase<Product,NorthwindContext>, IProductDal
    {
       // Not need to implement
       // Will be implemented by EfEntityRepository
        public List<ProductModel> GetProductsWithDetails()
        {
            using (var context = new NorthwindContext())
            {
                var result =from p in context.Products
                            join c in context.Categories
                            on p.CategoryId equals c.CategoryId
                        select new ProductModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryName = c.CategoryName,
                            UnitPrice = p.UnitPrice
                        };
                return result.ToList();
            }
        }
    }
}
