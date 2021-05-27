using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware.Urls
{
    /// <summary>
    /// 动态中台 Url
    /// </summary>
    public interface IDynamicMiddleUrl
    {
        /// <summary>
        ///  获取中台Url
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">名称</param>
        /// <returns></returns>
        public string GetMiddleUrl(string urlShcme, string serviceName);
    }
}
