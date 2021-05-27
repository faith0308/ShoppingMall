using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Filters
{
    /// <summary>
    /// 通用 返回结果对象
    /// </summary>
    public class CommonResult
    {
        /// <summary>
        /// 是否成功状态
        /// </summary>
        public string Code { set; get; }
        /// <summary>
        /// 失败信息
        /// </summary>
        public string MsgInfo { set; get; }
        /// <summary>
        /// 用于非结果集返回
        /// </summary>
        public IDictionary<string, object> resultDic { set; get; }
        /// <summary>
        /// 用于结果集返回
        /// </summary>
        public IList<IDictionary<string, object>> resultList { set; get; }
        /// <summary>
        /// 返回动态结果(通用)
        /// </summary>
        public dynamic Result { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public CommonResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msgInfo"></param>
        public CommonResult(string code, string msgInfo)
        {
            this.Code = code;
            this.MsgInfo = msgInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msgInfo"></param>
        /// <param name="resultDic"></param>
        /// <param name="resultList"></param>
        public CommonResult(string code, string msgInfo, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(code, msgInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}

