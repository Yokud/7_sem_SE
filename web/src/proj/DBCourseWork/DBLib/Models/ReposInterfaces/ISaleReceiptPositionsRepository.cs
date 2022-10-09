using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.DB;
using DBLib.SysEntities;

namespace DBLib.Models
{
    public interface ISaleReceiptPositionsRepository : IRepository<SaleReceiptPosition>
    {
        IEnumerable<Product> GetAllFromSaleReceipt(SaleReceipt saleReceipt);
    }
}
