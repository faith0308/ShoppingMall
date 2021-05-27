using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 秒杀记录客户端
    /// </summary>
    [MicroClient("https", "SeckillServices")]
    public interface ISeckillTimeClient
    {
        /// <summary>
        /// 查询秒杀时间表
        /// </summary>
        /// <returns></returns>
        [GetPath("/SeckillTimeModels")]
        public List<SeckillTimeModel> GetSeckillTimeModels();

        /// <summary>
        /// 根据时间查询秒杀活动
        /// </summary>
        /// <param name="timeId"></param>
        /// <returns></returns>
        [GetPath("/SeckillTimeModels/{timeId}/Seckills")]
        public List<Seckill> GetSeckills(int timeId);
    }
}
