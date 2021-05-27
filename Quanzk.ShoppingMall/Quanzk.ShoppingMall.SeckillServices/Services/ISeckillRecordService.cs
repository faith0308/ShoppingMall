using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Services
{
    /// <summary>
    /// 秒杀记录服务接口
    /// </summary>
    public interface ISeckillRecordService: IDependency
    {
        IEnumerable<SeckillRecord> GetSeckillRecords();
        SeckillRecord GetSeckillRecordById(int id);
        bool Create(SeckillRecord seckillRecord);
        bool Update(SeckillRecord seckillRecord);
        bool Delete(SeckillRecord seckillRecord);
        bool SeckillRecordExists(int id);
    }
}
