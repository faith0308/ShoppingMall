using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Utils
{
    /// <summary>
    /// 通用功能
    /// </summary>
    public class CommonUtil
    {
        #region 字典工具
        /// <summary>
        /// 对象转换成字典
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDictionary<string, object> ToDictonary(object value)
        {
            IDictionary<string, object> valuePairs = new Dictionary<string, object>();
            // 获取反射类型
            Type type = value.GetType();
            // 获取所有反射属性
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // 遍历PropertyInfo
            foreach (PropertyInfo info in propertyInfos)
            {
                valuePairs.Add(info.Name, Convert.ToString(info.GetValue(value)));
            }
            return valuePairs;
        }
        #endregion

        #region 订单号工具类(单机)

        //订单编号后缀（核心部分）
        private static long code;
        private static object lockObject = new object();

        /// <summary>
        /// 创建订单号
        /// </summary>
        /// <param name="prefix">前缀(可空)</param>
        /// <returns></returns>
        public static string CreateOrderCode(string prefix)
        {
            lock (lockObject)
            {
                code++;
                string str = DateTime.Now.ToString("yyyyMMddHHmmss");
                long m = long.Parse((str)) * 10000;
                m += code;
                return prefix + m;
            }
        }
        #endregion
    }
}
