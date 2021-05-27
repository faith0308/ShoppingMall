using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 秒杀微服务客户端
    /// </summary>
    [MicroClient("https", "SeckillServices")]
    public interface ISeckillsClient
    {
        /// <summary>
        /// 查询秒杀活动集合
        /// </summary>
        /// <returns></returns>
        [GetPath("/Seckills")]
        public List<Seckill> GetSeckills();

        /// <summary>
        /// 根据秒杀Id查询秒杀活动
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GetPath("/Seckills/{seckillId}")]
        public Seckill GetSeckill(int productId);

        /// <summary>
        /// 查询秒杀活动，通过时间条件查询
        /// </summary>
        /// <param name="timeId"></param>
        /// <returns></returns>
        [GetPath("/Seckills/GetList")]
        public List<Seckill> GetSeckillsByTimeId(string timeId);
    }
}
