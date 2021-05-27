using Quanzk.ShoppingMall.SeckillServices.Context;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀记录仓储实现
    /// </summary>
    public class SeckillRecordRepository : ISeckillRecordRepository
    {
        private readonly SeckillContext _seckillContext;

        public SeckillRecordRepository(SeckillContext seckillContext)
        {
            _seckillContext = seckillContext;
        }

        public bool Create(SeckillRecord seckillRecord)
        {
            var flag = false;
            if (seckillRecord != null)
            {
                _seckillContext.SeckillRecords.Add(seckillRecord);
                if (_seckillContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(SeckillRecord seckillRecord)
        {
            var flag = false;
            if (seckillRecord != null)
            {
                var entity = _seckillContext.SeckillRecords.Find(seckillRecord.Id);
                if (entity != null)
                {
                    _seckillContext.SeckillRecords.Remove(entity);
                    if (_seckillContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public SeckillRecord GetSeckillRecordById(int id)
        {
            var result = _seckillContext.SeckillRecords.Find(id);
            return result ?? new SeckillRecord();
        }

        public IEnumerable<SeckillRecord> GetSeckillRecords()
        {
            var result = _seckillContext.SeckillRecords.ToList();
            return result ?? new List<SeckillRecord>();
        }

        public bool SeckillRecordExists(int id)
        {
            return _seckillContext.SeckillRecords.Any(p => p.Id == id);
        }

        public bool Update(SeckillRecord seckillRecord)
        {
            var falg = false;
            if (seckillRecord != null && seckillRecord.Id > 0)
            {
                var entity = _seckillContext.SeckillRecords.Find(seckillRecord.Id);
                if (entity != null)
                {
                    entity.Id = seckillRecord.Id;
                    entity.SeckillId = seckillRecord.SeckillId;
                    entity.Updatetime = DateTime.Now;
                    entity.RecordStatus = seckillRecord.RecordStatus;
                    entity.RecordTotalprice = seckillRecord.RecordTotalprice;
                    entity.SeckillNum = seckillRecord.SeckillNum;
                    entity.SeckillPrice = seckillRecord.SeckillPrice;
                    entity.ProductPrice = seckillRecord.ProductPrice;
                    entity.UserId = seckillRecord.UserId;
                    entity.OrderSn = seckillRecord.OrderSn;
                    _seckillContext.SeckillRecords.Update(entity);
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
