using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀记录仓储接口
    /// </summary>
    public interface ISeckillRecordRepository : IDependency
    {
        IEnumerable<SeckillRecord> GetSeckillRecords();
        SeckillRecord GetSeckillRecordById(int id);
        bool Create(SeckillRecord seckillRecord);
        bool Update(SeckillRecord seckillRecord);
        bool Delete(SeckillRecord seckillRecord);
        bool SeckillRecordExists(int id);
    }
}
