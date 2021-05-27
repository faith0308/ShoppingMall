using Quanzk.ShoppingMall.SeckillServices.Models;
using Quanzk.ShoppingMall.SeckillServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀服务实现
    /// </summary>
    public class SeckillServiceImpl : ISeckillService
    {
        private readonly ISeckillRepository _seckillRepository;

        public SeckillServiceImpl(ISeckillRepository seckillRepository)
        {
            _seckillRepository = seckillRepository;
        }

        public bool Create(Seckill seckill)
        {
            return _seckillRepository.Create(seckill);
        }

        public bool Delete(Seckill seckill)
        {
            return _seckillRepository.Delete(seckill);
        }

        public Seckill GetSeckillById(int id)
        {
            return _seckillRepository.GetSeckillById(id);
        }

        public Seckill GetSeckillByProductId(int productId)
        {
            return _seckillRepository.GetSeckillByProductId(productId);
        }

        public IEnumerable<Seckill> GetSeckills()
        {
            return _seckillRepository.GetSeckills();
        }

        public IEnumerable<Seckill> GetSeckills(Seckill seckill)
        {
            return _seckillRepository.GetSeckills(seckill);
        }

        public bool SeckillExists(int id)
        {
            return _seckillRepository.SeckillExists(id);
        }

        public bool Update(Seckill seckill)
        {
            return _seckillRepository.Update(seckill);
        }
    }
}
