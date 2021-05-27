using Quanzk.ShoppingMall.SeckillServices.Context;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀时间仓储实现
    /// </summary>
    public class SeckillTimeModelRepository : ISeckillTimeModelRepository
    {
        private readonly SeckillContext _seckillContext;

        public SeckillTimeModelRepository(SeckillContext seckillContext)
        {
            _seckillContext = seckillContext;
        }

        public bool Create(SeckillTimeModel seckillTimeModel)
        {
            var flag = false;
            if (seckillTimeModel != null)
            {
                _seckillContext.seckillTimeModels.Add(seckillTimeModel);
                if (_seckillContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(SeckillTimeModel seckillTimeModel)
        {
            var flag = false;
            if (seckillTimeModel != null)
            {
                var entity = _seckillContext.seckillTimeModels.Find(seckillTimeModel.Id);
                if (entity != null)
                {
                    _seckillContext.seckillTimeModels.Remove(entity);
                    if (_seckillContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public SeckillTimeModel GetSeckillTimeModelById(int id)
        {
            var result = _seckillContext.seckillTimeModels.Find(id);
            return result ?? new SeckillTimeModel();
        }

        public IEnumerable<SeckillTimeModel> GetSeckillTimeModels()
        {
            var result = _seckillContext.seckillTimeModels.ToList();
            return result ?? new List<SeckillTimeModel>();
        }

        public bool SeckillTimeModelExists(int id)
        {
            return _seckillContext.seckillTimeModels.Any(p => p.Id == id);
        }

        public bool Update(SeckillTimeModel seckillTimeModel)
        {
            var falg = false;
            if (seckillTimeModel != null && seckillTimeModel.Id > 0)
            {
                var entity = _seckillContext.seckillTimeModels.Find(seckillTimeModel.Id);
                if (entity != null)
                {
                    entity.Id = seckillTimeModel.Id;
                    entity.TimeTitleUrl = seckillTimeModel.TimeTitleUrl;
                    entity.SeckillDate = seckillTimeModel.SeckillDate;
                    entity.SeckillStarttime = seckillTimeModel.SeckillStarttime;
                    entity.SeckillEndtime = seckillTimeModel.SeckillEndtime;
                    entity.TimeStatus = seckillTimeModel.TimeStatus;
                    _seckillContext.seckillTimeModels.Update(entity);
                    if (_seckillContext.SaveChanges() > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }
    }
}
