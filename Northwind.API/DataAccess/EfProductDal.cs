using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Northwind.API.Entities;

namespace Northwind.API.DataAccess
{
    public class EfProductDal: EfEntityRepositoryBase<Product,NorthwindContext>, IProductDal
    {
       // Not need to implement
       // Will be implemented by EfEntityRepository
    }
}
