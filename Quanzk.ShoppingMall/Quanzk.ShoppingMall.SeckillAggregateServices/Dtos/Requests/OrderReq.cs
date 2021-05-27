﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Requests
{
    /// <summary>
    /// 订单表单(接受参数)
    /// </summary>
    public class OrderReq
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { set; get; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductCount { set; get; }
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal OrderTotalPrice { set; get; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductUrl { set; get; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { set; get; } 
    }
}
