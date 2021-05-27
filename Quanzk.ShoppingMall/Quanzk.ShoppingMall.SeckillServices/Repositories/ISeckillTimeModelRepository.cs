using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀时间仓储接口
    /// </summary>
    public interface ISeckillTimeModelRepository : IDependency
    {
        IEnumerable<SeckillTimeModel> GetSeckillTimeModels();
        SeckillTimeModel GetSeckillTimeModelById(int id);
        bool Create(SeckillTimeModel seckillTimeModel);
        bool Update(SeckillTimeModel seckillTimeModel);
        bool Delete(SeckillTimeModel seckillTimeModel);
        bool SeckillTimeModelExists(int id);
    }
}
