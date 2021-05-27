using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Filters
{
    /// <summary>
    /// Action 返回结果 过滤器
    /// </summary>
    public class CommonResultWrapperFilter : IAsyncResultFilter
    {
        /// <summary>
        /// 返回过滤器封装
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode;
                if (statusCode == 200 || statusCode == 201 || statusCode == 202 || !statusCode.HasValue)
                {
                    // 返回正常结果
                    objectResult.Value = WrapSuccessResult(objectResult.Value);
                }
                else
                {
                    // 返回异常结果
                    objectResult.Value = WrapFailResult(objectResult);
                }
            }
            await next();
        }

        /// <summary>
        /// 正常结果封装
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private object WrapSuccessResult(object value)
        {
            // 定义返回结果
            dynamic warpResult = new ExpandoObject();
            warpResult.Code = "0";
            warpResult.MsgInfo = "";

            // 2、获取结果(输出到页面：结果集List<>，非结果集Dic)
            if (value.GetType().Name.Contains("List"))
            {
                // 转换成json
                warpResult.ResultList = new JsonResult(value).Value;
            }
            // 判断是否为字典
            else if (value.GetType().Name.Contains("Dictionary"))
            {
                // 判断是否含有MsgInfo
                IDictionary dictionary = (IDictionary)value;
                if (dictionary.Contains("MsgInfo"))
                {
                    warpResult.Code = dictionary["Code"];
                    warpResult.MsgInfo = dictionary["MsgInfo"];
                    // 移除字典中的Code和MsgInfo
                    dictionary.Remove("Code");
                    dictionary.Remove("MsgInfo");
                }
                // 获取结果
                warpResult.ResultDic = new JsonResult(value).Value;
            }
            else
            {
                warpResult.Result = new JsonResult(value).Value;
            }
            return warpResult;
        }

        /// <summary>
        /// 异常结果封装
        /// </summary>
        /// <param name="objectResult"></param>
        /// <returns></returns>
        private object WrapFailResult(ObjectResult objectResult)
        {
            // 定义返回结果
            dynamic warpResult = new ExpandoObject();
            warpResult.Code = objectResult.StatusCode;
            if (objectResult.Value is string strResult)
            {
                // 字符串异常信息
                warpResult.MsgInfo = strResult;
            }
            else
            {
                // 类型异常信息
                warpResult.MsgInfo = new JsonResult(objectResult.Value).Value;
            }
            return warpResult;
        }
    }
}
