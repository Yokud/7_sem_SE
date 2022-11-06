using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.DB;
using DBLib.SysEntities;

namespace DBLib.Models
{
    public interface ISaleReceiptsRepository : IRepository<SaleReceipt>
    {
        IEnumerable<SaleReceipt> GetAllFromShop(Shop shop);
        IEnumerable<SaleReceipt> GetAllFromShop(int shopId);
    }
}
