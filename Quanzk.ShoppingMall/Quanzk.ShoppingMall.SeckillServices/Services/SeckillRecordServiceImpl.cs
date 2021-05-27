using Quanzk.ShoppingMall.SeckillServices.Models;
using Quanzk.ShoppingMall.SeckillServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀记录服务实现
    /// </summary>
    public class SeckillRecordServiceImpl : ISeckillRecordService
    {
        private readonly ISeckillRecordRepository _seckillRecordRepository;

        public SeckillRecordServiceImpl(ISeckillRecordRepository seckillRecordRepository)
        {
            _seckillRecordRepository = seckillRecordRepository;
        }

        public bool Create(SeckillRecord seckillRecord)
        {
            return _seckillRecordRepository.Create(seckillRecord);
        }

        public bool Delete(SeckillRecord seckillRecord)
        {
            return _seckillRecordRepository.Delete(seckillRecord);
        }

        public SeckillRecord GetSeckillRecordById(int id)
        {
            return _seckillRecordRepository.GetSeckillRecordById(id);
        }

        public IEnumerable<SeckillRecord> GetSeckillRecords()
        {
            return _seckillRecordRepository.GetSeckillRecords();
        }

        public bool SeckillRecordExists(int id)
        {
            return _seckillRecordRepository.SeckillRecordExists(id);
        }

        public bool Update(SeckillRecord seckillRecord)
        {
            return _seckillRecordRepository.Update(seckillRecord);
        }
    }
}
