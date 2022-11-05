using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.DB;
using DBLib.SysEntities;

namespace DBLib.Models
{
    public interface ICostStoryRepository : IRepository<CostStory>
    {
        IEnumerable<CostStory> GetFullCostStory(Shop shop, Product product);
        IEnumerable<CostStory> GetFullCostStory(int shopId, int productId);
    }
}
