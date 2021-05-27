using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Utils
{
    /// <summary>
    /// http 参数工具类
    /// </summary>
    public class HttpParamUtil
    {
        public static string DicToHttpUrlParam(IDictionary<string, object> reqParam)
        {
            var result = "";
            if (reqParam.Count != 0)
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append("?");
                foreach (var item in reqParam)
                {
                    sbStr.Append(item.Key);
                    sbStr.Append("=");
                    sbStr.Append(item.Value);
                    sbStr.Append("&");
                }
                var urlParam = sbStr.ToString();
                result = urlParam.Substring(0, urlParam.Length - 1);
            }
            return result;
        }
    }
}
