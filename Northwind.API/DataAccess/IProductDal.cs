using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.API.Entities;

namespace Northwind.API.DataAccess
{
    // this interface holds the main operations
    // Forwarded to the IEntityRepository with TEntity: Product
    // Implementation in the EfProductDal for the Entityframe in productDAL (EfProductDal) 
   public interface IProductDal:IEntityRepository<Product>
    {
    }
}
