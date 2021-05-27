using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Middleware
{
    /// <summary>
    /// 中台返回结果处理对象
    /// </summary>
    public class MiddleResult
    {
        /// <summary>
        /// 成功状态码 默认0成功，-1失败
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string MsgInfo { get; set; }

        /// <summary>
        /// 非结果集类型返回
        /// </summary>
        public IDictionary<string, object> resultDic { get; set; }

        /// <summary>
        /// 结果集类型返回
        /// </summary>
        public IList<IDictionary<string, object>> resultList { get; set; }

        /// <summary>
        /// 动态类型返回（通用）
        /// </summary>
        public dynamic Result { get; set; }

        //public MiddleResult()
        //{
        //    resultDic = new Dictionary<string, object>();
        //    resultList = new List<IDictionary<string, object>>();
        //}

        public MiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
        }

        public MiddleResult(string code, string msgInfo)
        {
            this.Code = code;
            this.MsgInfo = msgInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string code, string msgInfo, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(code, msgInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
        }

        /// <summary>
        /// 中台结果Json串转换成为MiddleResult
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static MiddleResult JsonToMiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
            return result;
        }
    }
}
