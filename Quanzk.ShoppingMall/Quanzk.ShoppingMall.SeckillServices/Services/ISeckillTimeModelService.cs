using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀时间服务接口
    /// </summary>
    public interface ISeckillTimeModelService : IDependency
    {
        IEnumerable<SeckillTimeModel> GetSeckillTimeModels();
        SeckillTimeModel GetSeckillTimeModelById(int id);
        bool Create(SeckillTimeModel seckillTime);
        bool Update(SeckillTimeModel seckillTime);
        bool Delete(SeckillTimeModel seckillTime);
        bool SeckillTimeModelExists(int id);
    }
}
