using Quanzk.ShoppingMall.SeckillServices.Context;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀仓储实现
    /// </summary>
    public class SeckillRepository : ISeckillRepository
    {
        private readonly SeckillContext _seckillContext;

        public SeckillRepository(SeckillContext seckillContext)
        {
            _seckillContext = seckillContext;
        }

        public bool Create(Seckill seckill)
        {
            var flag = false;
            if (seckill != null)
            {
                _seckillContext.Seckills.Add(seckill);
                if (_seckillContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(Seckill seckill)
        {
            var flag = false;
            if (seckill != null)
            {
                var entity = _seckillContext.Seckills.Find(seckill.Id);
                if (entity != null)
                {
                    _seckillContext.Seckills.Remove(entity);
                    if (_seckillContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public Seckill GetSeckillById(int id)
        {
            var result = _seckillContext.Seckills.Find(id);
            return result ?? new Seckill();
        }

        public Seckill GetSeckillByProductId(int productId)
        {
            var result = _seckillContext.Seckills.FirstOrDefault(p => p.ProductId == productId);
            return result ?? new Seckill();
        }

        public IEnumerable<Seckill> GetSeckills()
        {
            var result = _seckillContext.Seckills.ToList();
            return result ?? new List<Seckill>();
        }

        public IEnumerable<Seckill> GetSeckills(Seckill seckill)
        {
            var query = _seckillContext.Seckills;
            if (seckill != null)
            {
                if (seckill.Id > 0)
                {
                    query.Where(p => p.Id == seckill.Id);
                }
                if (seckill.SeckillStatus > 0)
                {
                    query.Where(p => p.SeckillStatus == seckill.SeckillStatus);
                }
                if (seckill.SeckillType > 0)
                {
                    query.Where(p => p.SeckillType == seckill.SeckillType);
                }
                if (!string.IsNullOrEmpty(seckill.SeckillName))
                {
                    query.Where(p => p.SeckillName.Contains(seckill.SeckillName));
                }
                if (seckill.ProductId > 0)
                {
                    query.Where(p => p.ProductId == seckill.ProductId);
                }
            }
            return query.ToList() ?? new List<Seckill>();
        }

        public bool SeckillExists(int id)
        {
            return _seckillContext.Seckills.Any(p => p.Id == id);
        }

        public bool Update(Seckill seckill)
        {
            var falg = false;
            if (seckill != null && seckill.Id > 0)
            {
                var entity = _seckillContext.Seckills.Find(seckill.Id);
                if (entity != null)
                {
                    entity.Id = seckill.Id;
                    entity.ProductId = seckill.ProductId;
                    entity.SeckillDescription = seckill.SeckillDescription;
                    entity.SeckillIstop = seckill.SeckillIstop;
                    entity.SeckillLimit = seckill.SeckillLimit;
                    entity.SeckillName = seckill.SeckillName;
                    entity.SeckillPercent = seckill.SeckillPercent;
                    entity.SeckillPrice = seckill.SeckillPrice;
                    entity.SeckillStatus = seckill.SeckillStatus;
                    entity.SeckillStock = seckill.SeckillStock;
                    entity.SeckillType = seckill.SeckillType;
                    entity.SeckillUrl = seckill.SeckillUrl;
                    _seckillContext.Seckills.Update(entity);
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
