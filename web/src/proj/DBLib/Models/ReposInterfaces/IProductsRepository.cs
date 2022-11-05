using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.DB;
using DBLib.SysEntities;

namespace DBLib.Models
{
    public interface IProductsRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllFromShop(Shop shop);
        IEnumerable<Product> GetAllFromShop(int shopId);
    }
}
