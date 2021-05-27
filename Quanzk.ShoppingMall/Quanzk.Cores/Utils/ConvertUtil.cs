using Quanzk.Cores.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Utils
{
    /// <summary>
    /// 对象转换类
    /// </summary>
    public class ConvertUtil
    {
        /// <summary>
        /// 结果转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="middleResult"></param>
        /// <returns></returns>
        public static T MiddleResultToObject<T>(MiddleResult middleResult) where T : new()
        {
            return DicToObject<T>(middleResult.resultDic);
        }

        /// <summary>
        /// 结果转换成集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="middleResult"></param>
        /// <returns></returns>
        public static IList<T> MiddleResultTList<T>(MiddleResult middleResult) where T : new()
        {
            return ListToObject<T>(middleResult.resultList);
        }

        /// <summary>
        /// List集合转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultList"></param>
        /// <returns></returns>
        public static IList<T> ListToObject<T>(IList<IDictionary<string, object>> resultList) where T : new()
        {
            IList<T> lists = new List<T>();
            foreach (var list in resultList)
            {
                lists.Add(DicToObject<T>(list));
            }
            return lists;
        }


        /// <summary>
        /// List集合字典对象转换成集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public dynamic ListToObject(IList<IDictionary<string, object>> resultList, Type type)
        {
            // 1、创建List泛型类型并指定泛型
            Type listType = typeof(List<>).MakeGenericType(type);
            dynamic value = Activator.CreateInstance(listType);
            foreach (var list in resultList)
            {
                // 2、转换成泛型对象
                value.Add(DicToObject(list, type));
            }
            return value;
        }

        /// <summary>
        /// 字典转换成对象
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public dynamic DicToObject(IDictionary<string, object> dic, Type type)
        {
            var entity = Activator.CreateInstance(type);
            var fields = type.GetProperties();
            string val = string.Empty;
            object obj = null;

            foreach (var field in fields)
            {
                if (!dic.ContainsKey(field.Name))
                    continue;
                val = Convert.ToString(dic[field.Name]);

                object defaultVal;
                if (field.PropertyType.Name.Equals("String"))
                    defaultVal = "";
                else if (field.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (field.PropertyType.Name.Equals("Decimal"))
                    defaultVal = 0M;
                else
                    defaultVal = 0;

                if (!field.PropertyType.IsGenericType)
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                else
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                }

                field.SetValue(entity, obj, null);
            }

            return entity;
        }

        private static T DicToObject<T>(IDictionary<string, object> dic) where T : new()
        {
            Type myType = typeof(T);
            T entity = new T();
            var fields = myType.GetProperties();
            string val = string.Empty;
            object obj = null;
            foreach (var field in fields)
            {
                if (!dic.ContainsKey(field.Name))
                {
                    continue;
                }
                val = Convert.ToString(dic[field.Name]);

                object defaultVal;
                if (field.PropertyType.Name.Equals("String"))
                {
                    defaultVal = "";
                }
                else if (field.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (field.PropertyType.Name.Equals("Decimal"))
                {
                    defaultVal = 0M;
                }
                else
                {
                    defaultVal = 0;
                }

                if (!field.PropertyType.IsGenericType)
                {
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                }
                else
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                    field.SetValue(entity, obj, null);
                }
            }
            return entity;
        }
    }
}
