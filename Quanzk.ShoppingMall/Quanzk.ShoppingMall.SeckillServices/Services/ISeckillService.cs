using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀服务接口
    /// </summary>
    public interface ISeckillService : IDependency
    {
        IEnumerable<Seckill> GetSeckills();
        IEnumerable<Seckill> GetSeckills(Seckill seckill);
        Seckill GetSeckillById(int id);
        public Seckill GetSeckillByProductId(int productId);
        bool Create(Seckill seckill);
        bool Update(Seckill seckill);
        bool Delete(Seckill seckill);
        bool SeckillExists(int id);
    }
}
