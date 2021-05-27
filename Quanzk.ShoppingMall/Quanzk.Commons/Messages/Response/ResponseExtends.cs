using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 请求响应扩展类
    /// </summary>
    public class ResponseExtends
    {
        /// <summary>
        /// 返回实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task<ResponsePackage<T>> GetResponsePackage<T>(T result)
        {
            ResponsePackage<T> response = (ResponsePackage<T>)new ResponsePackage<T>().Success();
            response.Result = result;
            return await Task.FromResult(response);
        }

        /// <summary>
        /// 返回分页结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="pageIndex"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static async Task<ResponsePagePackage<T>> GetResponsePagePackage<T>(IEnumerable<T> result, int pageIndex, int rows)
        {
            ResponsePagePackage<T> response = (ResponsePagePackage<T>)new ResponsePagePackage<T>().Success();
            response.Results = result;
            response.PageIndex = pageIndex;
            response.Rows = rows;
            return await Task.FromResult(response);
        }

        /// <summary>
        /// 返回列表结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task<ResponseListPackage<T>> GetResponseListPackage<T>(IEnumerable<T> result)
        {
            ResponseListPackage<T> response = (ResponseListPackage<T>)new ResponseListPackage<T>().Success();
            response.Results = result;
            return await Task.FromResult(response);
        }
    }
}
