using Quanzk.ShoppingMall.SeckillServices.Models;
using Quanzk.ShoppingMall.SeckillServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀时间服务实现
    /// </summary>
    public class SeckillTimeModelServiceImpl : ISeckillTimeModelService
    {
        private readonly ISeckillTimeModelRepository _seckillTimeModelRepository;

        public SeckillTimeModelServiceImpl(ISeckillTimeModelRepository seckillTimeModelRepository)
        {
            _seckillTimeModelRepository = seckillTimeModelRepository;
        }

        public bool Create(SeckillTimeModel seckillTime)
        {
            return _seckillTimeModelRepository.Create(seckillTime);
        }

        public bool Delete(SeckillTimeModel seckillTime)
        {
            return _seckillTimeModelRepository.Delete(seckillTime);
        }

        public SeckillTimeModel GetSeckillTimeModelById(int id)
        {
            return _seckillTimeModelRepository.GetSeckillTimeModelById(id);
        }

        public IEnumerable<SeckillTimeModel> GetSeckillTimeModels()
        {
            return _seckillTimeModelRepository.GetSeckillTimeModels();
        }

        public bool SeckillTimeModelExists(int id)
        {
            return _seckillTimeModelRepository.SeckillTimeModelExists(id);
        }

        public bool Update(SeckillTimeModel seckillTime)
        {
            return _seckillTimeModelRepository.Update(seckillTime);
        }
    }
}
